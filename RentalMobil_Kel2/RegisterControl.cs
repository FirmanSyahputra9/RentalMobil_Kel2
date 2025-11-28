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
    public partial class RegisterControl : UserControl
    {
        private Form1 parentForm;
        public RegisterControl(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            textBox4.UseSystemPasswordChar = true;
            textBox5.UseSystemPasswordChar = true;
            button1.Text = "🙈";
            button2.Text = "🙈";
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {

            string id_user, nama, username, password, kon_password, type;
            bool status;

            id_user = this.textBox1.Text;
            nama = this.textBox2.Text;
            username = this.textBox3.Text;
            password = this.textBox4.Text;
            kon_password = this.textBox5.Text;
            status = false;
            type = "user";


            if (string.IsNullOrWhiteSpace(id_user) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(kon_password))
            {
                MessageBox.Show("Semua kolom data mobil harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (parentForm.IsUserIdExist(id_user))
            {
                MessageBox.Show("ID User sudah digunakan! Harap gunakan ID lain.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != kon_password)
            {
                MessageBox.Show("Konfirmasi password tidak cocok.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = parentForm.Register(id_user, nama, username, password, type, status);

            if (success)
            {
                MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
            }

        }

        private void ClearInputFields()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (this.Parent.Parent is AuthControl authContainer)
            {
                authContainer.LoadLogin();
            }
            else
            {
                MessageBox.Show("Error: Tidak dapat menemukan wadah AuthControl.", "Kesalahan Navigasi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.UseSystemPasswordChar)
            {
                textBox4.UseSystemPasswordChar = false;
                button1.Text = "🫣";
            }
            else
            {
                textBox4.UseSystemPasswordChar = true;
                button1.Text = "🙈";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.UseSystemPasswordChar)
            {
                textBox5.UseSystemPasswordChar = false;
                button2.Text = "🫣";
            }
            else
            {
                textBox5.UseSystemPasswordChar = true;
                button2.Text = "🙈";
            }
        }
    }
}
