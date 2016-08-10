using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CowsayLibrary
{
    class program
    {
        static void Main(string[] args)
         {
            //ListCowfiles files = new ListCowfiles("C:\\cow");

            CowFace face = CowFaces.getCowFace(CowFaces.cowFaces.dead);

            foreach (string file in Directory.GetFiles("C:\\cow\\cows"))
            {
                Console.WriteLine(file);
                GetCow.ReturnCow(file, true,face);
            }
            

            

            ////string shortMessage = "Julie is a cutie!";
            ////bool think = false;
            ////SpeechBubble Speak = new SpeechBubble(shortMessage, think,null);
            ////Cow Cow = new Cow();

            //bool thinkAgain = true;
            //string mediumMessage = "Julie is a cutie, and I love her with all my heart!";
            //SpeechBubble SpeakAgain = new SpeechBubble(mediumMessage, thinkAgain, null);
            //Cow anotherCow = new Cow(face.Eyes, face.Tongue, thinkAgain);

            //bool thinkOnceMore = false;
            //string longMessage = "Starting with Visual Studio 2008 SP1, the debugger steps over properties and operators in managed code by default. In most cases, this provides a better debugging experience. You can disable this behavior if you want the debugger to step into a property or operator.";
            //SpeechBubble SpeakOnceMore = new SpeechBubble(longMessage,thinkOnceMore, 76);
            //Cow oneMoreCow = new Cow("xx","U", thinkOnceMore);

            //string catMessage = "Meow!";
            //bool catThink = true;
            //SpeechBubble CowSpeak = new SpeechBubble(catMessage, catThink, null);
            //Cat cat = new Cat("XX", catThink);

        }
    }
}
