using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Exceptions
{
    public class InvalidPESignatureException : Exception
    {
        public InvalidPESignatureException(string message) : base(message) { }
        public InvalidPESignatureException(string message, Exception innerException) : base(message, innerException) { }
    }
}
