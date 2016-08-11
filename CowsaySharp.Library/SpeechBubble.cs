using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsaySharp.Library
{
    public class SpeechBubble
    {
        
        public SpeechBubble(string message) : this(message, false, null)
        {
        }

        public SpeechBubble(string message, bool think, int? maxLineLength)
        {
            char[] splitChar = { ' ' };
            Bubbles bubbles = new Bubbles();
            if(think)
            {
                bubbles.setBubbles(Bubbles.bubbleType.think);
            }
            else
            {
                bubbles.setBubbles(Bubbles.bubbleType.say);
            }

            if (!maxLineLength.HasValue)
            {
                maxLineLength = 40;
            }
            else if(maxLineLength > 76 || maxLineLength < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLineLength), "Cannot specify a size smaller than 10 characters or larger than 78 characters");
            }

            List<string> messageAsList = SplitToLinesAsList(message, splitChar, (int)maxLineLength);

            if (message.Length > maxLineLength)
            {
                message = createLargeWordBubble(messageAsList, bubbles);
            }
            else
            {
                message = createSmallWordBubble(message, bubbles);
            }
            Console.WriteLine(message);
        }

        string repeatCharacter(char character, int numberOfUnderscores)
        {
            return new string(character, numberOfUnderscores);
        }

        string createSmallWordBubble(string message, Bubbles bubbles)
        {
            int lengthOfMessage = message.Length;
            int lengthOfTopAndBottomLinesInBubble = lengthOfMessage + 2;
            string topBubbleLine = repeatCharacter('_', lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter('-', lengthOfTopAndBottomLinesInBubble);

            return $" {topBubbleLine} \r\n{bubbles.SmallLeft} {message.Trim()} {bubbles.SmallRight}\r\n {bottomBubbleLine}";
        }

        string createLargeWordBubble(List<string> list, Bubbles bubbles)
        {
            string message = "";

            int longestLineInList = list.Max(s => s.Length);
            int lengthOfTopAndBottomLinesInBubble = longestLineInList + 1;
            string topBubbleLine = repeatCharacter('_', lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter('-', lengthOfTopAndBottomLinesInBubble);
            string firstLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[0].Length);
            string lastLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[list.Count - 1].Length);

            list[0] = $"{bubbles.UpLeft} {list[0]}{firstLineInMessageSpaces}{bubbles.UpRight}";
            list[list.Count - 1] = $"{bubbles.DownLeft} {list[list.Count - 1]}{lastLineInMessageSpaces}{bubbles.DownRight}";
            for (int i = 1; i < list.Count() - 1; i++)
            {
                int numberofspaces = longestLineInList - list[i].Length;
                string spacesInLine = repeatCharacter(' ', numberofspaces + 1);

                list[i] = $"{bubbles.Left} {list[i]}{spacesInLine}{bubbles.Right}";
            }

            foreach (string line in list)
            {
                message = $"{message}\r\n{line}";
            }

            return $" {topBubbleLine} {Environment.NewLine}{message.Trim()}\r\n {bottomBubbleLine}";
        }

        List<string> SplitToLinesAsList(string text, char[] splitOnCharacters, int maxStringLength)
        {
            List<string> ListToReturn = new List<string>();
            var sb = new StringBuilder();
            var index = 0;

            while (text.Length > index)
            {
                // start a new line, unless we've just started
                if (index != 0)
                    sb.AppendLine();

                // get the next substring, else the rest of the string if remainder is shorter than `maxStringLength`
                var splitAt = index + maxStringLength <= text.Length ? text.Substring(index, maxStringLength).LastIndexOfAny(splitOnCharacters) : text.Length - index;

                // if can't find split location, take `maxStringLength` characters
                splitAt = (splitAt == -1) ? maxStringLength : splitAt;

                // add result to collection & increment index
                //sb.Append(text.Substring(index, splitAt).Trim());
                ListToReturn.Add(text.Substring(index, splitAt).Trim());

                index += splitAt;
            }

            return ListToReturn;
        }

        
        //attempt at creating a figlet interpretter....failure.
        //public List<string> StoreLinesInList(string text)
        //{
        //    List<string> ListToReturn = new List<string>();

        //    string stringToSplitOn = Environment.NewLine;

        //    StringBuilder message = new StringBuilder();

        //    message.Append(text);

        //    string messageAsString = message.ToString();
        //    int indexOfStringToSplit = messageAsString.IndexOf(stringToSplitOn);

        //    int firstIndex = 0;
        //    while (message.Length > 0)
        //    {
        //        ListToReturn.Add(messageAsString.Substring(firstIndex, indexOfStringToSplit));
        //        message.Remove(firstIndex, indexOfStringToSplit);
                

        //        messageAsString = message.ToString();
        //        if (messageAsString.IndexOf(stringToSplitOn) == -1)
        //        {
        //            indexOfStringToSplit = messageAsString.Length;
        //        }
        //        else
        //        {
        //            indexOfStringToSplit = messageAsString.Substring(firstIndex).IndexOf(stringToSplitOn);
        //            message.Remove(0, 2);
        //        }

        //    }

        //    return ListToReturn;
        //}
    }
}
