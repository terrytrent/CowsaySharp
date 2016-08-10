using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cowsay
{
    class ListCowfiles
    {
        const string cowSearchPattern = "*.cow";
        const string cowsFolder = "cows";
        public string cowFilesDirectory { get; private set; }

        public ListCowfiles(string directory)
        {
            cowFilesDirectory = $"{directory}\\{cowsFolder}";

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
