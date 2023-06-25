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
    public partial class customerList : Form
    {
        dbConnection conn = new dbConnection();
        public customerList()
        {
            InitializeComponent();
        }
        public event Action<string> CustomerSelected;
        private void customerList_Load(object sender, EventArgs e)
        {
            loadCustomer();
        }
        public string intCustomerId;
        private void guna2Button1_Click(object sender, EventArgs e)
        {

            string x = dgvCustomer.SelectedRows[0].Cells[0].Value.ToString();

            // Trigger the event to pass the customer name back to the account form
            CustomerSelected?.Invoke(x);
            this.Close();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvCustomer.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvCustomer.Rows[e.RowIndex];
                intCustomerId = selectedRow.Cells[0].Value.ToString();

                // Trigger the event to pass the customer name back to the account form
            }
        }
        public void loadCustomer()
        {
            dgvCustomer.Rows.Clear();
            MySqlConnection connection = conn.GetConnection();
            string sql = "Select intCustomerId,strFirstName, strAddress,strEmail,dtmBirthday,strPhoneNumber,strOccupation from tblCustomer";
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
                    dgvCustomer.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
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
    }
}
