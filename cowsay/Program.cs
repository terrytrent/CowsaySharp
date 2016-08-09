using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    class program
    {
        static void Main(string[] args)
         {
            string message = "We begin this tale, with me having left [Telco] to work at a [Data Center] for more money. While I had IT certifications at this point, the [Data Center] gave me the chance to actually put them to use, while also keeping my telco skills alive.";

            SpeechBubble Speak = new SpeechBubble(message,null);
            cow Cow = new cow();
            Cow.Tongue = "U";
            Cow.showCow();
            //cowsay newCow = new cowsay(message);
            
        }
    }
}
