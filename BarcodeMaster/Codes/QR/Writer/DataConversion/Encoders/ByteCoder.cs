using System;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion.Encoders
{
    /// <summary>
    /// ByteEncoder is class that encoding input data, to bits sequence
    /// </summary>
    class ByteCoder : QRCoder
    {
        byte[] data;
        int dataLength;

        public ByteCoder(byte[] data)
        {
            this.data = data;
        }

        internal override int DataLength
        {
            get
            {
                return dataLength;
            }
        }

        /// <summary>
        /// Method TryEncoding encoding input data to bits sequence
        /// </summary>
        /// <param name="result"> array consisting sequence of bits of encoded data </param>
        /// <returns>Return true if input data not empty, and false if not </returns>
        internal override bool TryEncoding(out byte[] result)
        {
            if (!IsValid(data))
            {
                result = default;
                return false;
            }

            //Converting data from string to array of bytes
            result = DataCombiner.ConvertByteToBinary(data);
            dataLength = result.Length / 8;
            return true;
        }

        /// <summary>
        /// Method IsValid checking input data on valid
        /// </summary>
        /// <param name="data">input data</param>
        /// <returns>Return true if data not empty</returns>
        private static bool IsValid(byte[] data)
        {
            if (data.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
