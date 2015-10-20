using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Headers
{
    public class HeaderBase
    {
        protected byte[] _headerValue;

        public byte[] HeaderValue
        {
            get { return _headerValue; }
        }

        protected byte[] GetArraySlice(int offset, int length)
        {
            return new ArraySegment<byte>(_headerValue, offset, length).ToArray(); //magic numbers from ECMA-335 documentation
        }

        protected byte[] GetArraySlice(byte[] source, int offset, int length)
        {
            return new ArraySegment<byte>(source, offset, length).ToArray();
        }
    }
}
