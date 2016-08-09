using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    public class cowsay : SomethingSay
    {
        public cowsay(string message)
        {
            Console.WriteLine(thisSays(message));
            Console.WriteLine(thisObject());
        }

        public string thisSays(string message)
        {
            int oneLineLength = 40;
            char[] splitChar = { ' ' };
            if (message.Length > oneLineLength)
            {
                message = createLargeWordBubble(SplitToLinesAsList(message, splitChar, oneLineLength));
            }
            else
            {
                message = createSmallWordBubble(message);
            }
            return message;
        }

        

        public string thisObject()
        {
            return $@"         \   ^__^ 
          \  (Oo)\_______
             (__)\       )\/\
               U ||----w |
                 ||     ||";
        }

        string repeatCharacter(char character, int numberOfUnderscores)
        {
            return new string(character, numberOfUnderscores);
        }

        string createSmallWordBubble(string message)
        {
            int lengthOfMessage = message.Length;
            int lengthOfTopAndBottomLinesInBubble = lengthOfMessage + 2;
            string topBubbleLine = repeatCharacter('_', lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter('-', lengthOfTopAndBottomLinesInBubble);

            return $" {topBubbleLine} \r\n< {message.Trim()} >\r\n {bottomBubbleLine}";
        }

        string createLargeWordBubble(List<string> list)
        {
            string message="";

            int longestLineInList = list.Max(s => s.Length);
            int lengthOfTopAndBottomLinesInBubble = longestLineInList + 1;
            string topBubbleLine = repeatCharacter('_', lengthOfTopAndBottomLinesInBubble);
            string bottomBubbleLine = repeatCharacter('-', lengthOfTopAndBottomLinesInBubble);
            string firstLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[0].Length);
            string lastLineInMessageSpaces = repeatCharacter(' ', longestLineInList - list[list.Count - 1].Length);

            list[0] = $"/ {list[0]}{firstLineInMessageSpaces}\\";
            list[list.Count - 1] = $"\\ {list[list.Count - 1]}{lastLineInMessageSpaces}/";
            for (int i = 1; i < list.Count() - 1; i++)
            {
                int numberofspaces = longestLineInList - list[i].Length;
                string spacesInLine = repeatCharacter(' ', numberofspaces+1);

                list[i] = $"| {list[i]}{spacesInLine}|";
            }

            foreach (string line in list)
            {
                message = $"{message}\r\n{line}";
            }

            return $" {topBubbleLine} \r\n{message.Trim()}\r\n {bottomBubbleLine}";
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
    }
}
