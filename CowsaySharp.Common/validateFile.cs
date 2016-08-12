using System.IO;

namespace CowsaySharp.Common
{
    static public class ValidateFile
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
