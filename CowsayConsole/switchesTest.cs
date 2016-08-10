using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cowsay.Common;
using CowsayLibrary;

namespace CowsayConsole
{
    static class switchesTest
    {
        static string cowFileLocation;
        static string message;

        static public void processSwitches(string[] args, string programDir)
        {
            bool breakOut = false;
            bool cowProcessing = true;
            cowFileLocation = $"{programDir}\\cows";
            string cowFile = $"{cowFileLocation}\\default.cow";
            CowFace face = new CowFace("OO","  ");
            int columnSize = 40;

            int numberOfArguments = args.Length;

            for (int i = 0; i < numberOfArguments; i++)
            {
                switch (args[i])
                {
                    case "-W":
                        columnSize = int.Parse(args[i + 1]);
                        i++;
                        break;
                    case "-e":
                        face.Eyes = args[i + 1];
                        break;
                    case "-T":
                        face.Tongue = args[i + 1];
                        break;
                    case "-f":
                        cowFileLocation = args[i+1];
                        if (!validateDirectory.validate(cowFileLocation))
                        {
                            Console.WriteLine($"The directory you specified is either invalid or cannot be accessed:\n{cowFileLocation}");
                            breakOut = true;
                        }
                        i++;
                        break;
                    case "-h":
                        if(cowProcessing)
                        {
                            help.DisplayHelp();
                            breakOut = true;
                        }
                        break;
                    case "-l":
                        if (cowProcessing)
                        {
                            ListCowfiles.ShowCowfiles(programDir);
                            breakOut = true;
                        }
                        break;
                    default:
                        message = args[i];
                        break;
                }
                if (breakOut)
                    break;

            }
            bool think = false;
            SpeechBubble Speak = new SpeechBubble(message, think, columnSize);
            GetCow.ReturnCow(cowFile, true, face);

        }

    }
}
