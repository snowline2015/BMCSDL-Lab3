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
            Program.con.Close();
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

            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                if (dc.Index.Equals(0))
                {
                    dc.ReadOnly = true;
                }
                else
                {
                    dc.ReadOnly = false;
                }
            }

            DataGridViewButtonColumn editGradeButton = new DataGridViewButtonColumn();
            editGradeButton.Name = "DIEM";
            editGradeButton.Text = "Edit";
            editGradeButton.UseColumnTextForButtonValue = true;
            if (dataGridView1.Columns["DIEM"] == null)
            {
                dataGridView1.Columns.Insert(dataGridView1.ColumnCount, editGradeButton);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow current = dataGridView1.CurrentRow;
            string query = "UPDATE SINHVIEN SET HOTEN = '" + current.Cells[dataGridView1.Columns["HOTEN"].Index].Value 
                + "', NGAYSINH = '" + current.Cells[dataGridView1.Columns["NGAYSINH"].Index].Value
                + "', DIACHI = '" + current.Cells[dataGridView1.Columns["DIACHI"].Index].Value + 
                "' WHERE MASV = '" + current.Cells[dataGridView1.Columns["MASV"].Index].Value + "'";
            SqlCommand cmd = new SqlCommand(query, Program.con);
            cmd.ExecuteNonQuery();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Grade editGradeForm = new Grade();

            if (e.ColumnIndex == dataGridView1.Columns["DIEM"].Index)
            {
                DataGridViewRow current = dataGridView1.CurrentRow;
                string query = "SELECT MASV, MAHP, DIEMTHI FROM BANGDIEM WHERE MASV = '" + current.Cells[dataGridView1.Columns["MASV"].Index].Value + "'";

                SqlDataAdapter sda = new SqlDataAdapter(query, Program.con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                editGradeForm.dataGridView2.DataSource = dtbl;
                editGradeForm.dataGridView2.EditMode = DataGridViewEditMode.EditOnEnter;

                foreach (DataGridViewColumn dc in editGradeForm.dataGridView2.Columns)
                {
                    if (dc.Index.Equals(0) || dc.Index.Equals(1))
                    {
                        dc.ReadOnly = true;
                    }
                    else
                    {
                        dc.ReadOnly = false;
                    }
                }

                editGradeForm.ShowDialog();
            }
        }
    }
}
