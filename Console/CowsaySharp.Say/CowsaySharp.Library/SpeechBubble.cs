﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CowsaySharp.Library
{
    public class SpeechBubble
    {
        
        static public string ReturnSpeechBubble(string message)
        {
            return ReturnSpeechBubble(message, false, null, false);
        }

        static public string ReturnSpeechBubble(string message, bool think, int? maxLineLength, bool figlet)
        {
            char[] splitChar = { ' ' };
            Bubbles bubbles = new Bubbles();
            List<string> messageAsList = new List<string>();
            if (think)
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
            else if (maxLineLength > 76 || maxLineLength < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLineLength), "Cannot specify a size smaller than 10 characters or larger than 78 characters");
            }

            if (figlet)
                messageAsList = SplitFigletToLinesAsList(message);
            else
                messageAsList = SplitToLinesAsList(message, splitChar, (int)maxLineLength);

            if (message.Length > maxLineLength)
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

        static string createSmallWordBubble(string message, Bubbles bubbles)
        {
            int lengthOfMessage = message.Length;
            int lengthOfTopAndBottomLinesInBubble = lengthOfMessage + 2;
            string topBubbleLine = repeatCharacter('_', lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter('-', lengthOfTopAndBottomLinesInBubble);

            return $" {topBubbleLine} \r\n{bubbles.SmallLeft} {message.Trim()} {bubbles.SmallRight}\r\n {bottomBubbleLine}";
        }

        static string createLargeWordBubble(List<string> list, Bubbles bubbles)
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

        static List<string> SplitToLinesAsList(string text, char[] splitOnCharacters, int maxStringLength)
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

        static List<string> SplitFigletToLinesAsList(string text)
        {
            List<string> ListToReturn = new List<string>();
            var sb = new StringBuilder(text);

            while(sb.Length > 0)
            {
                try
                {
                    int indexOfFirstNewLine = sb.ToString().IndexOf(Environment.NewLine);
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