using System.Drawing;

namespace BarcodeMaster.CodeDesigner.Styles
{
    /// <summary>
    /// Defines how draw code
    /// </summary>
    public abstract class Style
    {
        /// <summary>
        /// Method Draw draws code according to the template
        /// </summary>
        /// <param name="template">template of code</param>
        /// <param name="modulSize">size of each data modul</param>
        /// <returns>Returns drawn code in bitmap</returns>
        public abstract Bitmap Draw(byte[,] template, int modulSize);
    }
}
