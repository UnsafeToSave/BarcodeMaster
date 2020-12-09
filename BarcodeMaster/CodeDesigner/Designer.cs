using BarcodeMaster.CodeDesigner.Styles;
using System.Drawing;

namespace BarcodeMaster.CodeDesigner
{
    /// <summary>
    /// Specifies how will draw code
    /// </summary>
    public enum DrawStyle
    {
        /// <summary>
        /// A self made draw style. if use this style need add self style in property "Style"
        /// </summary>
        SelfStyle,
        /// <summary>
        /// A standart draw style. Use common black and white modules
        /// </summary>
        Standrad,
    }

    /// <summary>
    /// Class Designer creates graphical representation of code
    /// </summary>
    class Designer
    {
        Style style;
        DrawStyle drawStyle;

        public Designer()
        {
            DrawStyle = DrawStyle.Standrad;
            ModulSize = 5;
        }
        internal DrawStyle DrawStyle { get; set; }
        internal Style CustomStyle { get; set; }
        internal int ModulSize { get; set; }

        /// <summary>
        /// Method Create creates bitmap of code
        /// </summary>
        /// <param name="template">template of code</param>
        /// <returns>Returns bitmap of code</returns>
        internal Bitmap Create(byte[,] template)
        {
            if(DrawStyle != drawStyle)
            {
                style = SetStyle(DrawStyle);
                drawStyle = DrawStyle;
            }
            return style.Draw(template, ModulSize);
        }
        /// <summary>
        /// Method SetStyle sets style which use to draw code 
        /// </summary>
        /// <param name="drawStyle">draw style</param>
        /// <returns>Retruns class style</returns>
        private Style SetStyle(DrawStyle drawStyle)
        {
            switch (drawStyle)
            {
                case DrawStyle.SelfStyle:
                    if (CustomStyle == null)
                        return new Standard();
                    return CustomStyle;
                case DrawStyle.Standrad:
                    return new Standard();
                default:
                    return new Standard();
            }
        }
    }
}
