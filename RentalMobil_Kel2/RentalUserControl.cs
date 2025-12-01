using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RentalMobil_Kel2
{
    public partial class RentalUserControl : UserControl
    {
        private Form1 parentForm;
        public RentalUserControl(Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            LoadCarCards();   
        }

        private void LoadCarCards()
        {
            flowLayoutPanel1.Controls.Clear();
            DataTable dt = parentForm.GetCarData();

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateCarCard(row);
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private Panel CreateCarCard(DataRow row)
        {
            Panel card = new Panel();
            card.Width = 250;
            card.Height = 330;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);

            PictureBox pic = new PictureBox();
            pic.Width = 230;
            pic.Height = 150;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Location = new Point(10, 10);

            string code = row["KODE"].ToString();
            byte[] imgBytes = parentForm.GetCarImageByCode(code);

            if (imgBytes != null)
            {
                using (var ms = new System.IO.MemoryStream(imgBytes))
                {
                    pic.Image = Image.FromStream(ms);
                }
            }

            Label lblMerk = new Label();
            lblMerk.Text = $"{row["MEREK"]} - {row["TIPE"]}";
            lblMerk.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblMerk.Location = new Point(10, 170);
            lblMerk.AutoSize = true;

            Label lblTahun = new Label();
            lblTahun.Text = "Tahun: " + row["TAHUN"].ToString();
            lblTahun.Location = new Point(10, 200);
            lblTahun.AutoSize = true;

            Label lblHarga = new Label();
            lblHarga.Text = "Harga: " + row["HARGA"].ToString();
            lblHarga.Location = new Point(10, 225);
            lblHarga.AutoSize = true;

            Label lblStatus = new Label();
            lblStatus.Text = "Status: " + row["Status"].ToString();
            lblStatus.Location = new Point(10, 250);
            lblStatus.AutoSize = true;

            card.Controls.Add(pic);
            card.Controls.Add(lblMerk);
            card.Controls.Add(lblTahun);
            card.Controls.Add(lblHarga);
            card.Controls.Add(lblStatus);

            return card;
        }
    }
}
