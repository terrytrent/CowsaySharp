using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            StringBuilder bubbleBuilder = new StringBuilder();
            int longestLineInList = list.Max(s => s.Length);
            int lengthOfTopAndBottomLinesInBubble = longestLineInList + 2;
            string topBubbleLine = $" {repeatCharacter('_', lengthOfTopAndBottomLinesInBubble)}";
            string bottomBubbleLine = $" {repeatCharacter('-', lengthOfTopAndBottomLinesInBubble)}";
            string firstLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[0].Length + 1);
            string lastLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[list.Count - 1].Length + 1);

            bubbleBuilder.AppendLine(topBubbleLine);
            bubbleBuilder.AppendLine($"{bubbles.UpLeft} {list[0]}{firstLineInMessageSpaces}{bubbles.UpRight}");
            for (int i = 1; i < list.Count() - 1; i++)
            {
                int numberofspaces = longestLineInList - list[i].Length;
                string spacesInLine = repeatCharacter(' ', numberofspaces + 1);

                bubbleBuilder.AppendLine($"{bubbles.Left} {list[i]}{spacesInLine}{bubbles.Right}");
            }
            bubbleBuilder.AppendLine($"{bubbles.DownLeft} {list[list.Count - 1]}{lastLineInMessageSpaces}{bubbles.DownRight}");
            bubbleBuilder.AppendLine(bottomBubbleLine);

            return bubbleBuilder.ToString();
        }

        List<string> SplitToLinesAsList(string text, char[] splitOnCharacters, int maxStringLength)
        {
            List<string> ListToReturn = new List<string>();
            var sb = new StringBuilder();
            var index = 0;

            while (text.Length > index)
            {
                if (index != 0)
                    sb.AppendLine();

                var splitAt = index + maxStringLength <= text.Length ? text.Substring(index, maxStringLength).LastIndexOfAny(splitOnCharacters) : text.Length - index;

                splitAt = (splitAt == -1) ? maxStringLength : splitAt;

                ListToReturn.Add(text.Substring(index, splitAt).Trim());

                index += splitAt;
            }

            return ListToReturn;
        }
    }
}
