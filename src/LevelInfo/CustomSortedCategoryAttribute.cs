using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Quad64.src.LevelInfo
{
    public class CustomSortedCategoryAttribute : CategoryAttribute
    {
        private const char NonPrintableChar = '\t';

        public CustomSortedCategoryAttribute(string category,
                                                ushort categoryPos,
                                                ushort totalCategories)
            : base(category.PadLeft(category.Length + (totalCategories - categoryPos), NonPrintableChar))
        {
        }
    }
}
