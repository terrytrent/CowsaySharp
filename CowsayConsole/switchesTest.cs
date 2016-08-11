using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cowsay.Common;
using CowsayLibrary;

namespace CowsayConsole
{
    static class switchesTest
    {
        static string cowFileLocation;
        static StringBuilder message = new StringBuilder();

        static public void processSwitches(string[] args, string programDir)
        {
            bool breakOut = false;
            bool cowProcessing = false;
            cowFileLocation = $"{programDir}\\cows";
            string cowSpecified = $"{cowFileLocation}\\default.cow";
            CowFace face = new CowFace("OO",null);
            bool presetFaceSet = false;
            int columnSize = 40;

            int numberOfArguments = args.Length;

            for (int i = 0; i < numberOfArguments; i++)
            {
                string argument = args[i];
                if (args[i].Contains('-'))
                    argument = args[i].Remove(args[i].IndexOf('-'), 1);
                else
                    argument = "!";

                foreach (char arg in argument)
                {
                    switch (arg)
                    {
                        case 'W':
                            if (!cowProcessing)
                                cowProcessing = true;
                            columnSize = int.Parse(args[i + 1]);
                            i++;
                            break;
                        case 'b':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.borg);

                            break;
                        case 'd':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.dead);

                            break;
                        case 'g':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.greedy);

                            break;
                        case 'p':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.paranoid);

                            break;
                        case 's':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.stoned);

                            break;
                        case 't':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.tired);

                            break;
                        case 'w':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.wired);

                            break;
                        case 'y':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.getCowFace(CowFaces.cowFaces.young);

                            break;
                        case 'e':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                face.Eyes = args[i + 1];

                            i++;
                            break;
                        case 'T':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (String.IsNullOrWhiteSpace(face.Tongue))
                            {
                                face.Tongue = args[i + 1];
                            }

                            i++;
                            break;
                        case 'f':
                            if (!cowProcessing)
                                cowProcessing = true;

                            cowSpecified = args[i + 1];

                            specifyCowFile(ref cowSpecified, ref breakOut, ref cowProcessing);

                            i++;
                            break;
                        case 'h':
                            if (!cowProcessing)
                            {
                                help.DisplayHelp();
                                breakOut = true;
                            }
                            break;
                        case 'l':
                            if (!cowProcessing)
                            {
                                ListCowfiles.ShowCowfiles(programDir, list: false);
                                breakOut = true;
                            }
                            break;
                        case 'L':
                            if (!cowProcessing)
                            {
                                ListCowfiles.ShowCowfiles(programDir, list: true);
                                breakOut = true;
                            }
                            break;
                        case '!':
                        default:
                            if (!cowProcessing) cowProcessing = true;

                            for (int j = i; j < numberOfArguments; j++)
                                message.Append(args[j] + " ");

                            i = numberOfArguments;
                            break;
                    }
                    if (breakOut)
                        break;
                }
            }
            if (cowProcessing)
            {
                bool think = false;
                SpeechBubble Speak = new SpeechBubble(message.ToString().Trim(), think, columnSize);
                GetCow.ReturnCow(cowSpecified, think, face);
            }

        }

        static private void specifyCowFile(ref string cowSpecified, ref bool breakOut, ref bool cowProcessing)
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

                if (!validateDirectory.validate(directory))
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

            if (!validateFile.validate(cowSpecified))
            {
                Console.WriteLine($"The Cow File you specified does not exist or cannot be accessed:\n{cowSpecified}");
                breakOut = true;
                cowProcessing = false;
            }
        }

    }
}
