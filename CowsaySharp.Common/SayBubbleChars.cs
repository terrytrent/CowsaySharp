using CowsaySharp.Library;

namespace CowsaySharp.Common
{
    class SayBubbleChars : BubbleChars
    {
        public SayBubbleChars()
        {
            UpLeft = DownRight = "/";
            UpRight = DownLeft = Bubble = "\\";
            Left = Right = "|";
            SmallLeft = "<";
            SmallRight = ">";
        }
    }
}