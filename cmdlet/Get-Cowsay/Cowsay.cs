using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using CowsaySharp.Library;

namespace CowsaySharp.GetCowsay.Containers
{
    class Cowsay
    {
        public SpeechBubble speechBubble { get; set; }
        public string Cow { get; set; }
        public string SpeechBubbleAndCow { get; set; }

        public Cowsay(string cow, string speech)
        {
            speechBubble = new SpeechBubble(speech);

            Cow = "cowFile";
            SpeechBubbleAndCow = $"{speechBubble}{Environment.NewLine}{Cow}";
        }
    }
}
