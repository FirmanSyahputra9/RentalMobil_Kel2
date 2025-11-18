namespace RentalMobil_Kel2
{
    partial class SidebarControl
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
            HomeNav = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.HotTrack;
            groupBox1.Controls.Add(ExitNav);
            groupBox1.Controls.Add(AddUserNav);
            groupBox1.Controls.Add(AddCarNav);
            groupBox1.Controls.Add(ReturnNav);
            groupBox1.Controls.Add(RentalNav);
            groupBox1.Controls.Add(AuthNav);
            groupBox1.Location = new Point(40, 114);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(226, 642);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // ExitNav
            // 
            ExitNav.Location = new Point(13, 518);
            ExitNav.Name = "ExitNav";
            ExitNav.Size = new Size(200, 93);
            ExitNav.TabIndex = 5;
            ExitNav.Text = "ExitNav";
            ExitNav.UseVisualStyleBackColor = true;
            ExitNav.Click += ExitNav_Click;
            // 
            // AddUserNav
            // 
            AddUserNav.Location = new Point(13, 419);
            AddUserNav.Name = "AddUserNav";
            AddUserNav.Size = new Size(200, 93);
            AddUserNav.TabIndex = 4;
            AddUserNav.Text = "AddUserNav";
            AddUserNav.UseVisualStyleBackColor = true;
            AddUserNav.Click += AddUserNav_Click;
            // 
            // AddCarNav
            // 
            AddCarNav.Location = new Point(13, 320);
            AddCarNav.Name = "AddCarNav";
            AddCarNav.Size = new Size(200, 93);
            AddCarNav.TabIndex = 3;
            AddCarNav.Text = "AddCarNav";
            AddCarNav.UseVisualStyleBackColor = true;
            AddCarNav.Click += AddCarNav_Click;
            // 
            // ReturnNav
            // 
            ReturnNav.Location = new Point(13, 221);
            ReturnNav.Name = "ReturnNav";
            ReturnNav.Size = new Size(200, 93);
            ReturnNav.TabIndex = 2;
            ReturnNav.Text = "ReturnNav";
            ReturnNav.UseVisualStyleBackColor = true;
            ReturnNav.Click += ReturnNav_Click_1;
            // 
            // RentalNav
            // 
            RentalNav.Location = new Point(13, 122);
            RentalNav.Name = "RentalNav";
            RentalNav.Size = new Size(200, 93);
            RentalNav.TabIndex = 1;
            RentalNav.Text = "RentalNav";
            RentalNav.UseVisualStyleBackColor = true;
            RentalNav.Click += RentalNav_Click;
            // 
            // AuthNav
            // 
            AuthNav.Location = new Point(13, 23);
            AuthNav.Name = "AuthNav";
            AuthNav.Size = new Size(200, 93);
            AuthNav.TabIndex = 0;
            AuthNav.Text = "AuthNav";
            AuthNav.UseVisualStyleBackColor = true;
            AuthNav.Click += AuthNav_Click;
            // 
            // HomeNav
            // 
            HomeNav.Location = new Point(13, 13);
            HomeNav.Name = "HomeNav";
            HomeNav.Size = new Size(41, 29);
            HomeNav.TabIndex = 1;
            HomeNav.Text = "HomeNav";
            HomeNav.UseVisualStyleBackColor = true;
            HomeNav.Click += HomeNav_Click;
            // 
            // SidebarControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Highlight;
            Controls.Add(HomeNav);
            Controls.Add(groupBox1);
            Name = "SidebarControl";
            Size = new Size(300, 768);
            groupBox1.ResumeLayout(false);
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
        private Button HomeNav;
    }
}
