using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cowsay.Common
{
    public class validateDirectory
    {
        static public bool validate(string directory)
        {
            try
            {
                var fullPath = Directory.GetAccessControl(directory);
                    //Path.GetFullPath(directory);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
