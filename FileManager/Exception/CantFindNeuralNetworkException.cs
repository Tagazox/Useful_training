using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Exceptions
{
    public class CantFindException : Exception
    {
            public CantFindException(string message) : base(message)
            {
            }
    }
}
