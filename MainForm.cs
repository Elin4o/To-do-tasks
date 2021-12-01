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
    public partial class MainForm : Form
    {
        DataTable dt;
        SqlDataAdapter sda;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.tasksTableAdapter.Fill(this.tasksDataSet.Tasks);

            //showData обновява datagridview-а след всяко вмъкване,редактиране и изтриване. 
            ShowData();
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

        }
        //showData за ръчно обновяване.
        public void ShowData()
        {
            SqlConnection sc = new SqlConnection
            {
                ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True")
            };

            sda = new SqlDataAdapter("SELECT * FROM Tasks",sc);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        //Отваря нова форма,където добавяме нов запис.
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            Add_Task_Form f1 = new Add_Task_Form();
            f1.Show();
            this.Hide();
        }

        //Отваря нова форма,където обновяваме запис.
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            //Проверява дали datagridview-а е празен и не позволява отварянето,ако е .
            if (dataGridView1.Rows.Count != 0)
            {
                UpdateForm uf = new UpdateForm(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                uf.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Empty selection,please add tasks !");
            }

        }
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //Проверява дали datagridview-а е празен и не позволява изпълнението,ако е .
            if (dataGridView1.Rows.Count != 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this task ?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    SqlConnection sc = new SqlConnection();
                    SqlCommand com = new SqlCommand();
                    sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True");
                    sc.Open();

                    com.Connection = sc;
                    com.CommandText = @"DELETE FROM Tasks WHERE id = (@id)";
                    com.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value);

                    com.ExecuteNonQuery();


                    sc.Close();

                    MessageBox.Show("Task deleted!");
                    ShowData();
                }
            }
            else
            {
                MessageBox.Show("Empty selection,nothing to delete.Please add tasks !");
            }

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DoneTasksForm dtf = new DoneTasksForm();
            dtf.Show();
            this.Hide();
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void Btn_Done_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string taskValue = Convert.ToString(selectedRow.Cells[2].Value);
                string dateValue = Convert.ToString(selectedRow.Cells[3].Value);
                DateTime dateTime = DateTime.Parse(dateValue);

                //добавя
                SqlConnection sc = new SqlConnection();
                SqlCommand com = new SqlCommand();
                //Локация на базата от данни,вероятно трябва да замените локацията с мястото,където сте запазили файла V V V.
                sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True");
                sc.Open();

                com.Connection = sc;
                com.CommandText = @"INSERT INTO DoneTasks (Task,Date) VALUES (@task, @date)";
                com.Parameters.AddWithValue("@task", taskValue);
                com.Parameters.AddWithValue("@date", dateTime);
                com.ExecuteNonQuery();

                sc.Close();
                //трие
                SqlConnection sc1 = new SqlConnection();
                SqlCommand com1 = new SqlCommand();
                sc1.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True");
                sc1.Open();

                com1.Connection = sc1;
                com1.CommandText = @"DELETE FROM Tasks WHERE id = (@id)";
                com1.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value);

                com1.ExecuteNonQuery();


                sc1.Close();
                ShowData();
                MessageBox.Show("Task succesfully done!");
            }           
        }
        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //Взема информацията от таблицата
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string taskValue = Convert.ToString(selectedRow.Cells[2].Value);
                string dateValue = Convert.ToString(selectedRow.Cells[3].Value);
                DateTime dateTime = DateTime.Parse(dateValue);

                //добавя
                SqlConnection sc = new SqlConnection();
                SqlCommand com = new SqlCommand();
                //Локация на базата от данни,вероятно трябва да замените локацията с мястото,където сте запазили файла V V V.
                sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True");
                sc.Open();

                com.Connection = sc;
                com.CommandText = @"INSERT INTO DoneTasks (Task,Date) VALUES (@task, @date)";
                com.Parameters.AddWithValue("@task", taskValue);
                com.Parameters.AddWithValue("@date", dateTime);
                com.ExecuteNonQuery();

                sc.Close();
                //трие
                // SqlConnection sc1 = new SqlConnection();
                // SqlCommand com1 = new SqlCommand();
                sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True");
                sc.Open();

                com.Connection = sc;
                com.CommandText = @"DELETE FROM Tasks WHERE id = (@id)";
                com.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value);

                com.ExecuteNonQuery();


                sc.Close();
                ShowData();

                MessageBox.Show("Task succesfully done!");
            }
        }

    }
}
