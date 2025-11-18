namespace RentalMobil_Kel2
{
    partial class ReturnControl
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
            button1 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(510, 305);
            button1.Name = "button1";
            button1.Size = new Size(290, 124);
            button1.TabIndex = 0;
            button1.Text = "Retun";
            button1.UseVisualStyleBackColor = true;
            // 
            // ReturnControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button1);
            Name = "ReturnControl";
            Size = new Size(890, 768);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
    }
}
