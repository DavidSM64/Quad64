using Collada141;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Quad64.src.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Quad64
{
    class Model3D
    {
        public class MeshData
        {
            public int vbo, ibo, texBuf, colorBuf;
            public Vector3[] vertices;
            public Vector2[] texCoord;
            public Vector4[] colors;
            public uint[] indices;
            public Texture2D texture;

            public override string ToString() {
                return "Texture [ID/W/H]: [" + texture.ID + "/" + texture.Width + "/" + texture.Height + "]";
            }
        }

        //Vector3 center = new Vector3(0, 0, 0);
        Vector3 upper = new Vector3(0, 0, 0);
        Vector3 lower = new Vector3(0, 0, 0);
        public Vector3 UpperBoundary { get { return upper; } }
        public Vector3 LowerBoundary { get { return lower; } }
        public uint GeoDataSegAddress { get; set; }
        public ModelBuilder builder = new ModelBuilder();
        public List<MeshData> meshes = new List<MeshData>();
        
        public List<ScriptDumpCommandInfo> GeoLayoutCommands_ForDump = new List<ScriptDumpCommandInfo>();
        public List<List<ScriptDumpCommandInfo>> Fast3DCommands_ForDump = new List<List<ScriptDumpCommandInfo>>();
        
        public List<uint> geoDisplayLists = new List<uint>();

        public bool hasGeoDisplayList(uint value)
        {
            for (int i = 0; i < geoDisplayLists.Count; i++)
            {
                if (geoDisplayLists[i] == value)
                    return true;
            }
            geoDisplayLists.Add(value);
            Fast3DCommands_ForDump.Add(new List<ScriptDumpCommandInfo>());
            return false;
        }

        public int getNumberOfTrianglesInModel()
        {
            int count = 0;

            for (int i = 0; i < meshes.Count; i++)
            {
                if(meshes[i] != null && meshes[i].indices != null)
                    count += (meshes[i].indices.Length / 3);
            }

            return count;
        }

        private void calculateCenter()
        {
            float max_x = -1, min_x = -1, max_y = -1, min_y = -1, max_z = -1, min_z = -1;
            uint count = 0;
            foreach (MeshData md in meshes){
                foreach (Vector3 vec in md.vertices) {
                    if (count == 0)
                    {
                        min_x = vec.X; max_x = vec.X;
                        min_y = vec.Y; max_y = vec.Y;
                        min_z = vec.Z; max_z = vec.Z;
                    }
                    else
                    {
                        if (vec.X < min_x)
                            min_x = vec.X;
                        if (vec.X > max_x)
                            max_x = vec.X;
                        if (vec.Y < min_y)
                            min_y = vec.Y;
                        if (vec.Y > max_y)
                            max_y = vec.Y;
                        if (vec.Z < min_z)
                            min_z = vec.Z;
                        if (vec.Z > max_z)
                            max_z = vec.Z;
                         /*Console.WriteLine("Values: [" + max_x + ", " +min_x + ", " +max_y + ", " +min_y + ", " +max_z + ", " + min_z +"]");*/
                    }
                    count++;
                }
            }
            //center = new Vector3((max_x+min_x) / 2, (max_y + min_y) / 2, (max_z + min_z) / 2);
            upper = new Vector3(max_x, max_y, max_z);
            lower = new Vector3(min_x, min_y, min_z);
        }

        public void outputTextureAtlasToPng(string filename)
        {
            TextureAtlasBuilder.TextureAtlas atlas 
                = new TextureAtlasBuilder.TextureAtlas(builder.TextureImages);
            atlas.outputToPNG(filename);
        }

        public void buildBuffers()
        {
            builder.BuildData(ref meshes);
            //Console.WriteLine("#meshes = " + meshes.Count);
            for (int i = 0; i < meshes.Count; i++) {
                MeshData m = meshes[i];
                m.vertices = builder.getVertices(i);
                m.texCoord = builder.getTexCoords(i);
                m.colors = builder.getColors(i);
                m.indices = builder.getIndices(i);

                //combined = ContentPipe.LoadTexture(builder.Atlas.Image);

                m.vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, m.vbo);
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    (IntPtr)(Vector3.SizeInBytes * m.vertices.Length),
                    m.vertices,
                    BufferUsageHint.StaticDraw
                    );

                m.texBuf = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, m.texBuf);
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    (IntPtr)(Vector2.SizeInBytes * m.texCoord.Length),
                    m.texCoord,
                    BufferUsageHint.StaticDraw
                    );

                m.colorBuf = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, m.colorBuf);
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    (IntPtr)(Vector4.SizeInBytes * m.colors.Length),
                    m.colors,
                    BufferUsageHint.StaticDraw
                    );

                m.ibo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, m.ibo);
                GL.BufferData(
                    BufferTarget.ElementArrayBuffer,
                    (IntPtr)(sizeof(uint) * m.indices.Length),
                    m.indices,
                    BufferUsageHint.StaticDraw
                    );
            }
            calculateCenter();
        }

        
        public void drawModel(Vector3 scale, Quaternion rot, Vector3 pos)
        {
            GL.PushMatrix();
            GL.Translate(pos.X, pos.Y, pos.Z);
            //GL.Translate(center.X, center.Y, center.Z);
            GL.Rotate(rot.X, 1, 0, 0);
            GL.Rotate(rot.Y, 0, 1, 0);
            GL.Rotate(rot.Z, 0, 0, 1);
            //GL.Translate(-center.X, -center.Y, -center.Z);
            GL.Scale(scale.X, scale.Y, scale.Z);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            for (int i = 0; i < meshes.Count; i++)
            {
                MeshData m = meshes[i];
                //if (m.vertices == null || m.indices == null) return;
                
                if (m.texture != null)
                {
                    GL.BindTexture(TextureTarget.Texture2D, m.texture.ID);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, m.texture.TextureParamS);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, m.texture.TextureParamT);
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, m.vbo);
                GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
                GL.BindBuffer(BufferTarget.ArrayBuffer, m.texBuf);
                GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);
                GL.BindBuffer(BufferTarget.ArrayBuffer, m.colorBuf);
                GL.ColorPointer(4, ColorPointerType.Float, 0, IntPtr.Zero);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, m.ibo);
                
                if (Globals.doWireframe)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                else
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                
                GL.DrawElements(PrimitiveType.Triangles, m.indices.Length,
                    DrawElementsType.UnsignedInt, IntPtr.Zero);
                
                if (Globals.doWireframe)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.PopMatrix();
        }

        public void dumpModelToCOLLADA(float scale)
        {
            DumpModel.dumpModelToCOLLADA(this, scale);
        }

        public void dumpModelToOBJ(float scale)
        {
            StringBuilder objModel = new StringBuilder();
            StringBuilder objMtl = new StringBuilder();
            objModel.Append("mtllib Level.mtl" + Environment.NewLine);
           // string objModel = "mtllib Level.mtl" + Environment.NewLine;
           // string objMtl = "";
            int index_offset = 1;
            int img_index = 0;
            
            foreach(System.Drawing.Bitmap bmp in builder.TextureImages)
            {
                string filepath = Directory.GetCurrentDirectory() + "\\Level\\" + img_index + ".png";
                (new FileInfo(filepath)).Directory.Create();
                bmp.Save(filepath, System.Drawing.Imaging.ImageFormat.Png);
                objMtl.Append("newmtl Tex_" + img_index + Environment.NewLine);
                objMtl.Append("Ka 0.000000 0.000000 0.000000" + Environment.NewLine +
                    "Kd 1.000000 1.000000 1.000000" + Environment.NewLine +
                    "Ks 0.000000 0.000000 0.000000" + Environment.NewLine);
                objMtl.Append("map_Kd Level/" + img_index + ".png" + Environment.NewLine);
                img_index++;
            }

            for (int i = 0; i < meshes.Count; i++)
            {
                MeshData m = meshes[i];
                if (m.vertices.Length < 1)
                    continue;
                objModel.Append("usemtl Tex_" + i + Environment.NewLine);

                foreach (Vector3 vert in m.vertices)
                {
                    objModel.Append("v " + (vert.X * scale) + " " + (vert.Y * scale) + " " + (vert.Z * scale));
                    objModel.Append(Environment.NewLine);
                }

                foreach (Vector2 texCoord in m.texCoord)
                {
                    float X = texCoord.X, Y = texCoord.Y;
                    if (m.texture.TextureParamS == (int)All.ClampToEdge)
                        X = (X > 1.0f ? 1.0f : (X < 0.0f ? 0.0f : X));
                    if (m.texture.TextureParamT == (int)All.ClampToEdge)
                        Y = (Y > 1.0f ? 1.0f : (Y < 0.0f ? 0.0f : Y));
                    objModel.Append("vt " + X + " " + -Y + Environment.NewLine);
                }

                int largest_value = index_offset;
                for (int j = 0; j < m.indices.Length/3; j++)
                {
                    int v1 = (int)m.indices[(j * 3) + 0] + index_offset;
                    int v2 = (int)m.indices[(j * 3) + 1] + index_offset;
                    int v3 = (int)m.indices[(j * 3) + 2] + index_offset;
                    objModel.Append("f " + v1 + "/" + v1 + " " + v2 + "/" + v2 + " " + v3 + "/" + v3 + Environment.NewLine);

                    largest_value = Math.Max(v1, largest_value);
                    largest_value = Math.Max(v2, largest_value);
                    largest_value = Math.Max(v3, largest_value);
                }
                index_offset = largest_value + 1;
            }
            File.WriteAllText("Level.obj", objModel.ToString());
            File.WriteAllText("Level.mtl", objMtl.ToString());
        }

    }
}
