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
                ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.1\\DatabaseTasks.mdf;Integrated Security=True")
            };

            sda = new SqlDataAdapter("SELECT * FROM Tasks",sc);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        //Отваря нова форма,където добавяме нов запис.
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            add_Task_Form f1 = new add_Task_Form();
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
                    sc.ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.1\\DatabaseTasks.mdf;Integrated Security=True");
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

    }
}
