using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CowsaySharp.Common
{
    static public class validateDirectory
    {
        static public bool validate(string directory)
        {
            try
            {
                var directoryAccess = Directory.GetAccessControl(directory);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
