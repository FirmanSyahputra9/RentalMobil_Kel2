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
        }

        private void ClearInputFields()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
            this.comboBox1.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void FillComboBox()
        {

            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("user");
            this.comboBox1.Items.Add("admin");
            this.comboBox1.SelectedIndex = 0;
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
            bool status;

            id_user = this.textBox2.Text;
            nama = this.textBox3.Text;
            username = this.textBox4.Text;
            password = this.textBox5.Text;
            status = false;
            type = this.comboBox1.Text;
            alamat = this.textBox6.Text;
            no_hp = this.textBox7.Text;


            if (string.IsNullOrWhiteSpace(id_user) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(alamat) || string.IsNullOrWhiteSpace(no_hp))
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

            bool success = parentForm.Register(id_user, nama, username, password, type, status, alamat, no_hp);

            if (success)
            {
                MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGrid();
                ClearInputFields();
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
                    this.textBox5.Text = selectedRow.Cells["ROLE"].Value.ToString();
                    this.textBox6.Text = selectedRow.Cells["STATUS"].Value.ToString();
                    this.textBox7.Text = selectedRow.Cells["ALAMAT"].Value.ToString();
                       
                    //string tahunProduksi = selectedRow.Cells["TAHUN"].Value.ToString();
                    //int yearIndex = this.comboBox1.Items.IndexOf(tahunProduksi);
                    //if (yearIndex != -1)
                    //{
                    //    this.comboBox1.SelectedIndex = yearIndex;
                    //}
                    //else
                    //{
                    //    this.comboBox1.SelectedIndex = 0;
                    //}

                    //string statusText = selectedRow.Cells["Status"].Value.ToString();
                    //if (statusText == "Tersedia")
                    //{
                    //    this.comboBox2.SelectedIndex = 0;
                    //}
                    //else if (statusText == "Keluar" || statusText == "Keluar")
                    //{
                    //    this.comboBox2.SelectedIndex = 1;
                    //}

                    //string showText = selectedRow.Cells["Tampil"].Value.ToString();
                    //if (showText == "Ya")
                    //{
                    //    this.comboBox3.SelectedIndex = 0;
                    //}
                    //else if (showText == "Tidak")
                    //{
                    //    this.comboBox3.SelectedIndex = 1;
                    //}

                    //this.textBox1.Enabled = false;
                    //string carCodeForImage = selectedRow.Cells["KODE"].Value.ToString();
                    //byte[] imageBytes = parentForm.GetCarImageByCode(carCodeForImage);
                    //if (imageBytes != null && imageBytes.Length > 0)
                    //{
                    //    carImage = imageBytes;
                    //    using (MemoryStream ms = new MemoryStream(imageBytes))
                    //    {
                    //        pictureBox1.Image = Image.FromStream(ms);
                    //        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    //    }
                    //}
                    //else
                    //{
                    //    pictureBox1.Image = null;
                    //}


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


        private void btnDel_Click(object sender, EventArgs e)
        {

        }
    }
}
