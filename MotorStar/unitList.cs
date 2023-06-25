using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MotorStar
{
    public partial class unitList : Form
    {
        dbConnection conn = new dbConnection();
        public unitList()
        {
            InitializeComponent();
        }
        public event Action<string> UnitSelected;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string x = dgvUnit.SelectedRows[0].Cells[0].Value.ToString();

            // Trigger the event to pass the customer name back to the account form
            UnitSelected?.Invoke(x);
            this.Close();
        }

        private void unitList_Load(object sender, EventArgs e)
        {
            loadUnit();
        }

        public void loadUnit()
        {
            dgvUnit.Rows.Clear();
            MySqlConnection connection = conn.GetConnection();
            string sql = "Select intUnitId, strUnitName, strUnitType, strYearModel, strColor, strStatus, strUnitPrice from tblunit";
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
                    dgvUnit.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
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
