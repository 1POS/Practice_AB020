
// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.comusing System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace BaseGIBDD
{
    public partial class newPerson : Form
    {
        public newPerson()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) //новый автовладелец
        {
            mainForm main = this.Owner as mainForm;
            main.count++;
            DataRow newRow = main.dt.NewRow();
            newRow[0] = main.count;
            newRow[1] = name.Text;
            newRow[2] = mark.Text;
            newRow[3] = maskedTextBox1.Text;
            newRow[4] = releaseDate.Value.ToShortDateString();
            newRow[5] = inspection.Value.ToShortDateString();
            main.dt.Rows.Add(newRow);
            main.VisibleNew = false;
            savePerson.Visible = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm main = this.Owner as mainForm;
            main.VisibleNew = false;
            savePerson.Visible = false;
            main.VisibleEdit = false;
            button1.Visible = false;
            Close();

        }
        private void button1_Click_1(object sender, EventArgs e) //редактирование данных
        {
            mainForm main = this.Owner as mainForm;
            main.dataGridView1[1, main.dataGridView1.CurrentRow.Index].Value = name.Text;
            main.dataGridView1[2, main.dataGridView1.CurrentRow.Index].Value = mark.Text;
            main.dataGridView1[3, main.dataGridView1.CurrentRow.Index].Value = maskedTextBox1.Text;
            main.dataGridView1[4, main.dataGridView1.CurrentRow.Index].Value = releaseDate.Value.ToShortDateString();
            main.dataGridView1[5, main.dataGridView1.CurrentRow.Index].Value = inspection.Value.ToShortDateString();
            main.VisibleEdit = false;
            button1.Visible = false;
            Close();
        }
        private void newPerson_Shown(object sender, EventArgs e)
        {
            mainForm main = this.Owner as mainForm;
            savePerson.Visible = main.VisibleNew;
            button1.Visible = main.VisibleEdit;
            if (main.VisibleEdit == true)
            {
                name.Text = main.dataGridView1[1, main.dataGridView1.CurrentRow.Index].Value.ToString();
                mark.Text = main.dataGridView1[2, main.dataGridView1.CurrentRow.Index].Value.ToString();
                maskedTextBox1.Text = main.dataGridView1[3, main.dataGridView1.CurrentRow.Index].Value.ToString();
                releaseDate.Text = main.dataGridView1[4, main.dataGridView1.CurrentRow.Index].Value.ToString();
                inspection.Text = main.dataGridView1[5, main.dataGridView1.CurrentRow.Index].Value.ToString();
            }
            if (main.VisibleNew == true)
            {
                name.Text = "";
                mark.Text = "";
                maskedTextBox1.Text = "";
                releaseDate.Text = "";
                inspection.Text = "";
            }
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }
    }
}
