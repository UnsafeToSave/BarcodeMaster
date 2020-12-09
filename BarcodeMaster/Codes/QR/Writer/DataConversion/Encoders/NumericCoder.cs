using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion.Encoders
{
    /// <summary>
    /// DigitalEncoder is class that encoding input data, consisting only numbers, to bits sequence
    /// </summary>
    class NumericCoder : QRCoder
    {
        string digits;
        int dataLength;

        public NumericCoder(string data)
        {
            digits = data;
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
        /// <returns>Return true if input data can be encoded, and false if not</returns>
        internal override bool TryEncoding(out byte[] result)
        {
            string stringBinary;
            int[] numbers;
            
            if (!IsValid(digits))
            {
                result = default;
                return false;
            }

            dataLength = digits.Length;

            numbers = DataSplit(digits);

            stringBinary = ToBinary(numbers);
            if (dataLength == 2 || dataLength == 3)
                stringBinary += "00000000";

            //Converting data from string to array of bytes
            result = new byte[stringBinary.Length];
            for (int i = 0; i < stringBinary.Length; i++)
            {
                result[i] = (byte)stringBinary[i].CompareTo('0');
            }
            return true;
        }

        /// <summary>
        /// Method IsValid checking input data on valid
        /// </summary>
        /// <param name="digits">input data</param>
        /// <returns>Return true if data consists only numbers</returns>
        internal static bool IsValid(string digits)
        {
            if (digits.Length == 0)
            {
                return false;
            }
            if(!Regex.IsMatch(digits, @"^([0-9]+)$"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method DataSplit splitting data on groups
        /// </summary>
        /// <param name="digits">input data</param>
        /// <returns>array consisting data splited on groups</returns>
        private int[] DataSplit(string digits)
        {
            int length = (int)Math.Ceiling(digits.Length / 3.0M);
            int[] numbers = new int[length];
            StringBuilder num = new StringBuilder();

            for (int i = 0; i < numbers.Length; i++)
            {
                num.Clear();
                for (int j = 0; j < 3; j++)
                {
                    if (i * 3 + j >= digits.Length)
                        break;
                    num.Append(digits[i * 3 + j]);
                }
                numbers[i] = int.Parse(num.ToString());
            }

            return numbers;
        }

        /// <summary>
        /// Method ToBinary converting data splited on groups to sequence of bits
        /// </summary>
        /// <param name="numbers">splited data on groups</param>
        /// <returns>Return string consists data converted to sequence of bits </returns>
        private string ToBinary(int[] numbers)
        {
            StringBuilder stringBinary = new StringBuilder();
            string binary;
            foreach (var number in numbers)
            {
                binary = Convert.ToString(number, 2);
                switch (number.ToString().Length)
                {
                    case 3:
                        stringBinary.Append(binary.Insert(0, new string('0', 10 - binary.Length)));
                        break;
                    case 2:
                        stringBinary.Append(binary.Insert(0, new string('0', 7 - binary.Length)));
                        break;
                    case 1:
                        stringBinary.Append(binary.Insert(0, new string('0', 4 - binary.Length)));
                        break;
                }

            }
            return stringBinary.ToString();
        }
    }
}
