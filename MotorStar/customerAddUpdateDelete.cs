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
    public partial class customerAddUpdateDelete : Form
    {
        public string ButtonAction1;
        public customerAddUpdateDelete(string ButtonAction)
        {
            InitializeComponent();
            ButtonAction1 = ButtonAction;   
        }

        private void customerAddUpdateDelete_Load(object sender, EventArgs e)
        {
            if (ButtonAction1 == "Add")
            {
                btnAdd.Visible = true;
            }
            else if (ButtonAction1 == "Update")
            {
                btnUpdate.Visible = true;
            }
            else if (ButtonAction1 == "Show")
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
            }
        }
    }
}
