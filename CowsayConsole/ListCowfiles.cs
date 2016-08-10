using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cowsay.Common;

namespace CowsayConsole
{
    static class ListCowfiles
    {
        const string cowSearchPattern = "*.cow";
        const string cowsFolder = "cows";
        static private string cowFilesDirectory { get; set; }

        static public void ShowCowfiles(string directory)
        {
            cowFilesDirectory = $"{directory}\\{cowsFolder}";

            if(!validateDirectory.validate(cowFilesDirectory))
            {
                throw new ArgumentException("Cow Files Path is not valid or not accessible", cowFilesDirectory);
            }

            string[] cowfiles = Directory.GetFiles(cowFilesDirectory, cowSearchPattern);

            Console.WriteLine($"Cow files in {cowFilesDirectory}\r\n");


            foreach (string file in cowfiles)
            {
                string fileName = Path.GetFileName(file);
                Console.WriteLine($"    {fileName}");
            }
        }
    }
}
