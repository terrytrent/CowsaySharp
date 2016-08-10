using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CowsayConsole
{
    class Program
    {
        static public string strAppDir;

        static void Main(string[] args)
        {
            strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string strAppDirParsed = strAppDir.Substring(strAppDir.IndexOf("C:\\"), strAppDir.Length - 6);

            switchesTest.processSwitches(args, strAppDirParsed);
        }
    }
}
