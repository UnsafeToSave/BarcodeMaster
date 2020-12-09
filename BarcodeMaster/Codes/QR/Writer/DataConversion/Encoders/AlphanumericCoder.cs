using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeMaster.Codes.QR.Writer.DataConversion.Encoders
{
    /// <summary>
    /// AlphanumericEncoder is class that encoding input data, consisting only chars of special dictionary, to bits sequence
    /// </summary>
    class AlphanumericCoder : QRCoder
    {
        static Dictionary<char, int> encoderDictionary = new Dictionary<char, int>();
        string text;
        int dataLength;

        static AlphanumericCoder()
        {
            //Generating special dictionary
            for (int i = 0; i <= 9; i++)
            {
                encoderDictionary.Add((char)('0' + i), i);
            }
            for (int i = 0; i <= 25; i++)
            {
                encoderDictionary.Add((char)('A' + i), 10 + i);
            }
            encoderDictionary.Add(' ', 36);
            encoderDictionary.Add('$', 37);
            encoderDictionary.Add('%', 38);
            encoderDictionary.Add('*', 39);
            encoderDictionary.Add('+', 40);
            encoderDictionary.Add('-', 41);
            encoderDictionary.Add('.', 42);
            encoderDictionary.Add('/', 43);
            encoderDictionary.Add(':', 44);
        }

        public AlphanumericCoder(string data)
        {
            text = data;
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
        /// <param name="result"> array consisting sequence of bits of encoded data</param>
        /// <returns>Return true if input data can be encoded, and false if not</returns>
        internal override bool TryEncoding(out byte[] result)
        {
            string stringBinary;
            int[] numbers;
            text = text.ToUpper();
            if (!IsValid(text))
            {
                result = default;
                return false;
            }

            dataLength = text.Length;
            numbers = DataSplit(text);
            stringBinary = ToBinary(numbers);

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
        /// <param name="text">input data</param>
        /// <returns>Return true if data consists only chars of special dictionary</returns>
        internal static bool IsValid(string text)
        {
            text = text.ToUpper();
            if (text.Length == 0)
            {
                return false;
            }

            for(int i = 0; i < text.Length; i++)
            {
                if (!encoderDictionary.ContainsKey(text[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Method DataSplit splitting data on groups
        /// </summary>
        /// <param name="text">input data</param>
        /// <returns>array consisting data splited on groups</returns>
        private int[] DataSplit(string text)
        {
            int[] numbers;
            text = text.ToUpper();
            int number;
            int length = (int)Math.Ceiling(text.Length / 2.0M);
            numbers = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (i * 2 + 1 < text.Length)
                {
                    number = encoderDictionary[text[i * 2]];
                    numbers[i] = number * 45;

                    number = encoderDictionary[text[i * 2 + 1]];
                    numbers[i] += encoderDictionary[text[i * 2 + 1]];
                }
                else
                {
                    number = encoderDictionary[text[i * 2]];
                    numbers[i] = number;
                }
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
                if (number <= 44)
                    stringBinary.Append(binary.Insert(0, new string('0', 6 - binary.Length)));
                else
                    stringBinary.Append(binary.Insert(0, new string('0', 11 - binary.Length)));
            }

            return stringBinary.ToString();
        }
    }
}
