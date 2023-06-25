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
    public partial class accountDetails : Form
    {
        dbConnection conn = new dbConnection();
        private customerList customerForm;
        private unitList unitForm;
        public accountDetails()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (customerForm == null || customerForm.IsDisposed)
            {
                customerForm = new customerList();
                customerForm.CustomerSelected += CustomerForm_CustomerSelected;
                customerForm.Show();
            }
            else
            {
                customerForm.Focus();
            }
        }
        private void CustomerForm_CustomerSelected(string customerName)
        {
            
            MySqlConnection connection = conn.GetConnection();
            string sql = "SELECT intCustomerId,strFirstName,strMiddleName,strLastName,dtmBirthday,strPhoneNumber,strEmail,strOccupation,intSalary,strSourceOfIncome,strAddress FROM tblCustomer WHERE intCustomerId = @id";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", customerName);


            connection.Open();


            MySqlDataReader da = command.ExecuteReader();
            if (da.Read())
            {
                txtCustomerId.Text = da.GetValue(0).ToString();
                txtFirstName.Text = da.GetValue(1).ToString();
                txtMiddleName.Text = da.GetValue(2).ToString();
                txtLastName.Text = da.GetValue(3).ToString();
                txtBirthday.Text = da.GetValue(4).ToString();
                txtPhone.Text =  da.GetValue(5).ToString();
                txtEmail.Text = da.GetValue(6).ToString();
                txtOccupation.Text = da.GetValue(7).ToString();
                txtSalary.Text = da.GetValue(8).ToString();
                txtSourceOfIncome.Text = da.GetValue(9).ToString();
                txtAddress.Text = da.GetValue(10).ToString();
            }

            connection.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (unitForm == null || unitForm.IsDisposed)
            {
                unitForm = new unitList();
                unitForm.UnitSelected += UnitForm_UnitSelected;
                unitForm.Show();
            }
            else
            {
                unitForm.Focus();
            }
        }

        private void UnitForm_UnitSelected(string unitName)
        {
            MySqlConnection connection = conn.GetConnection();
            string sql = "SELECT intUnitId,strUnitName,strUnitType,strYearModel,strColor,strEngineNumber,strChassisNumber,strMVFileNumber,strUnitPrice FROM tblUnit WHERE intUnitId = @id";

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", unitName);


            connection.Open();


            MySqlDataReader da = command.ExecuteReader();
            if (da.Read())
            {
                txtUnitId.Text = da.GetValue(0).ToString();
                txtUnit.Text = da.GetValue(1).ToString();
                txtType.Text = da.GetValue(2).ToString();
                txtModel.Text = da.GetValue(3).ToString();
                txtColor.Text = da.GetValue(4).ToString();
                txtEngineNumber.Text = da.GetValue(5).ToString();
                txtChassisNumber.Text = da.GetValue(6).ToString();
                txtMVFileNumber.Text = da.GetValue(7).ToString();
                txtPrice.Text = da.GetValue(8).ToString();
            }

            connection.Close();
        }

        private void RetrieveEmployeeName()
        {
            string f_name = "";
            string m_name = "";
            string l_name = "";
            
            MySqlConnection connection = conn.GetConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT strFirstName, strMiddleName, strLastName FROM tblemployee WHERE intEmployeeId=@ID", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@ID", int.Parse(txtEmployeeId.SelectedItem.ToString()));
            MySqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                f_name = da.GetValue(0).ToString();
                m_name = da.GetValue(1).ToString();
                l_name = da.GetValue(2).ToString();
            }
            txtEmployeeName.Text = f_name + " " + m_name + " " + l_name;
            da.Close();
            connection.Close();
        }

        private void InsertCustomer()
        {
            MySqlConnection connection = conn.GetConnection();
            //SQL STMT
            string sql = "INSERT INTO tblaccount (intDownPayment, strTermsOfPayment, intMaxTerm, dblTotalPaid, dblRemainingBalance, dtmDateAccountCreated, strStatus, intEmployeeId, intCustomerId, intUnitId) VALUES (@DownPayment, @TermsOfPayment, @MaxtTerm, @TotalPaid, @RemainingBalance, @DateAccountCreated, @Status, @EmployeeId, @CustomerId, @UnitId)";
            MySqlCommand cmd = new MySqlCommand(sql, connection);

            int RemainingBalance = Convert.ToInt32(txtMaxTerm.Text) - Convert.ToInt32(txtDownPayment.Text);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@DownPayment", txtDownPayment.Text);
            cmd.Parameters.AddWithValue("@TermsOfPayment", txtTermsOfPayment.Text);
            cmd.Parameters.AddWithValue("@MaxTerm", txtMaxTerm.Text);
            cmd.Parameters.AddWithValue("@TotalPaid", txtDownPayment.Text);
            cmd.Parameters.AddWithValue("@RemainingBalance", RemainingBalance);
            cmd.Parameters.AddWithValue("@DateAccountCreated", DateTime.Now.ToString("MM-dd-yyyy"));
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeId.Text);
            cmd.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
            cmd.Parameters.AddWithValue("@UnitId", txtUnitId.Text);

            //OPEN CON AND EXEC insert       
            connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Successfully Inserted");
                connection.Close();
            }
            else
            {
                MessageBox.Show("Not Inserted");
                connection.Close();
                return;
            }
            connection.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
