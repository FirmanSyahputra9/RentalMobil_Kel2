namespace RentalMobil_Kel2
{
    partial class SidebarUserControl
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
            groupBox1 = new GroupBox();
            ExitNav = new Button();
            AddUserNav = new Button();
            AddCarNav = new Button();
            ReturnNav = new Button();
            RentalNav = new Button();
            AuthNav = new Button();
            pictureBox2 = new PictureBox();
            HomeNav = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightGray;
            groupBox1.Controls.Add(ExitNav);
            groupBox1.Controls.Add(AddUserNav);
            groupBox1.Controls.Add(AddCarNav);
            groupBox1.Controls.Add(ReturnNav);
            groupBox1.Controls.Add(RentalNav);
            groupBox1.Controls.Add(AuthNav);
            groupBox1.Location = new Point(32, 128);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(252, 633);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // ExitNav
            // 
            ExitNav.BackColor = SystemColors.ControlDarkDark;
            ExitNav.ForeColor = Color.WhiteSmoke;
            ExitNav.Location = new Point(26, 519);
            ExitNav.Name = "ExitNav";
            ExitNav.Size = new Size(200, 93);
            ExitNav.TabIndex = 5;
            ExitNav.Text = "ExitNav";
            ExitNav.UseVisualStyleBackColor = false;
            // 
            // AddUserNav
            // 
            AddUserNav.BackColor = SystemColors.ControlDarkDark;
            AddUserNav.ForeColor = Color.WhiteSmoke;
            AddUserNav.Location = new Point(26, 420);
            AddUserNav.Name = "AddUserNav";
            AddUserNav.Size = new Size(200, 93);
            AddUserNav.TabIndex = 4;
            AddUserNav.Text = "AddUserNav";
            AddUserNav.UseVisualStyleBackColor = false;
            // 
            // AddCarNav
            // 
            AddCarNav.BackColor = SystemColors.ControlDarkDark;
            AddCarNav.ForeColor = Color.WhiteSmoke;
            AddCarNav.Location = new Point(26, 321);
            AddCarNav.Name = "AddCarNav";
            AddCarNav.Size = new Size(200, 93);
            AddCarNav.TabIndex = 3;
            AddCarNav.Text = "AddCarNav";
            AddCarNav.UseVisualStyleBackColor = false;
            // 
            // ReturnNav
            // 
            ReturnNav.BackColor = SystemColors.ControlDarkDark;
            ReturnNav.ForeColor = Color.WhiteSmoke;
            ReturnNav.Location = new Point(26, 222);
            ReturnNav.Name = "ReturnNav";
            ReturnNav.Size = new Size(200, 93);
            ReturnNav.TabIndex = 2;
            ReturnNav.Text = "ReturnNav";
            ReturnNav.UseVisualStyleBackColor = false;
            // 
            // RentalNav
            // 
            RentalNav.BackColor = SystemColors.ControlDarkDark;
            RentalNav.ForeColor = Color.WhiteSmoke;
            RentalNav.Location = new Point(26, 123);
            RentalNav.Name = "RentalNav";
            RentalNav.Size = new Size(200, 93);
            RentalNav.TabIndex = 1;
            RentalNav.Text = "RentalNav";
            RentalNav.UseVisualStyleBackColor = false;
            RentalNav.Click += RentalNav_Click;
            // 
            // AuthNav
            // 
            AuthNav.BackColor = SystemColors.ControlDarkDark;
            AuthNav.ForeColor = Color.WhiteSmoke;
            AuthNav.Location = new Point(26, 24);
            AuthNav.Name = "AuthNav";
            AuthNav.Size = new Size(200, 93);
            AuthNav.TabIndex = 0;
            AuthNav.Text = "AuthNav";
            AuthNav.UseVisualStyleBackColor = false;
            AuthNav.Click += AuthNav_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(104, 48);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(125, 62);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // HomeNav
            // 
            HomeNav.Location = new Point(16, 8);
            HomeNav.Name = "HomeNav";
            HomeNav.Size = new Size(41, 29);
            HomeNav.TabIndex = 5;
            HomeNav.Text = "HomeNav";
            HomeNav.UseVisualStyleBackColor = true;
            HomeNav.Click += HomeNav_Click;
            // 
            // SidebarUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            Controls.Add(groupBox1);
            Controls.Add(pictureBox2);
            Controls.Add(HomeNav);
            Name = "SidebarUserControl";
            Size = new Size(300, 768);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button ExitNav;
        private Button AddUserNav;
        private Button AddCarNav;
        private Button ReturnNav;
        private Button RentalNav;
        private Button AuthNav;
        private PictureBox pictureBox2;
        private Button HomeNav;
    }
}
