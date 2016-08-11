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

        static public void ShowCowfiles(string directory, bool list)
        {
            cowFilesDirectory = $"{directory}\\{cowsFolder}";

            if(!validateDirectory.validate(cowFilesDirectory))
            {
                throw new ArgumentException("Cow Files Path is not valid or not accessible", cowFilesDirectory);
            }

            string[] cowfiles = Directory.GetFiles(cowFilesDirectory, cowSearchPattern);

            Console.WriteLine($"Cow files in {cowFilesDirectory}:");

            if (list)
                listInList(cowfiles);
            else if (!list)
                listInBunch(cowfiles);
        }

        static private void listInList(string[] cowfiles)
        {
            foreach (string file in cowfiles)
            {
                string fileName = Path.GetFileName(file);
                Console.WriteLine($"    {fileName.Remove(fileName.IndexOf(".cow"),4)}");
            }
        }

        static private void listInBunch(string[] cowfiles)
        {
            StringBuilder bunchBuilder = new StringBuilder();
            foreach (string file in cowfiles)
            {
                string fileName = Path.GetFileName(file);
                bunchBuilder.Append($"{fileName.Remove(fileName.IndexOf(".cow"), 4)} ");
            }

            string bunch = bunchBuilder.ToString();

            Console.WriteLine(bunch.Trim());
        }

    }
}
