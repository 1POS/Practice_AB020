// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using DataGridViewAutoFilter;
using System.Drawing.Text;
namespace BaseGIBDD
{
    public partial class Filtr : Form
    {
        public Filtr()
        {
            InitializeComponent();
        }

        private const string V = "ФИО LIKE'";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FIO = V;
            mainForm main = (mainForm)this.Owner;
            main.ds.Filter = FIO + nameFiltr.Text + "%";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            mainForm main = (mainForm)this.Owner;
            main.ds.Filter = "[Марка машины] LIKE'" + markFiltr.Text + "%'";
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            mainForm main = (mainForm)this.Owner;
            main.ds.Filter = "[Номер машины] LIKE'" + maskedTextBox1.Text + "%'";
        }

        private void nameFiltr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            mainForm main = (mainForm)this.Owner;
            main.ds.Filter = "[Год выпуска] LIKE'" + textBox1.Text + "%'";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            mainForm main = (mainForm)this.Owner;
            main.ds.Filter = "[Дата последнего техосмотра] LIKE'" + textBox2.Text + "%'";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm main = (mainForm)this.Owner;
            nameFiltr.Text = "";
            markFiltr.Text = "";
            maskedTextBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            for (int i = 0; i < main?.count; i++)
            {
                if (main.count == main.dataGridView1.Rows.Count)
                {
                    main.dataGridView1[0, i].Value = i + 1;
                }
            }
            Close();
        }
    }
}
