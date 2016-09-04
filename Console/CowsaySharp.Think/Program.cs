using System.IO;
using System.Reflection;
using CowsaySharp.Library;
using CowsaySharp.ConsoleLibrary;

namespace CowsaySharp.Think
{
    class Program
    {
        static public string strAppDir;
        
        static void Main(string[] args)
        {
            IBubbleChars bubbleChars = new ThinkBubbleChars();
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Switches.processSwitches(args, strAppDir, bubbleChars);
        }
    }
}
