using PEFileParser.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser
{
    public class PEFile
    {
        internal MSDOSHeader _MSDOS_Header;
        public MSDOSHeader MSDOS_Header
        {
            get { return _MSDOS_Header; }
        }

        internal PEFileHeader _PEFileHeader;
        public PEFileHeader PEFileHeader
        {
            get
            {
                return _PEFileHeader;
            }
        }

        public PEFile()
        {
            _MSDOS_Header = new MSDOSHeader();
            _PEFileHeader = new PEFileHeader();
        }
    }
}
