using System;
using System.Linq;

namespace PieEatingNinja.ProximityBeaconLibrary.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ReadAsString(this byte[] data, int startindex, int length)
        {
            if (length <= 0)
                throw new ArgumentException($"Invalid {nameof(length)} specified. {nameof(length)} can not be less or equal to 0.");

            return BitConverter.ToString(data.Skip(startindex).Take(length).ToArray());
        }

        public static short ReadAsShort(this byte[] data, int startindex, int length)
        {
            return Parse(data, startindex, length, 2, BitConverter.ToInt16);
        }

        public static ushort ReadAsUShort(this byte[] data, int startindex, int length)
        {
            return Parse(data, startindex, length, 2, BitConverter.ToUInt16);
        }

        private static T Parse<T>(byte[] data, int startindex, int length, int requiredLength, Func<byte[], int, T> function)
        {
            if (length <= 0)
                throw new ArgumentException($"Invalid parameter {nameof(length)} specified. {nameof(length)} can not be less or equal to 0.");
            else if (length > requiredLength)
                throw new ArgumentException($"Invalid parameter {nameof(length)} specified. When parsing to a '{typeof(T).FullName}', the {nameof(length)} can not be larger than {requiredLength}. Try parsing to a larger datatype instead.");
            else if (startindex >= data.Length)
                throw new ArgumentOutOfRangeException($"Parameter {nameof(length)} is not within the range of the byte array.");
            else if (startindex + length > data.Length)
                throw new ArgumentOutOfRangeException($"The combination of parameters {nameof(startindex)} and {nameof(length)} span over the size of the byte array.");

            byte[] arrayToParse = new byte[requiredLength];
            int index = 0;
            for (int i = 0; i < requiredLength; i++)
            {
                if (requiredLength - length > i)
                {
                    arrayToParse[i] = 0;
                }
                else
                {
                    arrayToParse[i] = data.Skip(startindex + index).First();
                    index++;
                }
            }

            if (BitConverter.IsLittleEndian)
                Array.Reverse(arrayToParse);

            return function.Invoke(arrayToParse, 0);
        }
    }
}
