using System;
using System.IO;
using System.Windows.Forms;

namespace genetic_alghoritm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) { Application.Exit(); return; }
            string path = "";
            foreach (string s in args) { if (path == "") { path += s; } else { path += " " + s; } }
            if (!File.Exists(path)) { MessageBox.Show(path); Application.Exit(); return; }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(path));
        }
    }
}
