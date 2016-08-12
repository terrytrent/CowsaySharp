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
                listInColumnsDown(cowfiles);
            else if (!list)
                listInBunch(cowfiles);
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

        static private void listInList(string[] cowfiles)
        {
            List<string> cowFilesList = StringArrayOfFilesToList.GetList(cowfiles);
            foreach(string file in cowFilesList)
            {
                Console.WriteLine(file);
            }
        }

        static private void listInColumnsAcross(string[] cowfiles)
        {
            List<string> cowFilesList =  StringArrayOfFilesToList.GetList(cowfiles);
            var columnSize = (short)cowFilesList.Max(s => s.Length) + 2;

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

        static private void listInColumnsDown(string[] cowfiles)
        {
            List<string> cowFilesList =  StringArrayOfFilesToList.GetList(cowfiles);
            List<string> returnList = new List<string>();
            StringBuilder fullList = new StringBuilder();
            const int numberOfColumns = 3;
            int columnSize = (short)cowFilesList.Max(s => s.Length) + 2;
            int numberOfFiles = cowFilesList.Count;
            int numberOfLines = ((numberOfFiles - (numberOfFiles % numberOfColumns)) / numberOfColumns) + 1;

            for (int currentIndexOfFile = 0,currentRowOfColulmn = 0,currentColumn = 0; currentColumn < numberOfColumns && currentIndexOfFile < numberOfFiles; currentIndexOfFile++,currentRowOfColulmn++)
{
                StringBuilder sb = new StringBuilder();
                string file = cowFilesList[currentIndexOfFile];
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
