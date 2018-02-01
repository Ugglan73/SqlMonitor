using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;

namespace TheSqlMonitor
{
    public partial class Service1 : ServiceBase
    {
        
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\TheSqlMonitor", false);
            object o = rk.GetValue("SqlKey", "DontExist");



            if (o.ToString() == "DontExist")
            {
                Writelog("System Exit : no registry key found");
                Application.Exit();
            }
            else
            {
                Writelog("System Starting" + ": " + o.ToString());
            }
        }

        protected override void OnStop()
        {
            Writelog("System shuting down");
            Application.Exit();
        }

        private void Writelog (string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
            //string path = @"D:";
            //File.AppendAllText(path + @"\SystemLog.txt", AppDomain.CurrentDomain.BaseDirectory.ToString() + "\r\n");
            File.AppendAllText(path + @"SystemLog.txt", DateTime.Now.ToString() + " : " + text.ToString() + "\r\n");

        }

    }
}
