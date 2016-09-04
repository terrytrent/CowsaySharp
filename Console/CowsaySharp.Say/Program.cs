using System.IO;
using System.Reflection;
using CowsaySharp.Library;
using CowsaySharp.ConsoleLibrary;

namespace CowsaySharp.Say
{
    class Program
    {
        static public string strAppDir;

        static void Main(string[] args)
        {
            IBubbleChars bubbleChars = new SayBubbleChars();
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Switches.processSwitches(args, strAppDir, bubbleChars);
        }
    }
}
