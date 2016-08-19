namespace CowsaySharp.Library
{
    /// <summary>
    /// Characters that will be used to generate the Speech Bubble
    /// </summary>
    public interface IBubbleChars
    {
        char topLine { get; }
        char bottomLine { get; }
        string UpLeft { get; }
        string UpRight { get; }
        string DownLeft { get; }
        string DownRight { get; }
        string Left { get; }
        string Right { get; }
        string SmallLeft { get; }
        string SmallRight { get; }
        string Bubble { get; }
    }
}
