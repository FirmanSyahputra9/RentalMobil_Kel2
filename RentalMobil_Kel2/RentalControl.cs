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
    public partial class RentalControl : UserControl
    {
        private Form1 parentForm;
        public RentalControl(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            boxSettings();
            LoadDataGrid();
            FillComboBox();
        }

        private void boxSettings()
        {
            this.textBox2.ReadOnly = true;
            this.textBox3.ReadOnly = true;
            this.textBox4.ReadOnly = true;
            this.textBox6.ReadOnly = true;
            this.textBox7.ReadOnly = true;
            this.textBox8.ReadOnly = true;
            this.textBox9.ReadOnly = true;
            this.textBox10.ReadOnly = true;
            this.dateTimePicker2.Enabled = false;

        }

        private void LoadDataGrid()
        {
            DataTable data = parentForm.GetRentalData();

            dataGridView1.DataSource = data;
            dataGridView1.Refresh();

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void FillComboBox()
        {
            DataTable dt = parentForm.GetUserData();

            DataView view = new DataView(dt);
            DataTable dtUser = view.ToTable(true, "NIK", "NAMA", "ALAMAT", "NO_HP");

            comboBox1.DataSource = dtUser;
            comboBox1.DisplayMember = "NIK";
            comboBox1.ValueMember = "NIK";

            DataTable dtMobil = parentForm.GetCarReadyData();

            DataView viewMobil = new DataView(dtMobil);
            DataTable dtNopol = viewMobil.ToTable(true, "id", "NOPOL", "MEREK", "TIPE", "TAHUN", "HARGA", "Status");
            comboBox2.DataSource = dtNopol;
            comboBox2.DisplayMember = "NOPOL";
            comboBox2.ValueMember = "id";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
                return;

            string nik = comboBox1.SelectedValue.ToString();

            DataTable dt = parentForm.GetUserData();

            DataRow[] userRows = dt.Select($"NIK = '{nik}'");

            if (userRows.Length > 0)
            {
                textBox2.Text = userRows[0]["NAMA"].ToString();
                textBox3.Text = userRows[0]["ALAMAT"].ToString();
                textBox4.Text = userRows[0]["NO_HP"].ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue == null)
                return;

            string car_id_str = comboBox2.SelectedValue.ToString();

            if (!int.TryParse(car_id_str, out int car_id_int))
            {
                return;
            }

            DataTable dt = parentForm.GetCarData();

            DataRow[] carRows = dt.Select($"id = {car_id_int}");

            if (carRows.Length > 0)
            {
                textBox6.Text = carRows[0]["MEREK"].ToString();
                textBox7.Text = carRows[0]["TIPE"].ToString();
                textBox8.Text = carRows[0]["TAHUN"].ToString();
                textBox9.Text = Convert.ToString(carRows[0]["HARGA"]);
                textBox10.Text = carRows[0]["Status"].ToString();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date >= DateTime.Now.Date)
            {
                dateTimePicker2.Enabled = true;
                TimeSpan lama_rental = dateTimePicker2.Value.Date - dateTimePicker1.Value.Date;
                this.textBox13.Text = lama_rental.TotalDays.ToString() + " hari";
            }

            else
            {
                dateTimePicker2.Enabled = false;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show("Tanggal pengembalian tidak boleh sebelum tanggal rental.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker2.Value = dateTimePicker1.Value;
                btnRental.Enabled = false;
            }
            else
            {
                TimeSpan lama_rental = dateTimePicker2.Value.Date - dateTimePicker1.Value.Date;
                this.textBox13.Text = lama_rental.TotalDays.ToString() + " hari";
            }
        }

        private void btnReSum_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox9.Text))
            {
                MessageBox.Show("Harga sewa mobil tidak tersedia.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.textBox13.Text))
            {
                MessageBox.Show("Pilih Ulang Tanggal.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (!double.TryParse(textBox9.Text, out double hargaSewa))
            {
                MessageBox.Show("Format harga sewa tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker2.Value.Date > dateTimePicker1.Value.Date && hargaSewa >= 0)
            {
                textBox13_TextChanged(sender, e);
            }

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            TimeSpan lama_rental = dateTimePicker2.Value.Date - dateTimePicker1.Value.Date;
            this.textBox13.Text = lama_rental.TotalDays.ToString();
            if (dateTimePicker1.Value.Date >= DateTime.Now.Date && dateTimePicker2.Value.Date > dateTimePicker1.Value.Date && lama_rental != null && double.TryParse(this.textBox9.Text, out double hargaSewa))
            {
                this.textBox14.Text = "Rp " + (lama_rental.TotalDays * hargaSewa).ToString();
            }
        }

        private void btnRental_Click(object sender, EventArgs e)
        {
            string nama, alamat, no_hp, merk, type, tahun, harga, status, user_id, nopol;
            int nikInt, nopolInt, statusInt;
            DateTime rental_date, rental_return;
            TimeSpan lama_rental;

            user_id = comboBox1.SelectedValue?.ToString();
            nopol = comboBox2.SelectedValue?.ToString();
            nama = this.textBox2.Text;
            alamat = this.textBox3.Text;
            no_hp = this.textBox4.Text;
            merk = this.textBox6.Text;
            type = this.textBox7.Text;
            tahun = this.textBox8.Text;
            harga = this.textBox9.Text;
            rental_date = this.dateTimePicker1.Value;
            rental_return = this.dateTimePicker2.Value;
            lama_rental = rental_return - rental_date;

            status = this.textBox10.Text;
            if (status == "Tersedia")
            {
                statusInt = 0;
            }
            else
            {
                return;
            }


            if (string.IsNullOrWhiteSpace(user_id) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(nopol) || string.IsNullOrWhiteSpace(alamat) || string.IsNullOrWhiteSpace(no_hp) || string.IsNullOrWhiteSpace(merk) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(tahun) || string.IsNullOrWhiteSpace(harga) || string.IsNullOrWhiteSpace(status) || rental_date.Date < DateTime.Now.Date)
            {
                MessageBox.Show("isi Data Dengan Benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = parentForm.CreateRentalData(user_id, nopol, rental_date, rental_return, statusInt);

            if (success)
            {
                ClearFields();
                LoadDataGrid();
                MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGrid();

            }


        }
        public void ClearFields()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Enabled = false;
            textBox13.Clear();
            textBox14.Clear();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
            ClearFields();

        }
    }
}
