using PEFileParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEParserTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parsing...");

            PEFileParserTool tool = new PEFileParserTool();
            string path = @"D:\Github Repo\ILQuickPeek\ILQuickPeek\HelloWorld\bin\Debug\HelloWorld.exe";
            PEFile testFile = tool.ParsePEFile(path);

            DateTime fileDateTime = testFile.PEFileHeader.Timestamp;

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
