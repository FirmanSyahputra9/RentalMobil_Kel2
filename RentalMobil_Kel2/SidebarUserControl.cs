using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalMobil_Kel2
{
    public partial class SidebarUserControl : UserControl
    {
        public SidebarUserControl()
        {
            InitializeComponent();
        }
        public event EventHandler<string> NavigationRequested;

        public void SetNavigationVisibility(bool isLoggin)
        {
            if (isLoggin)
            {
                AuthNav.Visible = true;
                ReturnNav.Visible = false;
                AddCarNav.Visible = false;
                AddUserNav.Visible = false;
                RentalNav.Visible = true;
                ReturnNav.Visible = false;
                ExitNav.Visible = true;
                HomeNav.Visible = true;


                AuthNav.Text = "Logout";
            }
            else
            {
                HomeNav.Visible = true;
                AuthNav.Visible = true;
                RentalNav.Visible = false;
                ReturnNav.Visible = false;
                AddCarNav.Visible = false;
                AddUserNav.Visible = false;
                AuthNav.Text = "Login";
            }
        }

        private void HomeNav_Click(object sender, EventArgs e)
        {

            NavigationRequested?.Invoke(this, "Home");

        }

        private void AuthNav_Click(object sender, EventArgs e)
        {
            if (AuthNav.Text == "Login")
            {
                NavigationRequested?.Invoke(this, "Auth");
            }
            else
            {
                NavigationRequested?.Invoke(this, "Logout");
            }
        }

        private void RentalNav_Click(object sender, EventArgs e)
        {
            NavigationRequested?.Invoke(this, "Rental");
        }
    }
}
