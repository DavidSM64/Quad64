using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quad64
{
    class Texture2D
    {
        private int id;
        private int width, height;

        public int ID { get { return id; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int TextureParamS { get; set; }
        public int TextureParamT { get; set; }

        public Texture2D(int id, int width, int height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }

    }
}
