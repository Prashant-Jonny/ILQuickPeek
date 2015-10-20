using PEFileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Headers
{
    public class PEFileHeader : HeaderBase
    {
        public byte[] Machine
        {
            get
            {
                return GetArraySlice(0, 2);
            }
        }

        public UInt16 NumberOfSections
        {
            get
            {
                return BitConverter.ToUInt16(GetArraySlice(2, 2), 0);
            }
        }

        public DateTime Timestamp
        {
            get
            {
                UInt32 seconds = BitConverter.ToUInt32(GetArraySlice(4, 4), 0);
                DateTime startTime = new DateTime(1970, 1, 1);
                return startTime.AddSeconds(seconds);
            }
        }

        public UInt32 PointerToSymbolTable
        {
            get
            {
                return BitConverter.ToUInt32(GetArraySlice(8, 4), 0);
            }
        }

        public UInt32 NumberOfSymbols
        {
            get
            {
                return BitConverter.ToUInt32(GetArraySlice(12, 4), 0);
            }
        }

        public UInt16 OptionalHeaderSize
        {
            get
            {
                return BitConverter.ToUInt16(GetArraySlice(16, 2), 0);
            }
        }

        private PEFileHeaderCharacteristics _characteristics;
        public PEFileHeaderCharacteristics Characteristics
        {
            get
            {
                return _characteristics;
            }
        }

        public PEFileHeader()
        {
            _headerValue = new byte[20];
        }

        public PEFileHeader(byte[] headerValue)
        {
            if (headerValue.Length > 20)
            {
                throw new PEHeaderException("The PE File header is too long.");
            }
            if (headerValue.Length < 20)
            {
                throw new PEHeaderException("The PE File header is too short.");
            }

            ValidateBytes(headerValue);

            _characteristics = new PEFileHeaderCharacteristics(GetArraySlice(headerValue, 18, 2));

            _headerValue = headerValue;
        }

        private void ValidateBytes(byte[] headerValue)
        {
            byte[] machineBytes = GetArraySlice(headerValue, 0, 2);
            if (machineBytes[0] !=  0x4C || machineBytes[1] != 0x01 ) //magic value from ECMA-335
            {
                throw new PEHeaderException("The Machine value does not match 0x14c.");
            }

            if(BitConverter.ToUInt32(GetArraySlice(headerValue, 8, 4), 0) != 0)
            {
                throw new PEHeaderException("The Pointer to Symbol Table value does not match 0x0.");
            }

            if (BitConverter.ToUInt32(GetArraySlice(headerValue, 12, 4), 0) != 0)
            {
                throw new PEHeaderException("The Number of Symbols value does not match 0x0.");
            }
        }
    }
}
