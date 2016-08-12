using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowsaySharp.Common
{
    static public class StringArrayOfFilesToList
    {
        static public List<string> GetList(string[] stringArray)
        {
            List<string> list = new List<string>();

            foreach (string file in stringArray)
            {
                string fileName = Path.GetFileName(file);
                string extension = fileName.Substring(fileName.LastIndexOf("."));
                list.Add(fileName.Remove(fileName.IndexOf(extension), extension.Length));
            }

            return list;
        }
    }
}
