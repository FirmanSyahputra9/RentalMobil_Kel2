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
    public partial class AddUserControl : UserControl
    {
        private Form1 parentForm;
        public AddUserControl()
        {
            InitializeComponent();
        }
        public AddUserControl(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            button6.Text = "🙈";
            LoadDataGrid();
            this.textBox1.PlaceholderText = "Cari nama";
            FillComboBox();
            textBox5.UseSystemPasswordChar = true;

        }

        private void ClearInputFields()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
            this.textBox6.Clear();
            this.textBox7.Clear();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
            this.textBox2.Enabled = true;
        }

        private void FillComboBox()
        {

            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("user");
            this.comboBox1.Items.Add("admin");
            this.comboBox1.SelectedIndex = 0;

            this.comboBox2.Items.Clear();
            this.comboBox2.Items.Add("Non-Aktif");
            this.comboBox2.Items.Add("Aktif");
            this.comboBox2.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox5.UseSystemPasswordChar)
            {
                textBox5.UseSystemPasswordChar = false;
                button6.Text = "🫣";
            }
            else
            {
                textBox5.UseSystemPasswordChar = true;
                button6.Text = "🙈";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string id_user, nama, username, password, kon_password, type, alamat, no_hp;
            int statusInt;

            id_user = this.textBox2.Text;
            nama = this.textBox3.Text;
            username = this.textBox4.Text;
            password = this.textBox5.Text;
            statusInt = this.comboBox2.SelectedIndex;
            type = this.comboBox1.Text;
            alamat = this.textBox6.Text;
            no_hp = this.textBox7.Text;


            if (string.IsNullOrWhiteSpace(id_user) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(alamat) || string.IsNullOrWhiteSpace(no_hp))
            {
                MessageBox.Show("Semua kolom data mobil harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool statusBool = (statusInt == 1);
            if (textBox2.Enabled == true)
            {
                bool success = parentForm.Register(id_user, nama, username, password, type, statusBool, alamat, no_hp);

                if (success)
                {
                    MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid();
                    ClearInputFields();
                }
            }
            if (textBox2.Enabled == false)
            {
                bool success = parentForm.updateUser(id_user, nama, username, password, type, statusBool, alamat, no_hp);
                if (success)
                {
                    MessageBox.Show("Update berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid();
                    ClearInputFields();
                    this.textBox2.Enabled = true;
                }
            }

        }

        private void LoadDataGrid()
        {
            DataTable data = parentForm.GetUserData();

            dataGridView1.DataSource = data;
            dataGridView1.Refresh();

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            LoadDataGrid();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ClearInputFields();

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                try
                {
                    this.textBox2.Text = selectedRow.Cells["NIK"].Value.ToString();
                    this.textBox3.Text = selectedRow.Cells["NAMA"].Value.ToString();
                    this.textBox4.Text = selectedRow.Cells["USERNAME"].Value.ToString();
                    this.textBox5.Text = selectedRow.Cells["PASSWORD"].Value.ToString();
                    this.textBox6.Text = selectedRow.Cells["ALAMAT"].Value.ToString();
                    this.textBox7.Text = selectedRow.Cells["NO_HP"].Value.ToString();
                    this.comboBox1.Text = selectedRow.Cells["ROLE"].Value.ToString();

                    string roleText = selectedRow.Cells["STATUS"].Value.ToString();
                    if (roleText == "user")
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }
                    else if (roleText == "admin")
                    {
                        this.comboBox1.SelectedIndex = 1;
                    }

                    string statusText = selectedRow.Cells["STATUS"].Value.ToString();
                    if (statusText == "Non-Aktif")
                    {
                        this.comboBox2.SelectedIndex = 0;
                    }
                    else if (statusText == "Aktif")
                    {
                        this.comboBox2.SelectedIndex = 1;
                    }
                    this.textBox2.Enabled = false;

                    MessageBox.Show("Data berhasil dimuat ke dalam formulir edit. Silahkan ubah data yang diperlukan, lalu klik tombol Simpan.", "Mode Edit Aktif", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal memuat data ke input. Pastikan nama kolom di DataGrid benar. Error: {ex.Message}", "Kesalahan Edit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Pilih satu baris data mobil untuk diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
