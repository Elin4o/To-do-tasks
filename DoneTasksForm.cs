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
    public partial class DoneTasksForm : Form
    {
        DataTable dt;
        SqlDataAdapter sda;
        public DoneTasksForm()
        {
            InitializeComponent();
        }

        private void BtnToMainForm_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void DoneTasksForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseTasksDataSet.DoneTasks' table. You can move, or remove it, as needed.
            ShowData();

        }
        public void ShowData()
        {
            SqlConnection sc = new SqlConnection
            {
                ConnectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Repos\\ToDoTask v.2\\DatabaseTasks.mdf;Integrated Security=True")
            };

            sda = new SqlDataAdapter("SELECT * FROM DoneTasks", sc);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
