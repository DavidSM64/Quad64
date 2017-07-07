

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Quad64.src.Viewer
{
    class BoundingBox
    {

        public static void draw_solid(Vector3 scale, Quaternion rot, Vector3 pos, Color color,
            Vector3 upper, Vector3 lower)
        {
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.AlphaTest);
            GL.PushMatrix();
            GL.Translate(pos.X, pos.Y, pos.Z);
            GL.Rotate(rot.X, 1, 0, 0);
            GL.Rotate(rot.Y, 0, 1, 0);
            GL.Rotate(rot.Z, 0, 0, 1);
            GL.Scale(scale.X, scale.Y, scale.Z);

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(color);

            GL.Vertex3(upper.X, upper.Y, lower.Z); // Top-right of top face
            GL.Vertex3(lower.X, upper.Y, lower.Z); // Top-left of top face
            GL.Vertex3(lower.X, upper.Y, upper.Z); // Bottom-left of top face
            GL.Vertex3(upper.X, upper.Y, upper.Z); // Bottom-right of top face

            GL.Vertex3(upper.X, lower.Y, lower.Z); // Top-right of bottom face
            GL.Vertex3(lower.X, lower.Y, lower.Z); // Top-left of bottom face
            GL.Vertex3(lower.X, lower.Y, upper.Z); // Bottom-left of bottom face
            GL.Vertex3(upper.X, lower.Y, upper.Z); // Bottom-right of bottom face

            GL.Vertex3(upper.X, upper.Y, upper.Z); // Top-Right of front face
            GL.Vertex3(lower.X, upper.Y, upper.Z); // Top-left of front face
            GL.Vertex3(lower.X, lower.Y, upper.Z); // Bottom-left of front face
            GL.Vertex3(upper.X, lower.Y, upper.Z); // Bottom-right of front face

            GL.Vertex3(upper.X, lower.Y, lower.Z); // Bottom-Left of back face
            GL.Vertex3(lower.X, lower.Y, lower.Z); // Bottom-Right of back face
            GL.Vertex3(lower.X, upper.Y, lower.Z); // Top-Right of back face
            GL.Vertex3(upper.X, upper.Y, lower.Z); // Top-Left of back face

            GL.Vertex3(lower.X, upper.Y, upper.Z); // Top-Right of left face
            GL.Vertex3(lower.X, upper.Y, lower.Z); // Top-Left of left face
            GL.Vertex3(lower.X, lower.Y, lower.Z); // Bottom-Left of left face
            GL.Vertex3(lower.X, lower.Y, upper.Z); // Bottom-Right of left face

            GL.Vertex3(upper.X, upper.Y, upper.Z); // Top-Right of left face
            GL.Vertex3(upper.X, upper.Y, lower.Z); // Top-Left of left face
            GL.Vertex3(upper.X, lower.Y, lower.Z); // Bottom-Left of left face
            GL.Vertex3(upper.X, lower.Y, upper.Z); // Bottom-Right of left face

            GL.Color4(Color.White);
            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
        }

        public static void draw(Vector3 scale, Quaternion rot, Vector3 pos, Color color, 
            Vector3 upper, Vector3 lower)
        {
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.AlphaTest);
            GL.PushMatrix();
            GL.Translate(pos.X, pos.Y, pos.Z);
            GL.Rotate(rot.X, 1, 0, 0);
            GL.Rotate(rot.Y, 0, 1, 0);
            GL.Rotate(rot.Z, 0, 0, 1);
            GL.Scale(scale.X, scale.Y, scale.Z);

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color4(color);
            
            GL.Vertex3(upper.X, upper.Y, lower.Z); // 1
            GL.Vertex3(lower.X, upper.Y, lower.Z); // 2
            GL.Vertex3(lower.X, upper.Y, upper.Z); // 3
            GL.Vertex3(upper.X, upper.Y, lower.Z); // 1
            GL.Vertex3(upper.X, upper.Y, upper.Z); // 4
            GL.Vertex3(lower.X, upper.Y, upper.Z); // 3
            
            GL.Vertex3(lower.X, lower.Y, upper.Z); // 7
            GL.Vertex3(lower.X, lower.Y, lower.Z); // 6
            GL.Vertex3(upper.X, lower.Y, lower.Z); // 5
            GL.Vertex3(lower.X, lower.Y, upper.Z); // 7
            GL.Vertex3(upper.X, lower.Y, upper.Z); // 8
            GL.Vertex3(upper.X, lower.Y, lower.Z); // 5

            GL.Vertex3(lower.X, upper.Y, lower.Z); // 2
            GL.Vertex3(lower.X, lower.Y, lower.Z); // 6
            GL.Vertex3(lower.X, upper.Y, upper.Z); // 3
            GL.Vertex3(upper.X, lower.Y, upper.Z); // 8
            GL.Vertex3(upper.X, upper.Y, upper.Z); // 4
            GL.Vertex3(upper.X, lower.Y, lower.Z); // 5
            
            GL.Color4(Color.White);
            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
        }
    }
}
