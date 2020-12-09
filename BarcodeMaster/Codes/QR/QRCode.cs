using BarcodeMaster.CodeDesigner;
using BarcodeMaster.CodeDesigner.Styles;
using BarcodeMaster.Codes.QR.Writer;
using System;
using System.Drawing;
using System.Text;

namespace BarcodeMaster.Codes.QR
{
    /// <summary>
    /// Specifies which correction level use
    /// </summary>
    public enum CorrectionLevel
    {
        L,
        M,
        Q,
        H
    }
    /// <summary>
    /// Class QRCode creates QR code
    /// </summary>
    public class QRCode
    {
        readonly QRWriter writer;

        public QRCode()
        {
            writer = new QRWriter();

            Encoding = Encoding.UTF8;
            CorrectionLevel = CorrectionLevel.M;
            ModulSize = 5;
            DrawStyle = DrawStyle.Standrad;
        }

        /// <summary>
        /// Sets error handler
        /// </summary>
        public Action<string> ErrorHandler
        {
            set
            {
                writer.ErrorHandler += value;
            }
        }

        /// <summary>
        /// Gets and sets value of correction level
        /// </summary>
        public CorrectionLevel CorrectionLevel
        {
            get
            {
                return writer.CorrectionLevel;
            }
            set
            {
                writer.CorrectionLevel = value;
            }
        }

        /// <summary>
        /// Gets and sets value of encoding type
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return writer.Encoding;
            }
            set
            {
                writer.Encoding = value;
            }
        }

        /// <summary>
        /// Gets and sets value of modul size in pixels
        /// </summary>
        public int ModulSize
        {
            get
            {
                return writer.ModulSize;
            }
            set
            {
                writer.ModulSize = value;
            }
        }

        /// <summary>
        /// Gets and sets value of draw style
        /// </summary>
        public DrawStyle DrawStyle
        {
            get
            {
                return writer.DrawStyle;
            }
            set
            {
                writer.DrawStyle = value;
            }
        }

        /// <summary>
        /// Sets self draw style 
        /// </summary>
        public Style Style { get; set; }


        /// <summary>
        /// Method Write creates bitmap of QRcode
        /// </summary>
        /// <param name="data">data in string format</param>
        /// <returns>Returns bitmap of QRCode</returns>
        public Bitmap Write(string data)
        {
            return writer.Write(data);
        }

        /// <summary>
        /// Method Write creates bitmap of QRcode
        /// </summary>
        /// <param name="data">data in byte format</param>
        /// <returns>Returns bitmap of QRCode</returns>
        public Bitmap Write(byte[] data)
        {
            return writer.Write(data);
        }
    }
}
