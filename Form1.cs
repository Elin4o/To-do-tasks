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

namespace ToDoTask
{
    public partial class add_Task_Form : Form
    {
        public add_Task_Form()
        {
            InitializeComponent();
            /*Не позволява да се избира отминала дата, но е позволено при 
            обновяването,защото се появи error при избирането на същия ден.*/
            this.dateTimePicker1.MinDate = System.DateTime.Now;
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            //Нов запис в базата от данни
            if (textBox1.Text != "" && dateTimePicker1.Text != "")
            {
                SqlConnection sc = new SqlConnection();
                SqlCommand com = new SqlCommand();
                //Локация на базата от данни,вероятно трябва да замените локацията с мястото,където сте запазили файла V V V.
                sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.1\\DatabaseTasks.mdf;Integrated Security=True");
                sc.Open();

                com.Connection = sc;
                com.CommandText = @"INSERT INTO Tasks (Task,Date) VALUES (@task, @date)";
                com.Parameters.AddWithValue("@task", textBox1.Text);
                com.Parameters.AddWithValue("@date", dateTimePicker1.Value);

                com.ExecuteNonQuery();

                sc.Close();
                MessageBox.Show("Task added!");
                MainForm mf = new MainForm();
                mf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You haven't added a task/date!");
            }
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }
    }
}
