using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms.ToolStripRenderer
{
    public class CustomToolStripColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return Theme.MAIN_MENUBAR_BORDER;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return Theme.MAIN_MENUBAR_BORDER;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_HIGHLIGHT;
            }
        }

        public override Color MenuStripGradientBegin
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_HIGHLIGHT;
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_HIGHLIGHT;
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Theme.MAIN_MENUBAR_ITEM_SELECTED;
            }
        }
    }
}
