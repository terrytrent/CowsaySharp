using System;
using System.Linq;
using System.Collections.Generic;
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
                listInColumns(cowfiles);
            else if (!list)
                listInBunch(cowfiles);
        }

        static private List<string> listInList(string[] cowfiles)
        {
            List<string> cowList = new List<string>();
            foreach (string file in cowfiles)
            {
                string fileName = Path.GetFileName(file);
                cowList.Add(fileName.Remove(fileName.IndexOf(".cow"), 4));
                //Console.WriteLine($"    {fileName.Remove(fileName.IndexOf(".cow"),4)}");
            }
            return cowList;
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
            List<string> cowFilesList = listInList(cowfiles);
            var columnSize = (short)cowFilesList.Max(s => s.Length) + 2;
            string columnSizeString = columnSize.ToString();
            

            for (int i = 0; i < cowFilesList.Count; i++)
            {
                string firstColumn;
                string secondColumn;
                string thirdColumn;

                if(!String.IsNullOrEmpty(cowFilesList[i]))
                    firstColumn = cowFilesList[i];
                if (!String.IsNullOrEmpty(cowFilesList[i+1]))
                    secondColumn = cowFilesList[i+1];
                if (!String.IsNullOrEmpty(cowFilesList[i+2]))
                    thirdColumn = cowFilesList[i+2];

                string columns = String.Format($"{{0,-{columnSize}}}{{1,-{columnSize}}}{{2,-{columnSize}}}", cowFilesList[i], cowFilesList[i + 1], cowFilesList[i + 2]);

                i = i + 3;
                Console.WriteLine(columns);
            }
        }
    }
}
