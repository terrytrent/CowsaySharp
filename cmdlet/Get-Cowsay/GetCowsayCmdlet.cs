using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using CowsaySharp.GetCowsay.Containers;
using CowsaySharp.Library;
using CowsaySharp.ConsoleLibrary;
using CowsaySharp.Common;

//https://www.simple-talk.com/dotnet/net-development/using-c-to-create-powershell-cmdlets-the-basics/

namespace CowsaySharp.GetCowsay
{
    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(Cowsay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        [Parameter]
        [ValidateSet("borg","dead","greedy","paranoid","stoned","tired","wired","young")]
        [Alias("m")]
        public string mode { get; set; }

        [Parameter]
        [Alias("l")]
        public SwitchParameter list { get; set; }

        [Parameter]
        [Alias("n")]
        public SwitchParameter figlet { get; set; }

        [Parameter]
        [Alias("e")]
        public string eyes { get; set; }

        [Parameter]
        [Alias("T")]
        public string tongue { get; set; }

        [Parameter]
        [Alias("f")]
        public string cowfile { get; set; }

        [Parameter]
        [Alias("W")]
        private int _wrapcolumn = 40;
        public int wrapcolumn
        {
            get
            {
                return _wrapcolumn;
            }
            set
            {
                _wrapcolumn = value;
            }
        }
        [Parameter]
        public SwitchParameter think { get; set; }

        [Parameter(ValueFromPipeline = true,ValueFromRemainingArguments = true)]
        public string message { get; set; }

        string moduleDirectory;
        string cowFileLocation;
        string cowSpecified;
        bool breakOut;
        CowFace face;

        protected override void BeginProcessing()
        {
            moduleDirectory = $"C:\\Users\\ttrent\\Source\\Repos\\CowsaySharp\\cmdlet\\Get-Cowsay\\bin\\Debug";
            cowFileLocation = $"C:\\Users\\ttrent\\Source\\Repos\\CowsaySharp\\cmdlet\\Get-Cowsay\\bin\\Debug\\cows";
            cowSpecified = $"{cowFileLocation}\\default.cow";
            face = new CowFace();

            if (!String.IsNullOrEmpty(mode))
                switch (mode)
                {
                    case "borg":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.borg);
                        break;
                    case "dead":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.dead);
                        break;
                    case "greedy":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.greedy);
                        break;
                    case "paranoid":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.paranoid);
                        break;
                    case "stoned":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.stoned);
                        break;
                    case "tired":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.tired);
                        break;
                    case "wired":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.wired);
                        break;
                    case "young":
                        face = CowFaces.getCowFace(CowFaces.cowFaces.young);
                        break;
                }
            else if(!String.IsNullOrEmpty(eyes))
                face = new CowFace(eyes);

            if (String.IsNullOrEmpty(face.Eyes) && String.IsNullOrEmpty(face.Tongue))
                face = CowFaces.getCowFace(CowFaces.cowFaces.defaultFace);

            if (!String.IsNullOrEmpty(tongue) && String.IsNullOrWhiteSpace(face.Tongue))
                face.Tongue = tongue;

            if(!String.IsNullOrEmpty(cowfile))
            {
                cowSpecified = cowfile;
                TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);
                breakOut = testCowFile.breakOut;
            }
            else
            {
                TestCowFile testCowFile = new TestCowFile(ref cowSpecified, cowFileLocation);
                breakOut = testCowFile.breakOut;
            }


        }

        protected override void ProcessRecord()
        {
            if (list)
            {
                Console.WriteLine();
                ListCowfiles.ShowCowfiles(moduleDirectory, list: true);
                Console.WriteLine();
                breakOut = true;
            }
            else if (String.IsNullOrWhiteSpace(message))
                Console.WriteLine();
            else if (!breakOut)
                WriteObject(BuildCowsay());
            
           
        }

        private Cowsay BuildCowsay()
        {
            string SpeechBubbleReturned = SpeechBubble.ReturnSpeechBubble(message, think, wrapcolumn, figlet);
            string CowReturned = GetCow.ReturnCow(cowSpecified, think, face);
            return new Cowsay(CowReturned, SpeechBubbleReturned);
        }
    }
}

