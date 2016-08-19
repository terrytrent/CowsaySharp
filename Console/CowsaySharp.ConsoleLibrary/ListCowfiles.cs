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

            IList<string> cowfiles = Directory.GetFiles(cowFilesDirectory, cowSearchPattern);
            for (int i = 0; i < cowfiles.Count; i++)
            {
                string cowfile = Path.GetFileNameWithoutExtension(cowfiles[i]);
                cowfiles[i] = cowfile;
            }

            Console.WriteLine($"Cow files in {cowFilesDirectory}:");

            if (list)
                listInColumnsDown(cowfiles);
            else if (!list)
                listInBunch(cowfiles);
        }

        static private void listInBunch(IList<string> cowfiles)
        {
            StringBuilder bunchBuilder = new StringBuilder();
            foreach (string file in cowfiles)
            {
                bunchBuilder.Append($"{file} ");
            }

            Console.WriteLine(bunchBuilder.ToString().Trim());
        }
        
        static private void listInColumnsDown(IList<string> cowfiles)
        {
            List<string> returnList = new List<string>();
            StringBuilder fullList = new StringBuilder();
            const int numberOfColumns = 3;
            int columnSize = (short)cowfiles.Max(s => s.Length) + 2;
            int numberOfFiles = cowfiles.Count;
            int numberOfLines = ((numberOfFiles - (numberOfFiles % numberOfColumns)) / numberOfColumns) + 1;

            for (int currentIndexOfFile = 0,currentRowOfColulmn = 0,currentColumn = 0; currentColumn < numberOfColumns && currentIndexOfFile < numberOfFiles; currentIndexOfFile++,currentRowOfColulmn++)
{
                StringBuilder sb = new StringBuilder();
                string file = cowfiles[currentIndexOfFile];
                string toAppend = String.Format($"{{0,-{columnSize}}}", file);

                if (currentColumn == 0)
                {
                    sb.Append(toAppend);
                    returnList.Add(sb.ToString());
                }
                else
                {
                    sb.Append(returnList[currentRowOfColulmn - 1]);
                    sb.Append(toAppend);
                    returnList[currentRowOfColulmn-1] = sb.ToString();
                }

                if (currentRowOfColulmn == numberOfLines - 1 && currentColumn == 0)
                { 
                    currentColumn++;
                    currentRowOfColulmn = 0;
                }
                else if (currentRowOfColulmn == numberOfLines)
                {
                    currentColumn++;
                    currentRowOfColulmn = 0;
                }
            }

            foreach (string item in returnList)
                fullList.AppendLine(item);

            Console.WriteLine(fullList.ToString().Trim());
        }
    }
}
