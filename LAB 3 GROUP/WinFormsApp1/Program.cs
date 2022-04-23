using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    static class Program
    {
        // @"Data Source=xxx;Initial Catalog=xxx;Integrated Security=True"
        public static SqlConnection con = new SqlConnection(@"Data Source=YORHA\SQLEXPRESS;Initial Catalog=QLSVNhom;Integrated Security=True");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
