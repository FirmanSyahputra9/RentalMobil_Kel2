namespace RentalMobil_Kel2
{
    partial class MainControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            BeRent = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // BeRent
            // 
            BeRent.AutoSize = true;
            BeRent.Font = new Font("Cooper Black", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BeRent.ImageAlign = ContentAlignment.MiddleLeft;
            BeRent.Location = new Point(242, 311);
            BeRent.Name = "BeRent";
            BeRent.Size = new Size(429, 46);
            BeRent.TabIndex = 0;
            BeRent.Text = "SELAMAT DATANG";
            BeRent.TextAlign = ContentAlignment.TopCenter;
            BeRent.Click += BeRent_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(222, 360);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(449, 152);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(357, 237);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(225, 71);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // MainControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(BeRent);
            Name = "MainControl";
            Size = new Size(890, 768);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label BeRent;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}
