using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace CommonLib
{
    public class Logger
    {
        public Logger()
        {
            Console.WriteLine("Logger Start");

            string sAttr = ConfigurationManager.AppSettings.Get("Key0");
            Console.WriteLine("The value of Key0 is " + sAttr);

        }

        public static String SolutionPath => Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    }
}
