using System;
using System.Windows.Forms;

namespace AddPath
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Call this function to add in my custom application's location to the system path variable
                SetPathVariable("PSModulePath");
            }
            catch
            {
            }
            finally
            {
            }
        }

        #region public methods
        public static void SetPathVariable(string Variable)
        {
            //Read and place the command line arguments into a string array.
            //The custom application path is the first argument.
            string[] cmd_args = System.Environment.GetCommandLineArgs();

            //Now call this function to append the custom application's folder
            //to the system Path environment variable
            AppendPathVariable(cmd_args[1],Variable);
        }

        public static void AppendPathVariable(string appPath, string Variable)
        {
            try
            {
                //Filter custom application's path
                string loc = GetPathOnly(appPath);
                //Get the current value of the Path environment variable
                string Value = System.Environment.GetEnvironmentVariable(Variable, EnvironmentVariableTarget.Machine);
                MessageBox.Show(loc);
                //Only append the custom application's path if it is not already in
                //the system Path environment variable.
                if (Value.ToUpper().Contains(loc.ToUpper()) == false)
                {
                    Value = Value + ";" + loc;
                    System.Environment.SetEnvironmentVariable(Variable, Value, EnvironmentVariableTarget.Machine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //This function simply ensures that the input string is a valid
        //path string
        public static string GetPathOnly(string url)
        {
            string outputStr = string.Empty;
            string ipurl = url;
            char[] delimiter = { '\\' };
            string[] values = ipurl.Split(delimiter);
            for (int i = 0; i < values.Length - 1; i++)
            {
                if (i == 0)
                {
                    outputStr = values[i];
                }
                else
                {
                    outputStr = outputStr + @"\" + values[i];
                }
            }
            return outputStr;
        }
        #endregion
    }
}
