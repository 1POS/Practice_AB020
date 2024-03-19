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
using System.Text.Encodings.Web;
using System.IO;
using DataGridViewAutoFilter;



namespace BaseGIBDD
{
    public partial class mainForm : Form
    {

        public mainForm()
        {
            InitializeComponent();
            textBox1.Text = "Поиск...";
            textBox1.ForeColor = Color.Gray;
        }

        public int count = 0;

        public bool VisibleEdit = false;

        public bool VisibleNew = false;

        public BindingSource ds = new BindingSource();

        public DataTable dt = new DataTable();
        public void Form1_Load(object sender, EventArgs e) //Выгружаем данные из файла
        {
            dt.Columns.Add("№", typeof(int));
            dt.Columns.Add("ФИО", typeof(string));
            dt.Columns.Add("Марка машины", typeof(string));
            dt.Columns.Add("Номер машины", typeof(string));
            dt.Columns.Add("Год выпуска", typeof(string));
            dt.Columns.Add("Дата последнего техосмотра", typeof(string));
            LoadToolStripMenuItem_Click(new object(), new EventArgs());
            ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Удалить строку");
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Копировать строку");
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Вставить строку");
            ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("Редактировать строку");
            contextMenuStrip1.Items.Add(toolStripMenuItem1);
            contextMenuStrip1.Items.Add(toolStripMenuItem2);
            contextMenuStrip1.Items.Add(toolStripMenuItem3);
            contextMenuStrip1.Items.Add(toolStripMenuItem4);
            toolStripMenuItem1.Click += delete_click; //привязываем метод
            toolStripMenuItem2.Click += copy_click;
            toolStripMenuItem3.Click += paste_click;
            toolStripMenuItem4.Click += edit_click;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            ds.DataSource = dt;
            dataGridView1.DataSource = ds;
        }
        private void delete_click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow); //Удаление выбранной строки
            count--;
            for (int i = 0; i < count; i++)
            {
                if (count == dataGridView1.Rows.Count)
                {
                    dataGridView1[0, i].Value = i + 1;
                }
            }
        }

        Person copyPaste = new Person();
        private void copy_click(object sender, EventArgs e)
        {
            copyPaste.name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            copyPaste.carMark = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            copyPaste.carNum = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            copyPaste.releaseDate = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            copyPaste.inspection = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
        }
        private void paste_click(object sender, EventArgs e)
        {
            dataGridView1[1, dataGridView1.CurrentRow.Index].Value = copyPaste.name;
            dataGridView1[2, dataGridView1.CurrentRow.Index].Value = copyPaste.carMark;
            dataGridView1[3, dataGridView1.CurrentRow.Index].Value = copyPaste.carNum;
            dataGridView1[4, dataGridView1.CurrentRow.Index].Value = copyPaste.releaseDate;
            dataGridView1[5, dataGridView1.CurrentRow.Index].Value = copyPaste.inspection;
        }
        private void edit_click(object sender, EventArgs e)
        {
            newPerson Person = new newPerson();
            Person.Owner = this;
            Person.Show();
            VisibleEdit = true;
        }

        private void WriteData(Person roma) //Добавляем новые данные
        {
            StreamWriter fileWriter = new StreamWriter("data.txt", true);
            fileWriter.WriteLine(JsonSerializer.Serialize(roma));
            fileWriter.Close();
        }

        private void button5_Click(object sender, EventArgs e) //Поиск
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;

                            break;
                        }
            }
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText("data.txt", string.Empty);
            Person roma = new Person();
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                roma.name = dataGridView1[1, i].Value.ToString();
                roma.carMark = dataGridView1[2, i].Value.ToString();
                roma.carNum = dataGridView1[3, i].Value.ToString();
                roma.releaseDate = dataGridView1[4, i].Value.ToString();
                roma.inspection = dataGridView1[5, i].Value.ToString();
                WriteData(roma);
            }
            MessageBox.Show("Данные сохранены");
        }
        public void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Person roma;
            StreamReader reader = new StreamReader("data.txt");
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            count = 0;
            while (!reader.EndOfStream)
            {
                roma = JsonSerializer.Deserialize<Person>(reader.ReadLine());
                dt.Rows.Add(new object[] { count + 1, roma.name, roma.carMark, roma.carNum, roma.releaseDate, roma.inspection });
                count++;
            }
            reader.Close();
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newPerson nP = new newPerson();
            nP.Owner = this;
            nP.Show();
            VisibleNew = true;
        }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            count = 0;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Поиск...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Поиск...";
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void exelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Worksheet result = ExcelWorkSheet;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }

            ExcelApp.Visible = false;
            ExcelApp.UserControl = true;
            //Отключаем отображения таблицы, но оставляем управление
            SaveFileDialog save = new SaveFileDialog();
            //Создаяем окно для выбора пути файла
            if (save.ShowDialog() == DialogResult.Cancel)
                return;
            //если нажали на этом окно отмена закрыть окно
            string fullfilename2007 = System.IO.Path.Combine(save.ToString(), save.FileName);
            //Получить имя файла и его путь ввиде строки
            if (System.IO.File.Exists(fullfilename2007)) System.IO.File.Delete(fullfilename2007);
            //Если уже существует файл по этому пути, то удаляем его
            ExcelWorkBook.SaveAs(fullfilename2007, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
            //Сохраняем файл по нашему пути
            MessageBox.Show("Файл экспортирован");
        }
        private void button1_Click(object sender, EventArgs e) //Фильтрация
        {
            Filtr filtr = new Filtr();
            filtr.Owner = this;
            filtr.Show();
        }
    }
}