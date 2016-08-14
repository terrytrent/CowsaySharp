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
        public string speechBubble { get; set; }
        public string Cow { get; set; }
        public string SpeechBubbleAndCow { get; set; }

        public Cowsay(string cow, string speech)
        {
            CowFace face = new CowFace("oO", "U");
            speechBubble = SpeechBubble.ReturnSpeechBubble(speech);

            Cow = GetCow.ReturnCow("C:\\Users\\ttrent\\Source\\Repos\\cowsay_chsarp\\cmdlet\\Get-Cowsay\\bin\\Debug\\cows\\default.cow", false, face);
            SpeechBubbleAndCow = $"{speechBubble}{Environment.NewLine}{Cow}";
        }
    }
}
