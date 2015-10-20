using PEFileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Headers
{
    public class PEOptionalHeader : HeaderBase
    {
        public PEOptionalHeader()
        {
            _headerValue = new byte[224];
        }

        public PEOptionalHeader(byte[] headerValue)
        {
            if (headerValue.Length > 128)
            {
                throw new PEOptionalHeaderException("The PE Optional header is too long.");
            }
            if (headerValue.Length < 128)
            {
                throw new PEOptionalHeaderException("The PE Optional header is too short.");
            }
        }
    }
}
