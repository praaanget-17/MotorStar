using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotorStar
{
    public partial class Login : Form
    {
        dbConnection conn = new dbConnection();
        MySqlConnection con = new MySqlConnection();
        DataTable dt = new DataTable();

        public Login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                Properties.Settings.Default.username = txtUsername.Text;
                Properties.Settings.Default.password = txtPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }
            if (login())
            {
                Dashboard dash = new Dashboard(getName());
                dash.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to login");
            }        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2CheckBox1.Checked = true;
            txtUsername.Text = Properties.Settings.Default.username;
            txtPassword.Text = Properties.Settings.Default.password;
        }

        public Boolean login()
        {
            MySqlConnection connection = conn.GetConnection();
            string username = txtUsername.Text; // Replace with the provided username
            string password = txtPassword.Text; // Replace with the provided password

            string sql = "SELECT COUNT(*) FROM tblemployee WHERE strEmail = @username AND strPassword = @password";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();

            int result = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string getName()
        {
            MySqlConnection connection = conn.GetConnection();
            string username = txtUsername.Text; // Replace with the provided username
            string password = txtPassword.Text; // Replace with the provided password

            string sql = "SELECT strFirstName FROM tblemployee WHERE strEmail = @username AND strPassword = @password";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();

            MySqlDataReader da = command.ExecuteReader();
            if (da.Read())
            {               
                return da.GetValue(0).ToString();               
            }
            else
            {         
                return "";
            }        
        }
    }
}
