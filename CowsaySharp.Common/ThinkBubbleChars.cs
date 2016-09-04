using CowsaySharp.Library;

namespace CowsaySharp.Common
{
    public class ThinkBubbleChars : BubbleChars
    {
        public ThinkBubbleChars()
        {
            UpLeft = DownLeft = Left = SmallLeft = "(";
            UpRight = DownRight = Right = SmallRight = ")";
            Bubble = "o";
        }
    }
}
