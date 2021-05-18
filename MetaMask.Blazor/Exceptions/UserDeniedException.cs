using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMask.Blazor.Exceptions
{
    public class UserDeniedException : ApplicationException
    {
        public UserDeniedException() : base("User denied access to MetaMask.")
        {

        }
    }
}
