using System.IO;
using System.Reflection;
using CowsaySharp.ConsoleLibrary;
using CowsaySharp.Library;

namespace CowsaySharp.Say
{
    class Program
    {
        static public string strAppDir;

        static void Main(string[] args)
        {
            const bool think = false;
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strAppDirParsed = strAppDir.Substring(strAppDir.IndexOf("C:\\"), strAppDir.Length - 6);

            Switches.processSwitches(args, strAppDirParsed, think);
         }
    }
}
