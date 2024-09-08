using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "studentsDataSet.Студенты". При необходимости она может быть перемещена или удалена.
            this.студентыTableAdapter.Fill(this.studentsDataSet.Студенты);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "studentsDataSet.Оценки". При необходимости она может быть перемещена или удалена.
            this.оценкиTableAdapter.Fill(this.studentsDataSet.Оценки);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "studentsDataSet.Предметы". При необходимости она может быть перемещена или удалена.
            this.предметыTableAdapter.Fill(this.studentsDataSet.Предметы);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "studentsDataSet.Специальности". При необходимости она может быть перемещена или удалена.
            this.специальностиTableAdapter.Fill(this.studentsDataSet.Специальности);

            DataSet ds = studentsDataSet;

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Дисцилина"; //текст в шапке
            column1.Name = "Предмет"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки текстовый

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Количество двоечников";
            column2.Name = "Двоечники";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            //dataGridView1.Columns.Add(column);

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);


            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

            var zapros = (from mark in ds.Tables["Оценки"].AsEnumerable()
                          from student in ds.Tables["Студенты"].AsEnumerable()
                          from predmet in ds.Tables["Предметы"].AsEnumerable()

                          where mark.Field<Int32>("Код студента") == student.Field<Int32>("Код студента")

                          where(mark.Field<Int32>("Код предмета 2") == predmet.Field<Int32>("Код предмета") || mark.Field<Int32>("Код предмета 1") == predmet.Field<Int32>("Код предмета"))

                          where(mark.Field<Int32>("Оценка 1") == 2 || mark.Field<Int32>("Оценка 2") == 2)

                          select new
                          {
                              disciplina = predmet.Field<string>("Название предмета"),
                              countTwo = student.Field<string>("ФИО")
                          });
            
            foreach (var st in zapros)
            {
                dataGridView1.Rows.Add(st.disciplina, st.countTwo);
            }

        }

        private void специальностиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.специальностиBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentsDataSet);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
