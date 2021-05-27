using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask.Blazor.Extensions
{
    public static class HexExtensions
    {
        public static int HexToInt(this string hexString)
        {
            if (hexString.StartsWith("0x"))
                hexString = hexString[2..];

            return int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
