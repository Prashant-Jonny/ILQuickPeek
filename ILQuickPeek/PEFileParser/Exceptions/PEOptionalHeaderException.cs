using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Exceptions
{
    public class PEOptionalHeaderException : Exception
    {
        public PEOptionalHeaderException(string message) : base(message) { }
        public PEOptionalHeaderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
