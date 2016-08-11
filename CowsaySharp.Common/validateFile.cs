using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CowsaySharp.Common
{
    static public class validateFile
    {
        static public bool validate(string file)
        {
            try
            {
                var fileAccess = File.GetAccessControl(file);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
