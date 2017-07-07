using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Quad64.src.Viewer
{
    class SimpleDraw
    {
        
        public static void draw_line(Vector3 start, Vector3 end, Color colorStart, Color colorEnd)
        {
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.AlphaTest);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.Lines);
            GL.Color4(colorStart);
            GL.Vertex3(start);
            GL.Color4(colorEnd);
            GL.Vertex3(end);

            GL.Color4(colorStart);
            GL.Vertex3(start + new Vector3(0, 1f, 0f));
            GL.Color4(colorEnd);
            GL.Vertex3(end + new Vector3(0, 1f, 0f));

            GL.Color4(colorStart);
            GL.Vertex3(start + new Vector3(0, -1f, 0f));
            GL.Color4(colorEnd);
            GL.Vertex3(end + new Vector3(0, -1f, 0f));
            
            GL.Color4(Color.White);
            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
        }
    }
}
