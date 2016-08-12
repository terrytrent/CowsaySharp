using System;
using System.Text;
using System.IO;
using CowsaySharp.Common;

namespace CowsaySharp.ConsoleLibrary
{
    static class ListCowfiles
    {
        const string cowSearchPattern = "*.cow";
        const string cowsFolder = "cows";
        static private string cowFilesDirectory { get; set; }

        static public void ShowCowfiles(string directory, bool list)
        {
            cowFilesDirectory = $"{directory}\\{cowsFolder}";

            if(!ValidateDirectory.validate(cowFilesDirectory))
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

        static private void listInColumns(string[] cowfiles)
        {
            for (int i = 0; i < cowfiles.Length; i++)
            {
                string firstColumn;
                string secondColumn;
                string thirdColumn;

                if(!String.IsNullOrEmpty(cowfiles[i]))
                    firstColumn = cowfiles[i];
                if (!String.IsNullOrEmpty(cowfiles[i]))
                    secondColumn = cowfiles[i+1];
                if (!String.IsNullOrEmpty(cowfiles[i]))
                    thirdColumn = cowfiles[i+2];
                string columns = $"{cowfiles[i],-10}{cowfiles[i + 1],-10}{cowfiles[i + 2],-10}";
                i = i + 3;
                Console.WriteLine(columns);
            }
        }
    }
}
