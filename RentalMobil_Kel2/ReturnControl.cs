using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentalMobil_Kel2
{
    public partial class ReturnControl : UserControl
    {
        private Form1 parentForm;
        public ReturnControl(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            LoadDataGrid();
            boxSettings();
            FillComboBox();
        }
        private void boxSettings()
        {
            this.comboBox1.Enabled = true;
            this.textBox2.ReadOnly = true;
            this.textBox3.ReadOnly = true;
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker3.Enabled = false;
            this.textBox6.ReadOnly = true;
            this.textBox7.ReadOnly = true;
        }

        private void FillComboBox()
        {
            DataTable dt = parentForm.GetRentalingData();

            DataView view = new DataView(dt);
            DataTable dtUser = view.ToTable(true, "ID_MOBIL", "NAMA", "NOPOL", "HARGA", "TANGGAL_PINJAM", "TANGGAL_KEMBALI");
            comboBox1.DataSource = dtUser;
            comboBox1.DisplayMember = "NAMA";
            comboBox1.ValueMember = "ID_MOBIL";
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
                return;

            if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int car_id_int))
                return;

            DataTable dt = parentForm.GetRentalingData();
            DataRow[] carRows = dt.Select($"ID_MOBIL = {car_id_int}");

            if (carRows.Length == 0)
                return;

            DataRow row = carRows[0];

            textBox2.Text = row["NAMA"].ToString();
            textBox3.Text = row["HARGA"].ToString();

            dateTimePicker1.Value = Convert.ToDateTime(row["TANGGAL_PINJAM"]);
            dateTimePicker2.Value = Convert.ToDateTime(row["TANGGAL_KEMBALI"]);
            dateTimePicker3.Value = DateTime.Now.Date;

            TimeSpan lama_rental = dateTimePicker2.Value.Date - dateTimePicker1.Value.Date;
            double hariRental = lama_rental.TotalDays;
            textBox6.Text = hariRental.ToString();


            decimal hargaMobil = Convert.ToDecimal(row["HARGA"]);


            TimeSpan terlambatSpan = dateTimePicker3.Value.Date - dateTimePicker2.Value.Date;
            double hariTelat = terlambatSpan.TotalDays;


            decimal denda = 0;

            if (hariTelat > 0)
            {
                denda = (hargaMobil * (decimal)hariTelat) / 10;
            }

            textBox7.Text = "Rp " + denda.ToString("N0");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string carId = comboBox1.SelectedValue?.ToString();
            if (string.IsNullOrWhiteSpace(carId))
            {
                MessageBox.Show("Pilih data rental terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DateTime tglPinjam = dateTimePicker1.Value.Date;
            DateTime tglKembali = dateTimePicker2.Value.Date;
            DateTime tglSekarang = DateTime.Now.Date;

            TimeSpan terlambatSpan = tglSekarang - tglKembali;
            int hariTelat = terlambatSpan.Days;

            if (!decimal.TryParse(textBox3.Text, out decimal hargaMobil))
            {
                MessageBox.Show("Harga mobil tidak valid.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal denda = 0;
            if (hariTelat > 0)
            {
                denda = (hargaMobil * hariTelat) / 10;
            }

            textBox7.Text = "Rp " + denda.ToString("N0");

            DialogResult dr = MessageBox.Show(
                $"Yakin ingin mengembalikan mobil?\nDenda: Rp {denda:N0}",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr != DialogResult.Yes)
                return;

            bool success = parentForm.ReturnCar(Convert.ToInt32(carId),
                tglPinjam,
                tglKembali,
                tglSekarang,
                denda
            );

            if (success)
            {
                MessageBox.Show("Mobil berhasil dikembalikan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                LoadDataGrid();
            }
            else
            {
                MessageBox.Show("Gagal mengembalikan mobil.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearFields()
        {
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            dateTimePicker3.Value = DateTime.Now.Date;
            textBox6.Clear();
            textBox7.Clear();
        }





        private void LoadDataGrid()
        {
            DataTable data = parentForm.GetRentalData();

            dataGridView1.DataSource = data;
            dataGridView1.Refresh();

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

  
    }
}
