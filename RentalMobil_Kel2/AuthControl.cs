using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace RentalMobil_Kel2
{

    public partial class AuthControl : UserControl
    {
        private Form1 parentForm;

        public AuthControl(Form1 parentForm)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            LoadLogin();
        }

        private void LoadAuthControl(UserControl newControl)
        {

            panel1.Controls.Clear();

            newControl.Dock = DockStyle.Fill;

            panel1.Controls.Add(newControl);
        }

        public void LoadLogin()
        {
            LoginControl login = new LoginControl(this.parentForm);
            LoadAuthControl(login);
        }

        public void LoadRegister()
        {
            RegisterControl register = new RegisterControl(this.parentForm);
            LoadAuthControl(register);
        }

    }
}
