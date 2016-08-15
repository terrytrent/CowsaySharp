using System.IO;
using System.Reflection;
using CowsaySharp.ConsoleLibrary;

namespace CowsaySharp.Say
{
    class Program
    {
        static public string strAppDir;

        static void Main(string[] args)
        {
            const bool think = false;
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Switches.processSwitches(args, strAppDir, think);
        }
    }
}
