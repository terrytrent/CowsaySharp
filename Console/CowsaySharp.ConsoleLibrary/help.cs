using System;
using System.Windows.Forms;

namespace CowsaySharp.ConsoleLibrary
{
    static class Help
    {
        static public void DisplayHelp()
        {
            string help = $@"CowsaySharp version {Application.ProductVersion}, (c) 2016 Terry Trent

  The original idea of cowsay come from:
    [Tony Monroe](http://www.nog.net/~tony/)
    [cowsay](https://github.com/schacon/cowsay)

Usage: cowsay [-bdgpstwy] [-h] [-e eyes] [-f cowfile] 
          [-l] [-L] [-n] [-T tongue] [-W wrapcolumn] [message]";

          Console.WriteLine(help);

        }
    }
}
