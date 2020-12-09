using System;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion
{
    /// <summary>
    /// DataCombine is class for different way combine data
    /// </summary>
    static class DataCombiner
    {
        #region Additional bytes
        static byte[] byte1 = { 1, 1, 1, 0, 1, 1, 0, 0 };
        static byte[] byte2 = { 0, 0, 0, 1, 0, 0, 0, 1 };
        #endregion

        /// <summary>
        /// Method FillingData fills data capacity with input data, service data, additional bytes
        /// </summary>
        /// <param name="dataCapacity"> data capacity</param>
        /// <param name="arrays">arrays: input data, service data in binary format</param>
        /// <returns>Returns a filled array input data, service data, additional bytes</returns>
        internal static byte[] FillingData(int dataCapacity, byte[] serviceData, byte[] data)
        {
            byte[] finalData = new byte[dataCapacity];
            byte[] concatData = ConcatArrays(serviceData, data);
            int zeroBlockLength = (dataCapacity - concatData.Length) % 8;
            int additionalBytesLength = (dataCapacity - concatData.Length) / 8;
            int start = 0;
            int end = concatData.Length;
            for (int i = start; i < end; i++)
            {
                finalData[i] = concatData[i];
            }
            start = end;
            end += zeroBlockLength;
            for (int i = start; i < end; i++)
            {
                finalData[i] = 0;
            }

            for (int i = 0; i < additionalBytesLength; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        finalData[end] = byte1[j];
                        end++;
                    }
                }
                else
                {
                    for (int j = 0; j < 8; j++)
                    {
                        finalData[end] = byte2[j];
                        end++;
                    }
                }
            }
            return finalData;
        }
        /// <summary>
        /// Method ConcatArrays combines few arrays in one array
        /// </summary>
        /// <param name="arrays">arrays of arrays for concatenation</param>
        /// <returns>Returns concatenated arrays</returns>
        private static byte[] ConcatArrays(params byte[][] arrays)
        {
            byte[] result;
            int length = 0;

            foreach (var array in arrays)
            {
                length += array.Length;
            }
            result = new byte[length];

            for (int i = 0; i < result.Length;)
            {
                for (int arrayIndex = 0; arrayIndex < arrays.Length; arrayIndex++)
                {
                    for (int j = 0; j < arrays[arrayIndex].Length; j++, i++)
                    {
                        result[i] = arrays[arrayIndex][j];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Method FillingBlocks sequentially fills each block with input data
        /// </summary>
        /// <param name="data">data in binary format</param>
        /// <param name="blocks">prepared array of arrays to fill with input data in byte format</param>
        /// <returns>Returns filled blocks of data</returns>
        internal static byte[][] FillingBlocks(byte[] data, byte[][] blocks)
        {
            byte[] byteData = new byte[data.Length / 8];
            string bitString;
            int pointer = 0;
            //Convert 8 bits to byte number
            for(int i = 0; i < byteData.Length; i++)
            {
                bitString = "";
                for(int j = 0; j < 8; j++)
                {
                    bitString += data[i * 8 + j].ToString();
                }
                byteData[i] = Convert.ToByte(bitString, 2);
            }
            //Filling blocks
            for (int idBlock = 0; idBlock < blocks.Length; idBlock++)
            {
                for(int i = 0; i < blocks[idBlock].Length; i++)
                {
                    blocks[idBlock][i] = byteData[pointer];
                    pointer++;
                } 
            }

            return blocks;
        }

        /// <summary>
        /// Method MergeBlocks merges data and correction blocks 
        /// </summary>
        /// <param name="dataBlocks">data blocks</param>
        /// <param name="correctionBlocks">correction blocks</param>
        /// <returns>Retuns merged blocks</returns>
        internal static byte[] MergeBlocks(byte[][] dataBlocks, byte[][] correctionBlocks)
        {
            byte[] mergeBlock = PrepearMergeArray(dataBlocks, correctionBlocks, out int arrayLength);
            int index = 0;
            //Add data from data blocks
            for (int i = 0; i < arrayLength; i++)
            {
                for (int j = 0; j < dataBlocks.Length; j++)
                {
                    if (i >= dataBlocks[j].Length)
                        continue;
                    mergeBlock[index] = dataBlocks[j][i];
                    index++;
                }
            }
            //Add data from correction blocks
            for (int i = 0; i < correctionBlocks[0].Length; i++)
            {
                for (int j = 0; j < correctionBlocks.Length; j++)
                {
                    mergeBlock[index] = correctionBlocks[j][i];
                    index++;
                }
            }

            return ConvertByteToBinary(mergeBlock);
        }

        /// <summary>
        /// Method PrepearMergeArray creates array desiered length
        /// </summary>
        /// <param name="dataBlocks"> data blocks</param>
        /// <param name="correctionBlocks">correction blocks</param>
        /// <param name="dataMaxLength"></param>
        /// <returns>Returns array desired length</returns>
        private static byte[] PrepearMergeArray(byte[][] dataBlocks, byte[][] correctionBlocks, out int mergeArrayLength)
        {
            int length = 0;
            mergeArrayLength = 0;
            for (int i = 0; i < dataBlocks.Length; i++)
            {
                if (dataBlocks[i].Length > mergeArrayLength)
                    mergeArrayLength = dataBlocks[i].Length;
                length += dataBlocks[i].Length + correctionBlocks[i].Length;
            }
            return new byte[length];
        }

        /// <summary>
        /// Method ConvertByteToBinary converts data from byte to binary
        /// </summary>
        /// <param name="data">data in byte form</param>
        /// <returns>Returns data in binary form</returns>
        internal static byte[] ConvertByteToBinary(byte[] data)
        {
            byte[] binaryData = new byte[data.Length * 8];

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    binaryData[i * 8 + j] = (byte)((data[i] >> 7 - j) & 1);
                }
            }
            return binaryData;
        }
    }
}
