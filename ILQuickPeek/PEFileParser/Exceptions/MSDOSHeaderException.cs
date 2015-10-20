using System;

namespace PEFileParser.Exceptions
{
    public class MSDOSHeaderException : Exception
    {
        public MSDOSHeaderException(string message) : base(message) { }
        public MSDOSHeaderException(string message, Exception innerException) : base(message, innerException) { }
    }
}
