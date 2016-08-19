using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsaySharp.Library
{
    class ThinkBubbleChars : BubbleChars
    {
        public ThinkBubbleChars()
        {
            UpLeft = DownLeft = Left = SmallLeft = "(";
            UpRight = DownRight = Right = SmallRight = ")";
            Bubble = "o";
        }
    }
}
