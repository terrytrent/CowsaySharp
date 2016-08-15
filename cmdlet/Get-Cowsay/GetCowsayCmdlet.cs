using System;
using System.Management.Automation;
using CowsaySharp.GetCowsay.Containers;
using CowsaySharp.Library;
using CowsaySharp.ConsoleLibrary;
using System.IO;
using System.Reflection;

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

        [Parameter(ValueFromPipeline = true,Position = 0)]
        public string message { get; set; }

        private int _wrapcolumn = 40;
        string moduleDirectory;
        string cowFileLocation;
        string cowSpecified;
        bool breakOut;
        CowFace face;

        protected override void BeginProcessing()
        {
            moduleDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); ;
            cowFileLocation = $"{moduleDirectory}\\cows";
            cowSpecified = $"{cowFileLocation}\\default.cow";
            face = new CowFace();

            if (!String.IsNullOrEmpty(mode))
            {
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
            }

            if (!String.IsNullOrEmpty(eyes) && String.IsNullOrWhiteSpace(face.Eyes))
                face = new CowFace(eyes);

            if (String.IsNullOrEmpty(face.Eyes))
                face = CowFaces.getCowFace(CowFaces.cowFaces.defaultFace);

            if (!String.IsNullOrEmpty(tongue) && String.IsNullOrWhiteSpace(face.Tongue))
                face.Tongue = tongue;


            if (!String.IsNullOrEmpty(cowfile))
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

            if (wrapcolumn < 10 | wrapcolumn > 76)
                ThrowTerminatingError(new ErrorRecord(new ArgumentOutOfRangeException(nameof(wrapcolumn), "Cannot specify a size smaller than 10 characters or larger than 76 characters"), "E1", ErrorCategory.LimitsExceeded, this));
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

