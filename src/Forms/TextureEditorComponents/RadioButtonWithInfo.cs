using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms.TextureEditorComponents
{
    class RadioButtonWithInfo : RadioButton
    {
        public Texture2D texture { get; set; }
        public Bitmap BitmapImage { get; set; }
        public uint Address { get; set; }
    }
}
