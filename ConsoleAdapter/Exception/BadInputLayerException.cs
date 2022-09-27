using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdapter.Exceptions
{
    public class WrongInputException : Exception
    {
            public WrongInputException(string message) : base(message)
            {
            }
    }
}
