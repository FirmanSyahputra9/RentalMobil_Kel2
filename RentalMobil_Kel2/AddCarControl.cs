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
            LoadDataGrid();
            FillComboBox();
        }

        private void LoadDataGrid()
        {
            DataTable data = parentForm.GetCarData();
            dataGridView1.DataSource = data;

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


        private void btnAdd_Click(object sender, EventArgs e)
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

            bool success = parentForm.SaveCarData(code, merk, type, tahunProduksi, nopol, hargaSewa, statusTersedia);

            if (success)
            {
                MessageBox.Show("Data mobil berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
