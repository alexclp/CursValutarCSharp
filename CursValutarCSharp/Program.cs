﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursValutarCSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            MessageBox.Show("Entered program.cs");

            NetworkingClass.ParseData(NetworkingClass.RequestData());
            //string temp = NetworkingClass.RequestData();
        }
    }
}
