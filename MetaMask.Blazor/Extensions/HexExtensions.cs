using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask.Blazor.Extensions
{
    public static class HexExtensions
    {
        public static long HexToInt(this string hexString)
        {
            if (hexString.StartsWith("0x"))
                hexString = hexString[2..];

            return long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }
        public static BigInteger HexToBigInteger(this string hexString)
        {
            if (hexString.StartsWith("0x"))
                hexString = hexString[2..];

            return BigInteger.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
