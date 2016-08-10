using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    class Cow
    {
        private string eyes;
        private string tongue;
        private string cowAscii;

        public string CowAscii
        {
            get { return cowAscii; }
            set { cowAscii = value; }
        }


        public string Eyes
        {
            get { return eyes; }
            set
            {
                if (value != null)
                {
                    int eyesLength = value.Length;
                    if (eyesLength != 2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(Eyes), "Must be exactly 2 characters in length");
                    }
                }
                eyes = value;
            }
        }

        public string Tongue
        {
            get { return tongue; }
            set
            {
                if (value != null)
                {
                    int tongueLength = value.Length;
                    if (tongueLength > 2)
                    {
                        throw new ArgumentOutOfRangeException(nameof(Eyes), "Must be 1 or 2 characters in length");
                    }

                    if (tongueLength != 2)
                    {
                        tongue = $" {value}";
                    }

                    if (tongueLength == 2)
                    {
                        tongue = value;
                    }
                }
            }
        }

        public Cow() : this(null, null, false)
        {

        }

        public Cow(string cowEyes, string cowTongue, bool think)
        {
            Eyes = cowEyes;
            Tongue = cowTongue;

            Bubbles bubbles = new Bubbles();
            if (think)
            {
                bubbles.setBubbles(Bubbles.bubbleType.think);
            }
            else
            {
                bubbles.setBubbles(Bubbles.bubbleType.say);
            }

            if (Eyes == null)
                Eyes = "oo";
            if (Tongue == null)
                Tongue = "  ";
                

            CowAscii = $@"         {bubbles.LargeBubble}   ^__^ 
          {bubbles.SmallBubble}  ({eyes})\_______
             (__)\       )\/\
              {Tongue} ||----w |
                 ||     ||";
            Console.WriteLine(CowAscii);
        }

        public void showCow()
        {
            Console.WriteLine(CowAscii);
        }
    }
}
