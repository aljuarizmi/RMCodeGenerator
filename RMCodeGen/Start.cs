using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using DataTierGenerator;

namespace DataTierGenerator
{
    class Start
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static void StartApp()
        {
            Application.EnableVisualStyles();
            frmSplash splash = new frmSplash();
            splash.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            splash.Close();         
            Application.Run(new frmGenerador());
            
        }
    }
}
