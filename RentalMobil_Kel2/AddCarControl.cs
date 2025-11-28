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
    public partial class AddCarControl : UserControl
    {
        private Form1 parentForm;

        public AddCarControl()
        {
            InitializeComponent();

        }
        public AddCarControl(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
            FillComboBox();
            LoadDataGrid();
            this.textBox1.ReadOnly = true;

        }

        private void LoadDataGrid()
        {
            DataTable data = parentForm.GetCarData();

            dataGridView1.DataSource = data;
            dataGridView1.Refresh();

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


        }

        private void FillComboBox()
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("-- Pilih Tahun --");
            int currentYear = DateTime.Now.Year;
            int startYear = 2000;

            for (int year = currentYear; year >= startYear; year--)
            {
                this.comboBox1.Items.Add(year.ToString());
            }

            this.comboBox1.SelectedIndex = 0;

            this.comboBox2.Items.Clear();
            this.comboBox2.Items.Add("Tersedia");
            this.comboBox2.Items.Add("Keluar");
            this.comboBox2.SelectedIndex = 0;
        }

        private void GenerateCarCode()
        {
            if (this.textBox1.Enabled)
            {
                string merk = this.textBox2.Text.ToUpper().Trim();
                string nopol = this.textBox4.Text.ToUpper().Trim();
                string tahunText = (this.comboBox1.SelectedIndex > 0) ? this.comboBox1.SelectedItem.ToString() : "0000";

                string initialPart = "";

                if (!string.IsNullOrEmpty(merk) && merk.Length >= 2)
                {
                    initialPart = merk.Substring(0, 1) + merk.Substring(merk.Length - 1, 1);
                }
                else if (!string.IsNullOrEmpty(merk) && merk.Length == 1)
                {
                    initialPart = merk + merk;
                }
                else
                {
                    initialPart = "XX"; 
                }

                int currentCount = parentForm.GetCarCountByMerk(merk) + 1;

                string generatedCode = $"{initialPart}{nopol}{tahunText.Substring(tahunText.Length - 2)}{currentCount:D3}";

                this.textBox1.Text = generatedCode.Replace(" ", "").Replace("-", ""); 
            }
        }


        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string code, merk, type, nopol, hargaText;
            int tahunIndex, statusIndex;
            int tahunProduksi, hargaSewa;
            bool statusTersedia;

            code = this.textBox1.Text;
            merk = this.textBox2.Text;
            type = this.textBox3.Text;
            tahunIndex = this.comboBox1.SelectedIndex;
            nopol = this.textBox4.Text;
            hargaText = this.textBox5.Text;
            statusIndex = this.comboBox2.SelectedIndex;

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(merk) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(nopol) || string.IsNullOrWhiteSpace(hargaText))
            {
                MessageBox.Show("Semua kolom data mobil harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(hargaText, out hargaSewa) || hargaSewa <= 0)
            {
                MessageBox.Show("Harga Sewa harus berupa angka positif.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tahunIndex <= 0 || this.comboBox1.SelectedItem == null || !int.TryParse(this.comboBox1.SelectedItem.ToString(), out tahunProduksi))
            {
                MessageBox.Show("Pilih Tahun Produksi yang valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (statusIndex < 0)
            {
                MessageBox.Show("Pilih Status mobil (Tersedia/Keluar).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            statusTersedia = (statusIndex == 0);

            bool success;
            string operation;

            if (this.textBox1.Enabled)
            {
                success = parentForm.SaveCarData(code, merk, type, tahunProduksi, nopol, hargaSewa, statusTersedia);
                operation = "ditambahkan";
            }
            else
            {

                success = parentForm.UpdateCarData(code, merk, type, tahunProduksi, nopol, hargaSewa, statusTersedia);
                operation = "diperbarui";
            }

            if (success)
            {
                MessageBox.Show($"Data mobil berhasil {operation}!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
                this.textBox1.Enabled = true;

                LoadDataGrid();
            }
            else
            {
                MessageBox.Show($"Gagal {operation} data mobil. Periksa koneksi atau kode Anda.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputFields()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.textBox1.Enabled = true;
            GenerateCarCode();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string carCode = selectedRow.Cells["KODE"].Value.ToString();
                string currentStatusText = selectedRow.Cells["Status"].Value.ToString();

                bool newStatus;
                string newStatusText;

                if (currentStatusText == "Tersedia")
                {
                    newStatus = false;
                    newStatusText = "Tidak Tersedia";
                }
                else
                {
                    newStatus = true;
                    newStatusText = "Tersedia";
                }

                DialogResult result = MessageBox.Show(
                    $"Apakah Anda yakin ingin mengubah status mobil KODE {carCode} dari '{currentStatusText}' menjadi '{newStatusText}'?",
                    "Konfirmasi Perubahan Status",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = parentForm.UpdateCarStatus(carCode, newStatus);

                    if (success)
                    {
                        MessageBox.Show($"Status mobil KODE {carCode} berhasil diubah menjadi '{newStatusText}'!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengubah status mobil.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih satu baris data mobil untuk diubah statusnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string carCode = selectedRow.Cells["KODE"].Value.ToString();
                string CurrenShowText = selectedRow.Cells["Tampil"].Value.ToString();
                bool newShow;
                string newShowText;
                if (CurrenShowText == "Ya")
                {
                    newShow = false;
                    newShowText = "Tidak";
                }
                else
                {
                    newShow = true;
                    newShowText = "Ya";
                }

                DialogResult result = MessageBox.Show(
                    $"Apakah Anda yakin ingin mengubah status tampil mobil KODE {carCode} dari '{CurrenShowText}' menjadi '{newShowText}'?",
                    "Konfirmasi Perubahan Status Tampil",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool success = parentForm.UpdateCarshow(carCode, newShow);
                    if (success)
                    {
                        MessageBox.Show($"Status tampil mobil KODE {carCode} berhasil diubah menjadi '{newShowText}'!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengubah status tampil mobil.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih satu baris data mobil untuk diubah status tampilnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
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

                    this.textBox1.Text = selectedRow.Cells["KODE"].Value.ToString();
                    this.textBox2.Text = selectedRow.Cells["MEREK"].Value.ToString();
                    this.textBox3.Text = selectedRow.Cells["TIPE"].Value.ToString();
                    this.textBox4.Text = selectedRow.Cells["NOPOL"].Value.ToString();
                    this.textBox5.Text = selectedRow.Cells["HARGA"].Value.ToString();

                    string tahunProduksi = selectedRow.Cells["TAHUN"].Value.ToString();
                    int yearIndex = this.comboBox1.Items.IndexOf(tahunProduksi);
                    if (yearIndex != -1)
                    {
                        this.comboBox1.SelectedIndex = yearIndex;
                    }
                    else
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }

                    string statusText = selectedRow.Cells["Status"].Value.ToString();
                    if (statusText == "Tersedia")
                    {
                        this.comboBox2.SelectedIndex = 0;
                    }
                    else if (statusText == "Tidak Tersedia" || statusText == "Keluar")
                    {
                        this.comboBox2.SelectedIndex = 1;
                    }

                    this.textBox1.Enabled = false;

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            GenerateCarCode();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateCarCode();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            GenerateCarCode();
        }
    }
}
