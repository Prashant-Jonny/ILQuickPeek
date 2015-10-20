using PEFileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEFileParser
{
    public class PEFileParserTool
    {
        private byte[] _fileBytes;

        public PEFileParserTool()
        {

        }

        public PEFile ParsePEFile(byte[] fileBytes)
        {
            _fileBytes = fileBytes;
            PEFile returnValue = new PEFile();
            
            ArraySegment<byte> msdosHeaderBytes = new ArraySegment<byte>(fileBytes, 0, 128);
            returnValue._MSDOS_Header = new Headers.MSDOSHeader(msdosHeaderBytes.ToArray());

            int peHeaderOffset = ValidatePESignature(returnValue.MSDOS_Header.lfanew);

            ArraySegment<byte> peFileHeaderBytes = new ArraySegment<byte>(fileBytes, peHeaderOffset, 20); //another shitty type cast
            returnValue._PEFileHeader = new Headers.PEFileHeader(peFileHeaderBytes.ToArray());

            return returnValue;
        }

        private int ValidatePESignature(uint lfanew)
        {
            int lfanew_int = Convert.ToInt32(lfanew);
            byte[] PESig = new ArraySegment<byte>(_fileBytes, lfanew_int, 4).ToArray();

            if ((PESig[0] != ASCIIEncoding.ASCII.GetBytes("P").First()) ||
                (PESig[1] != ASCIIEncoding.ASCII.GetBytes("E").First()) ||
                (PESig[2] != 0) ||
                (PESig[3] != 0))
            {
                throw new InvalidPESignatureException("The PE Signature was invalid.");
            }

            return lfanew_int + 4;
        }

        public PEFile ParsePEFile(string filePath)
        {
            using (FileStream peFileStream = File.Open(filePath, FileMode.Open))
            {
                byte[] fileBytes = new byte[peFileStream.Length];
                peFileStream.Read(fileBytes, 0, Convert.ToInt32(peFileStream.Length)); //Fucking seriously? TO DO: Fix this shit
                return ParsePEFile(fileBytes);
            }
        }
    }
}
