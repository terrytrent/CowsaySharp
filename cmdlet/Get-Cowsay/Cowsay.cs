using System;

namespace CowsaySharp.GetCowsay.Containers
{
    class Cowsay
    {
        public string Speech_Bubble { get; set; }
        public string Cow { get; set; }
        public string SpeechBubbleAndCow { get; set; }

        public Cowsay(string cow, string speechBubble)
        {
            Speech_Bubble = speechBubble;
            Cow = cow;
            SpeechBubbleAndCow = $"{Speech_Bubble}{Environment.NewLine}{Cow}";
        }
    }
}
