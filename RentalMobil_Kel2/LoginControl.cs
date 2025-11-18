using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RentalMobil_Kel2;

namespace RentalMobil_Kel2
{
    public partial class LoginControl : UserControl
    {
        private Form1 parentForm;
        private RegisterControl Register;
        public LoginControl(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Parent.Parent is AuthControl authContainer)
            {
                authContainer.LoadRegister();
            }
            else
            {
                MessageBox.Show("Error: Tidak dapat menemukan panel AuthControl.", "Kesalahan Navigasi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong.", "Peringatan");
                return;
            }

            int authResult = parentForm.AuthenticateUser(username, password);

            if (authResult == 1)
            {
                MessageBox.Show($"Selamat datang, {username}!", "Login Berhasil");
                parentForm.LoginSuccess();
            }
            else if (authResult == 0)
            {
                MessageBox.Show("Akun Anda tidak aktif. Hubungi admin!", "Akses Ditolak");
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Username atau Password salah.", "Login Gagal");
                textBox2.Clear();
            }
        }
    }
}
