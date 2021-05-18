using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask.Blazor.Exceptions
{
    public class NoMetaMaskException : ApplicationException
    {
        public NoMetaMaskException() : base("MetaMask is not installed.")
        {

        }
    }
}
