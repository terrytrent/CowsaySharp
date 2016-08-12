using System.IO;

namespace CowsaySharp.Common
{
    static public class ValidateDirectory
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
