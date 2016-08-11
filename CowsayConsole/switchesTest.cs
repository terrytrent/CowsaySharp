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
    static public class switchesTest
    {
        static string cowFileLocation;
        static StringBuilder message = new StringBuilder();

        static public void processSwitches(string[] args, string programDir, bool think)
        {
            bool breakOut = false;
            bool cowProcessing = false;
            bool cowFileTested = false;
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

                            TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);

                            breakOut = testCowFile.breakOut;
                            cowProcessing = testCowFile.cowProcessing;
                            cowFileTested = true;

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
                if (!cowFileTested)
                {
                    TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);
                    breakOut = testCowFile.breakOut;
                }

                if (!breakOut)
                {
                    SpeechBubble Speak = new SpeechBubble(message.ToString().Trim(), think, columnSize);
                    GetCow.ReturnCow(cowSpecified, think, face);
                }
            }

        }

        

    }
}
