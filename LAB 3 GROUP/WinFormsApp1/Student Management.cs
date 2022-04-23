using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Student_Management : Form
    {
        public Student_Management()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            comboBox1.ResetText();
            comboBox1.SelectedItem = null;

            string query = "SELECT MALOP, TENLOP FROM LOP WHERE MANV = '" + Form1.ID + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Program.con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                comboBox1.Items.Add(dtbl.Rows[i]["MALOP"].ToString() + " - " + dtbl.Rows[i]["TENLOP"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            string[] split = Regex.Split(comboBox1.SelectedItem.ToString(), " - ");

            string query = "SELECT MASV, HOTEN, NGAYSINH, DIACHI FROM SINHVIEN WHERE MALOP = '" + split[0] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Program.con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            dataGridView1.DataSource = dtbl;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }
    }
}
