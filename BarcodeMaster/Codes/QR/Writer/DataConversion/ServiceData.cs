using System;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion
{
    /// <summary>
    /// ServiceData is class to combine service data to bits sequenc
    /// </summary>
    class ServiceData
    {
        #region Capacities
        static int[] capacitiesL = { 152, 272, 440, 640, 864, 1088, 1248, 1552, 1856, 2192, 2592, 2960, 3424, 3688, 4184, 4712, 5176, 5768, 6360, 6888, 7456, 8048, 8752, 9392, 10208, 10960, 11744, 12248, 13048, 13880, 14744, 15640, 16568, 17528, 18448, 19472, 20528, 21616, 22496, 23648 };
        static int[] capacitiesM = { 128, 224, 352, 512, 688, 864, 992, 1232, 1456, 1728, 2032, 2320, 2672, 2920, 3320, 3624, 4056, 4504, 5016, 5352, 5712, 6256, 6880, 7312, 8000, 8496, 9024, 9544, 10136, 10984, 11640, 12328, 13048, 13800, 14496, 15312, 15936, 16816, 17728, 18672 };
        static int[] capacitiesQ = { 104, 176, 272, 384, 496, 608, 704, 880, 1056, 1232, 1440, 1648, 1952, 2088, 2360, 2600, 2936, 3176, 3560, 3880, 4096, 4544, 4912, 5312, 5744, 6032, 6464, 6968, 7288, 7880, 8264, 8920, 9368, 9848, 10288, 10832, 11408, 12016, 12656, 13328 };
        static int[] capacitiesH = { 72, 128, 208, 288, 368, 480, 528, 688, 800, 976, 1120, 1264, 1440, 1576, 1784, 2024, 2264, 2504, 2728, 3080, 3248, 3536, 3712, 4112, 4304, 4768, 5024, 5288, 5608, 5960, 6344, 6760, 7208, 7688, 7888, 8432, 8768, 9136, 9776, 10208 };
        #endregion

        public ServiceData(CodingType type, CorrectionLevel level)
        {
            CorrectionLevel = level;
            CodingType = type;
        }
        
        internal CodingType CodingType { get; set; }
        internal CorrectionLevel CorrectionLevel { get; set; }
        internal int Version { get; private set; }
        internal int Capacity { get; private set; }

        /// <summary>
        /// Method TryGetData generates service data 
        /// </summary>
        /// <param name="bitCount"> bits count of input data in binary format</param>
        /// <param name="dataLength"> input data length </param>
        /// <param name="serviceData">service data in binary format</param>
        /// <returns></returns>
        internal bool TryGetData(int bitCount, int dataLength, out byte[] serviceData)
        {
            string binaryString = "";

            if (!TryGetVersion(bitCount))
            {
                Version = default;
                Capacity = default;
                serviceData = default;
                return false;
            }
            binaryString += ConvertToBinary((int)CodingType, 4);
            int capacityLength = GetCapacityFieldLength(CodingType);
            binaryString += ConvertToBinary(dataLength, capacityLength);

            if (bitCount + binaryString.Length > Capacity)
                return TryGetData(bitCount + binaryString.Length, dataLength, out serviceData);

            serviceData = new byte[binaryString.Length];
            for (int i = 0; i < binaryString.Length; i++)
            {
                serviceData[i] = (byte)binaryString[i].CompareTo('0');
            }
            return true;
        }

        /// <summary>
        /// Method TryGetVersion sets values Version and Capacties
        /// </summary>
        /// <param name="dataSize">data size in binary format</param>
        /// <returns>Return true if value of data size lower then max value from capacities array and false if not</returns>
        private bool TryGetVersion(int dataSize)
        {
            int[] capacities = GetCapacities(CorrectionLevel);
            for (int i = 0; i < capacities.Length; i++)
            {
                if (capacities[i] > dataSize)
                {
                    Version = i + 1;
                    Capacity = capacities[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method GetCapacities returns array of capacities by correction level
        /// </summary>
        /// <returns>Return array of capacities</returns>
        private int[] GetCapacities(CorrectionLevel correctionLevel)
        {
            switch (correctionLevel)
            {
                case CorrectionLevel.L:
                    return capacitiesL;
                case CorrectionLevel.M:
                    return capacitiesM;
                case CorrectionLevel.Q:
                    return capacitiesQ;
                case CorrectionLevel.H:
                    return capacitiesH;
                default:
                    return default;
            }
        }

        /// <summary>
        /// Method ConvertToBinary converts integer to binary
        /// </summary>
        /// <param name="value"></param>
        /// <param name="countBits"> count bits</param>
        /// <returns>Return converted value as binay string</returns>
        private string ConvertToBinary(int value, int countBits)
        {
            string binaryString;
            binaryString = Convert.ToString(value, 2);
            binaryString = binaryString.Insert(0, new string('0', countBits - binaryString.Length));
            return binaryString;
        }

        /// <summary>
        /// Method GetCapacityFieldLength returns length by type of coding and version
        /// </summary>
        /// <returns>Return length of capacity field</returns>
        private int GetCapacityFieldLength(CodingType codingType)
        {
            switch (codingType)
            {
                case CodingType.Numeric:
                    if (Version <= 9)
                        return 10;
                    if (Version <= 26)
                        return 12;
                    if (Version <= 40)
                        return 14;
                    break;
                case CodingType.Alphanumeric:
                    if (Version <= 9)
                        return 9;
                    if (Version <= 26)
                        return 11;
                    if (Version <= 40)
                        return 13;
                    break;
                case CodingType.Byte:
                    if (Version <= 9)
                        return 8;
                    if (Version <= 26)
                        return 16;
                    if (Version <= 40)
                        return 16;
                    break;
            }
            return 0;
        }
    }
}
