using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Exceptions
{
    public class PEHeaderException : Exception
    {
        public PEHeaderException(string message) : base(message) { }
        public PEHeaderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
