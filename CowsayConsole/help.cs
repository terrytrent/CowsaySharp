using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CowsayConsole
{
    static class help
    {
        static public void DisplayHelp()
        {
            //[-n] removed becuase it is not yet implemented
            string help = $@"CowsaySharp version {Application.ProductVersion}, (c) 2016 Terry Trent

  The original idea of cowsay come from:
    [Tony Monroe](http://www.nog.net/~tony/)
    [cowsay](https://github.com/schacon/cowsay)

Usage: cowsay [-bdgpstwy] [-h] [-e eyes] [-f cowfile] 
          [-l] [-T tongue] [-W wrapcolumn] [message]";

          Console.WriteLine(help);

        }
    }
}
