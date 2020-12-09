namespace BarcodeMaster.Codes.QR.Writer.DataConversion
{

    /// <summary>
    /// DataDivider is class for dividing data by blocks
    /// </summary>
    static class DataDivider
    {
        #region Blocks Count
        static int[] blocksCountL = { 1, 1, 1, 1, 1, 2, 2, 2, 2, 4, 4, 4, 4, 4, 6, 6, 6, 6, 7, 8, 8, 9, 9, 10, 12, 12, 12, 13, 14, 15, 16, 17, 18, 19, 19, 20, 21, 22, 24, 25 };
        static int[] blocksCountM = { 1, 1, 1, 2, 2, 4, 4, 4, 5, 5, 5, 8, 9, 9, 10, 10, 11, 13, 14, 16, 17, 17, 18, 20, 21, 23, 25, 26, 28, 29, 31, 33, 35, 37, 38, 40, 43, 45, 47, 49 };
        static int[] blocksCountQ = { 1, 1, 2, 2, 4, 4, 6, 6, 8, 8, 8, 10, 12, 16, 12, 17, 16, 18, 21, 20, 23, 23, 25, 27, 29, 34, 34, 35, 38, 40, 43, 45, 48, 51, 53, 56, 59, 62, 65, 68 };
        static int[] blocksCountH = { 1, 1, 2, 4, 4, 4, 5, 6, 8, 8, 11, 11, 16, 16, 18, 16, 19, 21, 25, 25, 25, 34, 30, 32, 35, 37, 40, 42, 45, 48, 51, 54, 57, 60, 63, 66, 70, 74, 77, 81 };
        #endregion

        /// <summary>
        /// Method DivideByBlocks dividing data by blocks
        /// </summary>
        /// <param name="data"></param>
        /// <param name="correctionLevel"></param>
        /// <param name="version"></param>
        /// <returns>Returns data divided by blocks</returns>
        internal static byte[][] DivideByBlocks(byte[] data, CorrectionLevel correctionLevel, int version)
        {
            byte[][] dataBlocks;
            int blocksCount = GetBlocksCount(correctionLevel, version);
            dataBlocks = new byte[blocksCount][];
            int[] blocksLengths = GetBlocksLengths(blocksCount, data.Length);
            for (int i = 0; i < dataBlocks.Length; i++)
            {
                dataBlocks[i] = new byte[blocksLengths[i]];
            }
            dataBlocks = DataCombiner.FillingBlocks(data, dataBlocks);
            return dataBlocks;
        }

        /// <summary>
        /// Method GetBlocks Lengths defines lengths for each blocks
        /// </summary>
        /// <param name="blocksCount">count blocks</param>
        /// <param name="dataLength">length whole sequence bits of data</param>
        /// <returns>Rerturns lengths for each blocks</returns>
        private static int[] GetBlocksLengths(int blocksCount, int dataLength)
        {
            dataLength /= 8;
            int[] blocksLengths = new int[blocksCount];
            int blocksLength = dataLength / blocksCount;
            int remainder;
            for (int i = 0; i < blocksLengths.Length; i++)
            {
                blocksLengths[i] = blocksLength;
            }
            if (dataLength % blocksCount != 0)
            {
                remainder = dataLength % blocksCount;
                for (int i = blocksLengths.Length - 1; remainder > 0; i--, remainder--)
                {
                    blocksLengths[i] += 1;
                }
            }
            return blocksLengths;
        }

        /// <summary>
        /// Method GetBlockCount get count of data blocks by correction level
        /// </summary>
        /// <param name="correctionLevel"></param>
        /// <param name="version"></param>
        /// <returns>Return count of data blocks</returns>
        private static int GetBlocksCount(CorrectionLevel correctionLevel, int version)
        {
            switch (correctionLevel)
            {
                case CorrectionLevel.L:
                    return blocksCountL[version - 1];
                case CorrectionLevel.M:
                    return blocksCountM[version - 1];
                case CorrectionLevel.Q:
                    return blocksCountQ[version - 1];
                case CorrectionLevel.H:
                    return blocksCountH[version - 1];
                default:
                    return default;
            }
        }
    }
}
