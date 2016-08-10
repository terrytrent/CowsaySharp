using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsayLibrary
{
    class Bubbles
    {
        public enum bubbleType
        {
            think,
            say
        }

        public string UpLeft { get; private set; }
        public string UpRight { get; private set; }
        public string DownLeft { get; private set; }
        public string DownRight { get; private set; }
        public string Left { get; private set; }
        public string Right { get; private set; }
        public string SmallLeft { get; private set; }
        public string SmallRight { get; private set; }
        public bubbleType Type { get; private set; }
        public string Bubble { get; private set; }

        public Bubbles() { }

        public void setBubbles(bubbleType type)
        {
            Type = type;
            if (Type == bubbleType.say)
            {
                UpLeft = DownRight = "/";
                UpRight = DownLeft = Bubble = "\\";
                Left = Right = "|";
                SmallLeft = "<";
                SmallRight = ">";
            }
            else if (Type == bubbleType.think)
            {
                UpLeft = DownLeft = Left = SmallLeft = "(";
                UpRight = DownRight = Right = SmallRight = ")";
                Bubble = "o";
            }
        }

    }
}
