using System.Reflection;
using System.Windows.Forms;

namespace PropertyGridExtensionHacks
{
    public static class PropertyGridExtensions
    {
        /// <summary>
        /// Gets the (private) PropertyGridView instance.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>The PropertyGridView instance.</returns>
        private static object GetPropertyGridView(PropertyGrid propertyGrid)
        {
            //private PropertyGridView GetPropertyGridView();
            //PropertyGridView is an internal class...
            MethodInfo methodInfo = typeof(PropertyGrid).GetMethod("GetPropertyGridView", BindingFlags.NonPublic | BindingFlags.Instance);
            return methodInfo.Invoke(propertyGrid, new object[] { });
        }

        /// <summary>
        /// Gets the width of the left column.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>
        /// The width of the left column.
        /// </returns>
        public static int GetInternalLabelWidth(this PropertyGrid propertyGrid)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            //protected int InternalLabelWidth
            PropertyInfo propInfo = gridView.GetType().GetProperty("InternalLabelWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            return (int)propInfo.GetValue(gridView, null);
        }

        /// <summary>
        /// Moves the splitter to the supplied horizontal position.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="xpos">The horizontal position.</param>
        public static void MoveSplitterTo(this PropertyGrid propertyGrid, int xpos)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            //private void MoveSplitterTo(int xpos);
            MethodInfo methodInfo = gridView.GetType().GetMethod("MoveSplitterTo", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(gridView, new object[] { xpos });
        }
    }
}