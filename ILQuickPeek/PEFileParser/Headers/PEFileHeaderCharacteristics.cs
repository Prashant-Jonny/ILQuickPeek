using PEFileParser.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser.Headers
{
    public class PEFileHeaderCharacteristics
    {
        private BitArray _valueBits;

        public bool IMAGE_FILE_DLL
        {
            get
            {
                return _valueBits[13];
            }
        }

        public bool IsDllFile
        {
            get
            {
                return IMAGE_FILE_DLL;
            }
        }

        public PEFileHeaderCharacteristics()
        {
            _valueBits = new BitArray(16);
        }

        public PEFileHeaderCharacteristics(byte[] headerValue)
        {
            if (headerValue.Length > 2)
            {
                throw new PEHeaderException("The PE File header Characteristics is too long.");
            }
            if (headerValue.Length < 2)
            {
                throw new PEHeaderException("The PE File header Characteristics is too short.");
            }

            //ValidateBytes(headerValue);
            _valueBits = new BitArray(headerValue);
        }

        //private void ValidateBytes(byte[] headerValue)
        //{
        //    BitArray testBitArray = new BitArray(headerValue.ToArray());

        //    for(int i = 0; i < 16; i++)
        //    {
        //        if((i > 0 && i <= 4) ||  i == 5 || i == 8)
        //        {
        //            if(testBitArray[i] != true)
        //            {
        //                throw new PEHeaderException("The PE File header Characteristics is false where is should be true.");
        //            }
        //        }
        //        else
        //        {
        //            if (testBitArray[i] != false)
        //            {
        //                throw new PEHeaderException("The PE File header Characteristics is true where is should be false.");
        //            }
        //        }
        //    }
        //}
    }
}
