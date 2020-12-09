using System;
using System.Drawing;

namespace BarcodeMaster.Codes
{
    /// <summary>
    /// Class CodeWriter is base class for all code writers
    /// </summary>

    abstract class CodeWriter
    {
        /// <summary>
        /// Method TryConvertData do try to convert data to desiring format
        /// </summary>
        /// <typeparam name="T">string or byte</typeparam>
        /// <param name="inputData">input data</param>
        /// <param name="data">converted data</param>
        /// <returns>Returns true if conversion is successful false if not</returns>
        internal abstract bool TryConvertData<T>(T inputData, out byte[] data);

        /// <summary>
        /// Method CreateCodeTemplate creates template to draw
        /// </summary>
        /// <param name="data">converted data</param>
        /// <returns>Returns template to draw</returns>
        internal abstract byte[,] CreateCodeTemplate(byte[] data);

        /// <summary>
        /// Method DrawCode draws code by template
        /// </summary>
        /// <param name="codeTemplate">code template</param>
        /// <returns>Returns code image in bitmap</returns>
        internal abstract Bitmap DrawCode(byte[,] codeTemplate);

        /// <summary>
        /// Method Write writes data on code
        /// </summary>
        /// <param name="inputData">data in string or byte</param>
        /// <returns>Returns code image in bitmap</returns>
        internal Bitmap Write<T>(T inputData)
        {
            byte[,] template;
            Bitmap codeImage;

            codeImage = default;
            if(TryConvertData<T>(inputData, out byte[] data))
            {
                template = CreateCodeTemplate(data);
                codeImage = DrawCode(template);
            }

            GC.Collect();
            return codeImage;
        }
    }
}
