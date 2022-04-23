using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static string ID = string.Empty;
        public static string pubKey = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static string Md5(string input, bool isLowercase = false)
        {
            using (var md5 = MD5.Create())
            {
                var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }

        public static string Sha1(string input, bool isLowercase = false)
        {
            using (var sha1 = SHA1.Create())
            {
                var byteHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.con.Open();
            string query = "SELECT MANV, PUBKEY FROM NHANVIEN WHERE TENDN = '" + textBox1.Text.Trim() + "' AND CONVERT(VARCHAR(MAX), MATKHAU, 2) = '" + Sha1(textBox2.Text.Trim()) + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Program.con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                MessageBox.Show("Login Sucessfully !");
                ID = dtbl.Rows[0]["MANV"].ToString();
                pubKey = dtbl.Rows[0]["PUBKEY"].ToString();

                this.Hide();
                Student_Management sm = new Student_Management();
                sm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect !");
                Program.con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
