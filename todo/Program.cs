using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            Application.Run(form1);
            // After the form exists, update lists.txt
            string stringToWrite = "Warning: Modifying this file is not recommended. If this file is corrupted, the program will not work\n\n";
            foreach (string String in form1.Lists)
            {
                stringToWrite += String + "\n";
            }
            File.WriteAllText(@"data\lists.txt", stringToWrite);
        }
    }
}
