using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace MotorStar
{
    public partial class Dashboard : Form
    {
        private accountDetails ad;
        private bool _draging = false;
        private Point _start_point = new Point(0, 0);
        public string userName;
        dbConnection conn = new dbConnection();

        private customerAddUpdateDelete custAddUpdateDelete;

        public Dashboard(string name)
        {
            InitializeComponent();
            userName = name;          
        }

        private void guna2Panel4_MouseHover(object sender, EventArgs e)
        {
            guna2Panel4.BackColor =  Color.FromArgb(0, 149, 146);
        }

        private void guna2Panel4_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel4.BackColor = Color.FromArgb(244,244,244);
        }

        private void guna2Panel5_MouseHover(object sender, EventArgs e)
        {
            guna2Panel5.BackColor = Color.FromArgb(0, 149, 146);
        }

        private void guna2Panel5_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel5.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void guna2Panel6_MouseHover(object sender, EventArgs e)
        {
            guna2Panel6.BackColor = Color.FromArgb(0, 149, 146);
        }

        private void guna2Panel6_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel6.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            guna2Panel7.BackColor = Color.FromArgb(0, 149, 146);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel7.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void guna2Panel8_MouseHover(object sender, EventArgs e)
        {
            guna2Panel8.BackColor = Color.FromArgb(0, 149, 146);
        }

        private void guna2Panel8_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel8.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void guna2Panel9_MouseHover(object sender, EventArgs e)
        {
            guna2Panel9.BackColor = Color.FromArgb(0, 149, 146);
        }

        private void guna2Panel9_MouseLeave(object sender, EventArgs e)
        {
            guna2Panel9.BackColor = Color.FromArgb(244, 244, 244);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            

            label22.Text = userName;
            populateDashbourd();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            _draging = true;
            _start_point = new Point(e.X, e.Y);
            this.Opacity = .8;
        }

        private void guna2Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void guna2Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            _draging = false;
            this.Opacity = 1;
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void populateDashbourd()
        {
            MySqlConnection connection = conn.GetConnection();
            string sql = "Select overdue.total AS overdue, advance.total as advance, deliquent.total from tblaccount t1 LEFT JOIN(select count(strPaymentStatus) total from tblaccount where strPaymentStatus = 'Overdue') overdue ON 1 = 1 LEFT JOIN(select count(strPaymentStatus) total from tblaccount where strPaymentStatus = 'Advance Payer') advance ON 1 = 1 LEFT JOIN(select count(strPaymentStatus) total from tblaccount where strPaymentStatus = 'Deliquent Payer') deliquent ON 1 = 1";
            MySqlCommand command = new MySqlCommand(sql, connection);


            connection.Open();


            MySqlDataReader da = command.ExecuteReader();
            if (da.Read())
            {
                txtAdvance.Text = da.GetValue(1).ToString();
                txtOverdue.Text = da.GetValue(0).ToString();
                txtDeliquent.Text = da.GetValue(2).ToString();

                chart1.Series["s1"].IsValueShownAsLabel = true;
                chart1.Series["s1"].Points.AddXY("Advance Payer", da.GetValue(1).ToString());
                chart1.Series["s1"].Points.AddXY("Overdue Payer", da.GetValue(0).ToString());
                chart1.Series["s1"].Points.AddXY("Deliquent Payer", da.GetValue(2).ToString());
            }
            connection.Close();
        }
        public void PopulateAccount()
        {
            dgvAccount.Rows.Clear();
            MySqlConnection connection = conn.GetConnection();
            string sql = "SELECT A.intAccountId, C.strFirstName, A.intMaxTerm, A.dblTotalPaid, A.dblRemainingBalance, U.strUnitName, A.dtmDateAccountCreated FROM tblaccount A LEFT JOIN tblcustomer C ON C.intCustomerID = A.intCustomerId LEFT JOIN tblunit U on U.intUnitId = A.intUnitId";
            MySqlCommand command = new MySqlCommand(sql, connection);
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            try
            {

                //LOOP THRU DT
                foreach (DataRow row in dt.Rows)
                {
                    dgvAccount.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                }
                //CLEAR DT
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }
        private void guna2Panel7_Click(object sender, EventArgs e)
        {
            PopulateAccount();
            dgvAccount.Columns[0].Width = 50;
            pnlDashboard.Hide();
            pnlAccount.Show();
        }

        private void guna2Panel4_Click(object sender, EventArgs e)
        {
            pnlDashboard.Show();
            pnlAccount.Hide();
        }
        
        private void guna2Button1_Click(object sender, EventArgs e)
        {                
            if (ad == null || ad.IsDisposed)
            {
                ad = new accountDetails();                
                ad.Show();
            }
            else
            {
                ad.Focus();
            }
        }

        private void pnlCustomer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (custAddUpdateDelete == null || custAddUpdateDelete.IsDisposed)
            {
                custAddUpdateDelete = new customerAddUpdateDelete("Add");
                custAddUpdateDelete.Show();
            }
            else
            {
                custAddUpdateDelete.Focus();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (custAddUpdateDelete == null || custAddUpdateDelete.IsDisposed)
            {
                custAddUpdateDelete = new customerAddUpdateDelete("Delete");
                custAddUpdateDelete.Show();
            }
            else
            {
                custAddUpdateDelete.Focus();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (custAddUpdateDelete == null || custAddUpdateDelete.IsDisposed)
            {
                custAddUpdateDelete = new customerAddUpdateDelete("Show");
                custAddUpdateDelete.Show();
            }
            else
            {
                custAddUpdateDelete.Focus();
            }
        }

        private void EnableButtons()
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnShow.Enabled = true;
        }

        private void DisabaleButtons()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnShow.Enabled = false;
        }
    }

}
