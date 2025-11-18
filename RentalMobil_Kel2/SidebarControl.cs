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
    public partial class SidebarControl : UserControl
    {
        public SidebarControl()
        {
            InitializeComponent();
        }
        public event EventHandler<string> NavigationRequested;

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

        private void ReturnNav_Click_1(object sender, EventArgs e)
        {
            NavigationRequested?.Invoke(this, "ReturnControl");
        }

        private void AddCarNav_Click(object sender, EventArgs e)
        {
            NavigationRequested?.Invoke(this, "AddCar");
        }

        private void AddUserNav_Click(object sender, EventArgs e)
        {
            NavigationRequested?.Invoke(this, "AddUser");
        }

        private void ExitNav_Click(object sender, EventArgs e)
        {
            NavigationRequested?.Invoke(this, "Exit");
        }

        public void SetNavigationVisibility(bool isLoggin)
        {
            if (isLoggin)
            {
                AuthNav.Visible = true;
                ReturnNav.Visible = true;
                AddCarNav.Visible = true;
                AddUserNav.Visible = true;
                RentalNav.Visible = true;
                ReturnNav.Visible = true;
                ExitNav.Visible = true;
                HomeNav.Visible = true;

                AuthNav.Text = "Logout";
            }
            else
            {
                AuthNav.Visible = true;
                RentalNav.Visible = false;
                ReturnNav.Visible = false;
                AddCarNav.Visible = false;
                AddUserNav.Visible = false;
                AuthNav.Text = "Login";
            }
        }

   
    }
}
