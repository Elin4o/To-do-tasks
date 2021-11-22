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
    public partial class UpdateForm : Form
    {
        public string id;
        public UpdateForm(string value)
        {
            InitializeComponent();
            id = value;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            //Попълва textbox-а и dateTimePicker-а със съответния избор от предходната форма.
            SqlConnection sc = new SqlConnection();
            SqlCommand com = new SqlCommand();
            sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.1\\DatabaseTasks.mdf;Integrated Security=True");
            sc.Open();

            com.Connection = sc;
            com.CommandText = @"SELECT Task,Date FROM Tasks WHERE ID=@ID";
            com.Parameters.AddWithValue("@ID", id);

            SqlDataReader dr = com.ExecuteReader();
            while(dr.Read())
            {
                txb_Task.Text = dr.GetValue(0).ToString();
                dateTimePicker.Value = (DateTime)dr.GetValue(1);
            }

            sc.Close();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            //Обновява записа в базата.
            if (txb_Task.Text != "" && dateTimePicker.Text != "")
            {
                SqlConnection sc = new SqlConnection();
                SqlCommand com = new SqlCommand();
                sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.1\\DatabaseTasks.mdf;Integrated Security=True");
                sc.Open();

                com.Connection = sc;
                com.CommandText = @"UPDATE Tasks set Task=@task,Date=@date WHERE ID=@ID";
                com.Parameters.AddWithValue("@task", txb_Task.Text);
                com.Parameters.AddWithValue("@date", dateTimePicker.Value);
                com.Parameters.AddWithValue("@ID", id);

                com.ExecuteNonQuery();

                sc.Close();

                MessageBox.Show("Task updated!");

                MainForm mf = new MainForm();
                mf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Your task field is empty !");
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
