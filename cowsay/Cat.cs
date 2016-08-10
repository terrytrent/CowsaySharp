using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    class Cat
    {
        private string eyes;
        private string catAscii;

        public string CatAscii
        {
            get { return catAscii; }
            set { catAscii = value; }
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

        public Cat() : this(null, false)
        {

        }

        public Cat(string catEyes, bool think)
        {
            Eyes = catEyes;

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
                Eyes = "OO";


            catAscii = @"     " + bubbles.LargeBubble + @"
      " + bubbles.SmallBubble + @"   
       /\     /\
      {  `---'  }
      {  " + Eyes[0]+@"   "+Eyes[1]+@"  }
      ~~>  V  <~~
       \  \|/  /
        `-----'__
        /     \  `^\_
       {       }\ |\_\_   W
       |  \_/  |/ /  \_\_( )
        \__/  /(_E     \__/
          (  /
           MM
    ";
            Console.WriteLine(CatAscii);
        }

        public void showCat()
        {
            Console.WriteLine(CatAscii);
        }
    }
}
