using System;
using System.IO;
using CowsaySharp.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsaySharp.ConsoleLibrary
{
    public class TestCowFile
    {
        public bool breakOut;
        public bool cowProcessing;

        public TestCowFile(ref string cowSpecified, string cowFileLocation)
        {
            string directory;

            if (cowSpecified.Contains("\\"))
            {
                string cowFile = cowSpecified.Substring(cowSpecified.LastIndexOf('\\') + 1);

                if (cowSpecified.Substring(0, 1) == "\\")
                {
                    directory = $"{Directory.GetCurrentDirectory()}{cowSpecified.Substring(0, cowSpecified.IndexOf(cowFile))}";
                    cowSpecified = $"{directory}{cowFile}";
                }
                else
                    directory = cowSpecified.Substring(0, cowSpecified.LastIndexOf('\\'));

                if (!ValidateDirectory.validate(directory))
                {
                    Console.WriteLine($"The directory you specified is either invalid or cannot be accessed:\n{directory}");
                    breakOut = true;
                    cowProcessing = false;
                }

                if (cowFile.Length == 0)
                {
                    Console.WriteLine($"You specified a directory but did not specify a Cow File.");
                    breakOut = true;
                    cowProcessing = false;
                }
                else if (!cowFile.EndsWith(".cow"))
                {
                    cowSpecified += ".cow";
                }
            }
            else
            {
                if (!cowSpecified.EndsWith(".cow"))
                    cowSpecified = $"{cowFileLocation}\\{cowSpecified}.cow";
                else
                    cowSpecified = $"{cowFileLocation}\\{cowSpecified}";
            }
            if (!breakOut && !ValidateFile.validate(cowSpecified))
            {
                Console.WriteLine($"The Cow File you specified does not exist or cannot be accessed:\n{cowSpecified}");
                breakOut = true;
                cowProcessing = false;
            }
        }
    }
}
