namespace CowsaySharp.Library
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