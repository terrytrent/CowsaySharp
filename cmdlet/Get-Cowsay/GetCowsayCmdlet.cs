using System;
using System.Management.Automation;
using CowsaySharp.GetCowsay.Containers;
using CowsaySharp.Library;
using CowsaySharp.ConsoleLibrary;
using System.IO;
using System.Reflection;

namespace CowsaySharp.GetCowsay
{
    /// <summary>
    /// <para type="synopsis">This cmdlet generates ASCII pictures of a cow with a message.</para> 
    /// </summary>
    /// <example>
    ///   <para>This is part of the first example's introduction.</para>
    ///   <code>New-Thingy | Write-Host</code>
    ///   <para>This is part of the first example's remarks.</para>
    ///   <para>This is also part of the first example's remarks.</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "Cowsay")]
    [OutputType(typeof(Cowsay))]
    public class GetCowsayCmdlet : Cmdlet
    {
        /// <summary>
        /// <para type="description">Preset faces to select for the cow.  Will override any eyes or tongue settings set manually.</para>
        /// </summary>
        [Parameter]
        [ValidateSet("borg","dead","greedy","paranoid","stoned","tired","wired","young")]
        [Alias("m")]
        public string mode { get; set; }
        /// <summary>
        /// <para type="description">List available cow files in module cows folder.</para> 
        /// </summary>
        /// 
        [Parameter]
        [Alias("l")]
        public SwitchParameter list { get; set; }

        /// <summary>
        /// <para type="description">Enable figlet mode for your message.  Must escape special characters in order to work properly.</para> 
        /// </summary>
        [Parameter]
        [Alias("n")]
        public SwitchParameter figlet { get; set; }

        /// <summary>
        /// <para type="description">Eyes for the cow, cannot be more than 2 characters long.  If more than 2 characters long, will be cut off.  Overridden by the mode parameter.</para> 
        /// </summary>
        [Parameter]
        [Alias("e")]
        public string eyes { get; set; }

        /// <summary>
        /// <para type="description">Tongue for the cow, cannot be more than 2 characters long.  If more than 2 characters long, will be cut off.  Overridden by the mode parameter. </para> 
        /// </summary>
        [Parameter]
        [Alias("T")]
        public string tongue { get; set; }

        /// <summary>
        /// <para type="description">Specify either a specific cowfile found under the Module cows folder or a specific cowfile in a specific directory.</para>
        /// </summary>
        [Parameter]
        [Alias("f")]
        public string cowfile { get; set; }

        /// <summary>
        /// <para type="description">Change the width of the speech bubble.  Default is 40 characters wide.  Cannot be less than 10 characters or more than 76 characters.</para>
        /// </summary>
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

        /// <summary>
        /// <para type="description">Make the cow think.</para> 
        /// </summary>
        [Parameter]
        public SwitchParameter think { get; set; }

        /// <summary>
        /// <para type="message">What the cow says.</para>
        /// </summary>
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

