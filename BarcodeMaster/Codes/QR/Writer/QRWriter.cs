using BarcodeMaster.CodeDesigner;
using BarcodeMaster.CodeDesigner.Styles;
using BarcodeMaster.Codes.QR.Writer.DataConversion;
using BarcodeMaster.Codes.QR.Writer.DataConversion.Encoders;
using System;
using System.Drawing;
using System.Text;

namespace BarcodeMaster.Codes.QR.Writer
{
    /// <summary>
    /// Specifies which type of coding will use
    /// </summary>
    internal enum CodingType
    {
        /// <summary>
        /// A numeric coding type use when data contains only digit
        /// </summary>
        Numeric = 1,
        /// <summary>
        /// A alphanumeric type use when data contains digits[0-9], letters[A-Z], space and ($,%,*,+,-,.,/,:)
        /// </summary>
        Alphanumeric = 2,
        /// <summary>
        /// A byte type use when data is byte sequence
        /// </summary>
        Byte = 4
    }

    /// <summary>
    /// Class Writer binds basic logic creation QR code 
    /// </summary>
    internal class QRWriter : CodeWriter
    {
        internal event Action<string> ErrorHandler;

        readonly Designer designer;
        QRCoder coder;
        QRTemplater templater;
        

        ServiceData serviceData;


        public QRWriter()
        {
            designer = new Designer();
        }


        internal CorrectionLevel CorrectionLevel { get; set; }

        internal CodingType CodingType { get; private set; }

        internal Encoding Encoding { get; set; }

        internal DrawStyle DrawStyle
        {
            get
            {
                return designer.DrawStyle;
            }
            set
            {
                designer.DrawStyle = value;
            }
        }

        internal Style Style
        {
            get
            {
                return designer.CustomStyle;
            }
            set
            {
                designer.CustomStyle = value;
            }
        }

        internal int ModulSize
        {
            get
            {
                return designer.ModulSize;
            }
            set
            {
                designer.ModulSize = value;
            }
        }

        internal override bool TryConvertData<T>(T inputData, out byte[] data)
        {
            data = default;
            if (inputData is string stringData)
                coder = ChooseTextCoder(stringData);
            else if (inputData is byte[] byteData)
            {
                CodingType = CodingType.Byte;
                coder = new ByteCoder(byteData);
            }
            else
            {
                ErrorHandler?.Invoke("Неверный тип данных");
                return false;
            }

            return TryCreateQRCode(out data);
        }

        internal override byte[,] CreateCodeTemplate(byte[] data)
        {
            if(templater == null)
                templater = new QRTemplater(CorrectionLevel, serviceData.Version);
            templater.CorrectionLevel = CorrectionLevel;
            templater.Version = serviceData.Version;
            return templater.CreateQRTemplate(data);
        }

        internal override Bitmap DrawCode(byte[,] codeTemplate)
        {
            return designer.Create(codeTemplate);
        }

        /// <summary>
        /// Method ChooseTextCoder choose coding type to data coding
        /// </summary>
        /// <param name="data">input data in string format</param>
        /// <returns>Returns class which will coding data</returns>
        private QRCoder ChooseTextCoder(string data)
        {
            if (NumericCoder.IsValid(data))
            {
                CodingType = CodingType.Numeric;
                return new NumericCoder(data);
            }
            else if (AlphanumericCoder.IsValid(data))
            {
                CodingType = CodingType.Alphanumeric;
                return new AlphanumericCoder(data);
            }
            else
            {
                CodingType = CodingType.Byte;
                return new ByteCoder(Encoding.GetBytes(data));
            }
        }

        /// <summary>
        /// Method TryCreateQRCode makes attempt conversion data
        /// </summary>
        /// <param name="QRdata">array will contain conversion data</param>
        /// <returns>Returns true if attempt was success and false if not</returns>
        private bool TryCreateQRCode(out byte[] QRdata)
        {
            QRdata = default;

            byte[][] dataBlocks;
            byte[][] correctionBlocks;

            if(serviceData == null)
                serviceData = new ServiceData(CodingType, CorrectionLevel);
            serviceData.CodingType = CodingType;
            serviceData.CorrectionLevel = CorrectionLevel;

            if (coder.TryEncoding(out byte[] binaryData))
            {
                if (serviceData.TryGetData(binaryData.Length, coder.DataLength, out byte[] binaryServiceData))
                {
                    binaryData = DataCombiner.FillingData(serviceData.Capacity, binaryServiceData, binaryData);
                    dataBlocks = DataDivider.DivideByBlocks(binaryData, CorrectionLevel, serviceData.Version);
                    correctionBlocks = Correction.GetCorrectionBlocks(dataBlocks, CorrectionLevel, serviceData.Version);
                    QRdata = DataCombiner.MergeBlocks(dataBlocks, correctionBlocks);
                    return true;
                }
                ErrorHandler?.Invoke("Превышен лимит данных! \r\nУменьшите кол-во данных или используйте другой уровень коррекции");
                return false;
            }
            ErrorHandler?.Invoke("Данные невозможно закодировать! \r\nСтрока данных содержит недопустимые символы или данные отсутствуют.");
            return false;
        }
    }
}
