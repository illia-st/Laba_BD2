namespace Laba_BD2
{
    partial class Main_menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Add_btn = new System.Windows.Forms.Button();
            this.Update_btn = new System.Windows.Forms.Button();
            this.Info_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Add_btn
            // 
            this.Add_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.Add_btn.Location = new System.Drawing.Point(0, 0);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(271, 118);
            this.Add_btn.TabIndex = 0;
            this.Add_btn.Text = "Додати до БД";
            this.Add_btn.UseVisualStyleBackColor = true;
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // Update_btn
            // 
            this.Update_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Update_btn.Location = new System.Drawing.Point(0, 246);
            this.Update_btn.Name = "Update_btn";
            this.Update_btn.Size = new System.Drawing.Size(271, 130);
            this.Update_btn.TabIndex = 1;
            this.Update_btn.Text = "Редагувати записи";
            this.Update_btn.UseVisualStyleBackColor = true;
            this.Update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // Info_btn
            // 
            this.Info_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Info_btn.Location = new System.Drawing.Point(0, 118);
            this.Info_btn.Name = "Info_btn";
            this.Info_btn.Size = new System.Drawing.Size(271, 128);
            this.Info_btn.TabIndex = 2;
            this.Info_btn.Text = "Переглянути інформацію";
            this.Info_btn.UseVisualStyleBackColor = true;
            this.Info_btn.Click += new System.EventHandler(this.Info_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Info_btn);
            this.panel1.Controls.Add(this.Add_btn);
            this.panel1.Controls.Add(this.Update_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 376);
            this.panel1.TabIndex = 3;
            // 
            // Main_menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 376);
            this.Controls.Add(this.panel1);
            this.Name = "Main_menu";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button Add_btn;
        private Button Update_btn;
        private Button Info_btn;
        private Panel panel1;
    }
}