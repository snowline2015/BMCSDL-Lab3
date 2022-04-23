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

namespace WinFormsApp1
{
    public partial class Grade : Form
    {
        public static bool addedRow = false;
        public static int currentRows = 0;

        public Grade()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow current = dataGridView2.CurrentRow;
            if (current.Cells[dataGridView2.Columns["MASV"].Index].Value.ToString() != ""
                && current.Cells[dataGridView2.Columns["MAHP"].Index].Value.ToString() != ""
                && current.Cells[dataGridView2.Columns["DIEMTHI"].Index].Value.ToString() != "")
            {
                if (current.Index == currentRows)       // Add New Row
                {
                    addedRow = true;
                    currentRows += 1;
                }
                if (!addedRow)
                {
                    string query = "UPDATE BANGDIEM SET MASV = '" + current.Cells[dataGridView2.Columns["MASV"].Index].Value
                        + "', MAHP = '" + current.Cells[dataGridView2.Columns["MAHP"].Index].Value
                        + "', DIEMTHI = ENCRYPTBYASYMKEY(ASYMKEY_ID('" + Form1.ID + "_DIEM"
                        + "'), CONVERT(VARCHAR(MAX), " + current.Cells[dataGridView2.Columns["DIEMTHI"].Index].Value
                        + ")) WHERE MASV = '" + current.Cells[dataGridView2.Columns["MASV"].Index].Value
                        + "' AND MAHP = '" + current.Cells[dataGridView2.Columns["MAHP"].Index].Value + "'";
                    SqlCommand cmd = new SqlCommand(query, Program.con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query = "INSERT INTO BANGDIEM VALUES ('" + current.Cells[dataGridView2.Columns["MASV"].Index].Value
                        + "', '" + current.Cells[dataGridView2.Columns["MAHP"].Index].Value
                        + "', ENCRYPTBYASYMKEY(ASYMKEY_ID('" + Form1.ID + "_DIEM"
                        + "'), CONVERT(VARCHAR(MAX), " + current.Cells[dataGridView2.Columns["DIEMTHI"].Index].Value
                        + ")))";
                    SqlCommand cmd = new SqlCommand(query, Program.con);
                    cmd.ExecuteNonQuery();
                    addedRow = false;
                }
            }
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }
    }
}
