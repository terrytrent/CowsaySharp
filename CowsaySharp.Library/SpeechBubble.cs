using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CowsaySharp.Library
{
    static public class SpeechBubble
    {
        static public string ReturnSpeechBubble(string message, IBubbleChars bubbles, int? maxLineLength, bool figlet)
        {
            char[] splitChar = { ' ',(char)10,(char)13 };
            List<string> messageAsList = new List<string>();

            if (!maxLineLength.HasValue)
            {
                maxLineLength = 40;
            }
            else if (maxLineLength > 76 || maxLineLength < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLineLength), "Cannot specify a size smaller than 10 characters or larger than 76 characters");
            }

            if (figlet)
                messageAsList = SplitFigletToLinesAsList(message);
            else if (message.Length > maxLineLength)
                messageAsList = SplitToLinesAsList(message, splitChar, (int)maxLineLength);
            else if (message.Length < maxLineLength && message.IndexOfAny(splitChar) != -1)
                messageAsList = SplitToLinesAsListShort(message);

            if (message.Length > maxLineLength || messageAsList.Count > 1)
            {
                message = createLargeWordBubble(messageAsList, bubbles);
            }
            else
            {
                message = createSmallWordBubble(message, bubbles);
            }

            return message;
        }

        static string repeatCharacter(char character, int numberOfUnderscores)
        {
            return new string(character, numberOfUnderscores);
        }

        static string createSmallWordBubble(string message, IBubbleChars bubbles)
        {
            int lengthOfMessage = message.Length;
            int lengthOfTopAndBottomLinesInBubble = lengthOfMessage + 2;
            string topBubbleLine = repeatCharacter(bubbles.TopLine, lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter(bubbles.BottomLine, lengthOfTopAndBottomLinesInBubble);

            return $" {topBubbleLine} \r\n{bubbles.SmallLeft} {message.Trim()} {bubbles.SmallRight}\r\n {bottomBubbleLine}";
        }

        static string createLargeWordBubble(List<string> list, IBubbleChars bubbles)
        {
            StringBuilder bubbleBuilder = new StringBuilder();
            int longestLineInList = list.Max(s => s.Length);
            int lengthOfTopAndBottomLinesInBubble = longestLineInList + 2;
            string topBubbleLine = $" {repeatCharacter(bubbles.TopLine, lengthOfTopAndBottomLinesInBubble)}";
            string bottomBubbleLine = $" {repeatCharacter(bubbles.BottomLine, lengthOfTopAndBottomLinesInBubble)}";
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

        static List<string> SplitToLinesAsListShort(string message)
        {
            List<string> ListToReturn = new List<string>();
            StringBuilder sb = new StringBuilder(message);
            char[] splitChars = { (char)10, (char)13 };

            int startingIndex = 0;
            int lengthLeft = sb.ToString().Length;

            while (lengthLeft != 0)
            {

                int lengthIndex = sb.ToString().IndexOfAny(splitChars) != -1 ? sb.ToString().IndexOfAny(splitChars) : sb.ToString().Length;
                
                ListToReturn.Add(sb.ToString().Substring(startingIndex, lengthIndex));
                if (sb.ToString().Length == lengthIndex)
                    sb.Remove(startingIndex, lengthIndex);
                else
                {
                    string newLineChar = sb.ToString().Substring(lengthIndex, 2);
                    if (newLineChar == Environment.NewLine)
                        sb.Remove(startingIndex, lengthIndex + 2);
                    else
                        sb.Remove(startingIndex, lengthIndex + 1);
                }
                lengthLeft = sb.ToString().Length;
            }

            return ListToReturn;
        }

        static List<string> SplitToLinesAsList(string message, char[] splitOnCharacters, int maxStringLength)
        {
            List<string> ListToReturn = new List<string>();
            StringBuilder messageSB = new StringBuilder(message);
            char[] newLineCharacters = { (char)10,(char)13 };
            var index = 0;
            int splitAt;

            while (messageSB.ToString().Length > index)
            {
                if(index + maxStringLength <= messageSB.ToString().Length)
                {
                    string thisLine = messageSB.ToString().Substring(index, maxStringLength);
                    if (thisLine.IndexOfAny(newLineCharacters) != -1)
                        if (thisLine.StartsWith(((char)10).ToString()) || thisLine.StartsWith(((char)13).ToString()))
                            splitAt = 1;
                        else
                            splitAt = thisLine.LastIndexOfAny(newLineCharacters);
                    else
                        splitAt = thisLine.LastIndexOf(' ');
                }
                else
                {
                    splitAt = messageSB.ToString().Length - index;
                }

                splitAt = (splitAt == -1) ? maxStringLength : splitAt;

                ListToReturn.Add(messageSB.ToString().Substring(index, splitAt).Trim());
                messageSB.Remove(index, splitAt);

            }

            return ListToReturn;
        }

        static List<string> SplitFigletToLinesAsList(string text)
        {
            List<string> ListToReturn = new List<string>();
            char[] newLines = { (char)10, (char)13 };
            var sb = new StringBuilder(text);

            while(sb.Length > 0)
            {
                try
                {
                    int indexOfFirstNewLine = sb.ToString().IndexOfAny(newLines);
                    ListToReturn.Add(sb.ToString().Substring(0, indexOfFirstNewLine));
                    sb.Remove(0, indexOfFirstNewLine + 2);
                }
                catch
                {
                    ListToReturn.Add(sb.ToString().Substring(0));
                    sb.Clear();
                }
            }

            return ListToReturn;
        }
    }
}
