using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CowsayLibrary;

namespace CowsayConsole
{
    class switches
    {
        enum cowsaySwitch
        {
            b=0,
            d=1,
            g=2,
            p=3,
            s=4,
            t=5,
            w=6,
            y=7,
            h=8,
            e=9,
            f=10,
            l=11,
            //n,
            T=12,
            W=13
        }

        int columnSize = 40;
        string presetIsAlreadySetError = "You can only specify one set of Eyes and/or Tongue.";
        bool presetIsSet = false;
        string message;


        public switches(string[] args,bool think)
        {
            bool breakOut = false;
            string eyes;
            string tongue;

            for(int i = 0;i < args.Count();i++)
            {
                switch(args[i])
                {
                    case "-b":
                        breakOut = presetSwitch("-b", presetIsSet);
                        break;
                    case "-d":
                        breakOut = presetSwitch("-d", presetIsSet);
                        break;
                    case "-g":
                        breakOut = presetSwitch("-g", presetIsSet);
                        break;
                    case "-p":
                        breakOut = presetSwitch("-p", presetIsSet);
                        break;
                    case "-s":
                        breakOut = presetSwitch("-s", presetIsSet);
                        break;
                    case "-t":
                        breakOut = presetSwitch("-t", presetIsSet);
                        break;
                    case "-w":
                        breakOut = presetSwitch("-w", presetIsSet);
                        break;
                    case "-y":
                        breakOut = presetSwitch("-y", presetIsSet);
                        break;
                    case "-h":
                        help.DisplayHelp();
                        breakOut = true;
                        break;
                    case "-e":
                        i++;
                        eyes = args[i];
                        break;
                    case "-f":

                        break;
                    case "-l":
                        ListCowfiles.ShowCowfiles("dir");
                        breakOut = true;
                        break;
                    case "-T":
                        i++;
                        tongue = args[i];
                        break;
                    case "-W":
                        i++;
                        columnSize = int.Parse(args[i]);
                        break;
                    default:
                        message = args[i];
                        break;
                }
                if (breakOut)
                    break;
            }

        }

        private bool presetSwitch(string cowsaySwitch, bool presetIsSet)
        {
            if (!presetIsSet)
            {
                SpeechBubble speak = new SpeechBubble(message, false,columnSize);
                presetIsSet = true;
                return false;
            }
            else
            {
                Console.WriteLine(presetIsAlreadySetError);
                return true;
            }
        }
    }
}
