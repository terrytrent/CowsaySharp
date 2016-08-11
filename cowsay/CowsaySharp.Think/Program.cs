using System.IO;
using System.Reflection;
using CowsayConsole;

namespace CowsaySharp.Think
{
    class Program
    {
        static public string strAppDir;

        static void Main(string[] args)
        {
            const bool think = true;
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strAppDirParsed = strAppDir.Substring(strAppDir.IndexOf("C:\\"), strAppDir.Length - 6);

            switchesTest.processSwitches(args, strAppDirParsed, think);
        }
    }
}
