using System;
using System.Linq;
using System.Text;
using CowsaySharp.Library;

namespace CowsaySharp.ConsoleLibrary
{
    static public class Switches
    {
        static public void processSwitches(string[] args, string programDir, bool think)
        {
            bool breakOut = false;
            bool cowProcessing = false;
            bool cowFileTested = false;
            bool presetFaceSet = false;
            bool isFiglet = false;

            string cowFileLocation = $"{programDir}\\cows";
            string cowSpecified = $"{cowFileLocation}\\default.cow";
            string argument;
            string messageAsString;

            StringBuilder message = new StringBuilder();

            CowFace face = CowFaces.GetCowFace(CowFaces.cowFaces.defaultFace);

            int numberOfArguments = args.Length;
            int columnSize = 40;

            for (int i = 0; i < numberOfArguments; i++)
            {
                argument = args[i];
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
                        case 'n':
                            if (!cowProcessing)
                                cowProcessing = true;

                            isFiglet = true;

                            break;
                        case 'b':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.borg);

                            break;
                        case 'd':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.dead);

                            break;
                        case 'g':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.greedy);

                            break;
                        case 'p':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.paranoid);

                            break;
                        case 's':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.stoned);

                            break;
                        case 't':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.tired);

                            break;
                        case 'w':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.wired);

                            break;
                        case 'y':
                            if (!cowProcessing)
                                cowProcessing = true;

                            if (!presetFaceSet)
                                presetFaceSet = true;

                            face = CowFaces.GetCowFace(CowFaces.cowFaces.young);

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

                            TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);

                            breakOut = testCowFile.breakOut;
                            cowProcessing = testCowFile.cowProcessing;
                            cowFileTested = true;

                            i++;
                            break;
                        case 'h':
                            if (!cowProcessing)
                            {
                                Help.DisplayHelp();
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
                            if (!cowProcessing)
                                cowProcessing = true;

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
                if (!cowFileTested)
                {
                    TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);
                    breakOut = testCowFile.breakOut;
                }

                if (!breakOut)
                {
                    if (isFiglet)
                        messageAsString = message.ToString();
                    else
                        messageAsString = message.ToString().Trim();

                    string SpeechBubbleReturned = SpeechBubble.ReturnSpeechBubble(messageAsString, think, columnSize, isFiglet);
                    string CowReturned = GetCow.ReturnCow(cowSpecified, think, face);

                    Console.WriteLine(SpeechBubbleReturned + Environment.NewLine + CowReturned);
                }
            }

        }



    }
}
