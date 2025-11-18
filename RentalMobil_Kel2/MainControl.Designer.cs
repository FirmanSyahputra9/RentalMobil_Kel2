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
            BeRent = new Label();
            SuspendLayout();
            // 
            // BeRent
            // 
            BeRent.AutoSize = true;
            BeRent.Font = new Font("Cooper Black", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BeRent.ImageAlign = ContentAlignment.MiddleLeft;
            BeRent.Location = new Point(128, 311);
            BeRent.Name = "BeRent";
            BeRent.Size = new Size(646, 46);
            BeRent.TabIndex = 0;
            BeRent.Text = "SELAMAT DATANG DI BeRent";
            BeRent.TextAlign = ContentAlignment.TopCenter;
            BeRent.Click += BeRent_Click;
            // 
            // MainControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Aquamarine;
            Controls.Add(BeRent);
            Name = "MainControl";
            Size = new Size(890, 768);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label BeRent;
    }
}
