using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad64.src.Scripts
{

    class ModelBuilder
    {
        public struct FinalMesh
        {
            public List<Vector3> vertices;
            public List<Vector2> texCoords;
            public List<Vector4> colors;
            public List<uint> indices;
        }

        public struct TempMesh
        {
            public List<Vector3> vertices;
            public List<Vector2> texCoords;
            public List<Vector4> colors;
            public FinalMesh final;
        }

        public struct TextureInfo
        {
            public int wrapS, wrapT;
        }

        public bool processingTexture = false;

        public float currentScale = 1f;
        private int currentMaterial = -1;
        public int numTriangles = 0;
        public int CurrentMaterial { get { return currentMaterial; } }

        private List<Bitmap> textureImages = new List<Bitmap>();
        private List<uint> textureAddresses = new List<uint>();
        private List<TextureInfo> textureInfo = new List<TextureInfo>();

        public List<Bitmap> TextureImages { get { return textureImages; } }
        public List<uint> TextureAddresses { get { return textureAddresses; } }
        public List<TextureInfo> TexInfo { get { return textureInfo; } }
        private List<TempMesh> TempMeshes = new List<TempMesh>();
        private FinalMesh finalMesh;

        public bool UsesFog { get; set; }
        public Color FogColor { get; set; }
        public List<uint> FogColor_romLocation = new List<uint>();

        private Vector3 offset = new Vector3(0, 0, 0);
        public Vector3 Offset { get { return offset; } set { offset = value; } }

        private FinalMesh newFinalMesh()
        {
            FinalMesh m = new FinalMesh();
            m.vertices = new List<Vector3>();
            m.texCoords = new List<Vector2>();
            m.colors = new List<Vector4>();
            m.indices = new List<uint>();
            return m;
        }
        private TempMesh newTempMesh()
        {
            TempMesh m = new TempMesh();
            m.vertices = new List<Vector3>();
            m.texCoords = new List<Vector2>();
            m.colors = new List<Vector4>();
            m.final = newFinalMesh();
            return m;
        }
        public TextureInfo newTexInfo(int wrapS, int wrapT)
        {
            TextureInfo info = new TextureInfo();
            info.wrapS = wrapS;
            info.wrapT = wrapT;
            return info;
        }

        public void AddTexture(Bitmap bmp, TextureInfo info, uint segmentAddress)
        {
            currentMaterial = textureImages.Count;
            textureImages.Add(bmp);
            textureAddresses.Add(segmentAddress);
            textureInfo.Add(info);
            TempMeshes.Add(newTempMesh());
        }

        public void AddTempVertex(Vector3 pos, Vector2 uv, Vector4 color)
        {
            pos += offset;
            if (currentScale != 1f)
                pos *= currentScale;
            //Console.WriteLine("currentMaterial = " + currentMaterial + ", totalCount = " + textureImages.Count);
            if (currentMaterial == -1)
            {
                AddTexture(
                    TextureFormats.createColorTexture(System.Drawing.Color.White),
                    newTexInfo((int)OpenTK.Graphics.OpenGL.All.Repeat, (int)OpenTK.Graphics.OpenGL.All.Repeat),
                    0x00000000
                );
            }
            TempMeshes[currentMaterial].vertices.Add(pos);
            TempMeshes[currentMaterial].texCoords.Add(uv);
            TempMeshes[currentMaterial].colors.Add(color);
        }
        /*
        private void AddFinalVertex(Vector3 pos, Vector2 uv, Vector4 color)
        {
            finalMesh.vertices.Add(pos);
            finalMesh.texCoords.Add(uv);
            finalMesh.colors.Add(color);
        }*/
        
        private int doesVertexAlreadyExist(int index, Vector3 pos, Vector2 uv, Vector4 col)
        {
            TempMesh tmp = TempMeshes[index];
            for (int i = 0; i < tmp.final.vertices.Count; i++)
            {
                Vector3 v = tmp.final.vertices[i];
                if(pos.X == v.X && pos.Y == v.Y && pos.Z == v.Z)
                {
                    Vector2 t = tmp.final.texCoords[i];
                    if (uv.X == t.X && uv.Y == t.Y)
                    {
                        Vector4 c = tmp.final.colors[i];
                        if (col.X == c.X && col.Y == c.Y && col.Z == c.Z && col.W == c.W)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public void BuildData(ref List<Model3D.MeshData> meshes) {
            finalMesh = newFinalMesh();
            for (int t = 0; t < TempMeshes.Count; t++) {
                uint indexCount = 0;
                Model3D.MeshData md = new Model3D.MeshData();
                Bitmap bmp = textureImages[t];
                md.texture = ContentPipe.LoadTexture(ref bmp);
                md.texture.TextureParamS = textureInfo[t].wrapS;
                md.texture.TextureParamT = textureInfo[t].wrapT;
                TempMesh temp = TempMeshes[t];
                for (int i = 0; i < temp.vertices.Count; i++)
                {
                    int vExists = doesVertexAlreadyExist(t, temp.vertices[i], temp.texCoords[i], temp.colors[i]);
                    if (vExists < 0)
                    {
                        Vector2 texCoord = temp.texCoords[i];
                        texCoord.X /= (float)bmp.Width * 32.0f;
                        texCoord.Y /= (float)bmp.Height * 32.0f;
                        temp.final.vertices.Add(temp.vertices[i]);
                        temp.final.texCoords.Add(texCoord);
                        temp.final.colors.Add(temp.colors[i]);
                        temp.final.indices.Add(indexCount);
                        indexCount++;
                    }
                    else
                    {
                        temp.final.indices.Add((uint)vExists);
                    }
                }
                meshes.Add(md);
            }
        }

        public Vector3[] getVertices(int i) {
            return TempMeshes[i].final.vertices.ToArray();
        }

        public Vector2[] getTexCoords(int i)
        {
            return TempMeshes[i].final.texCoords.ToArray();
        }

        public Vector4[] getColors(int i)
        {
            return TempMeshes[i].final.colors.ToArray();
        }

        public uint[] getIndices(int i)
        {
            return TempMeshes[i].final.indices.ToArray();
        }
        
        public bool hasTexture(uint segmentAddress)
        {
            int index = 0;
            foreach (uint addr in textureAddresses)
            {
                if (addr == segmentAddress)
                {
                    currentMaterial = index;
                    return true;
                }
                index++;
            }
            return false;
        }
    }
}
