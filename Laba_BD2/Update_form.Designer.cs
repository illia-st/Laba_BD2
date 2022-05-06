namespace Laba_BD2
{
    partial class Update_form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Par_label = new System.Windows.Forms.Label();
            this.Enter_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Par_box = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Delete_Genre = new System.Windows.Forms.RadioButton();
            this.Update_Sub_Prise = new System.Windows.Forms.RadioButton();
            this.Delete_Sub_Radio = new System.Windows.Forms.RadioButton();
            this.Delete_Movie = new System.Windows.Forms.RadioButton();
            this.Update_Movie_Sub_Radio = new System.Windows.Forms.RadioButton();
            this.Update_Movie_Prise_Radio = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Delete_label = new System.Windows.Forms.Label();
            this.Update_label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Par_label);
            this.panel1.Controls.Add(this.Enter_label);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 59);
            this.panel1.TabIndex = 0;
            // 
            // Par_label
            // 
            this.Par_label.AutoSize = true;
            this.Par_label.Location = new System.Drawing.Point(442, 20);
            this.Par_label.Name = "Par_label";
            this.Par_label.Size = new System.Drawing.Size(50, 20);
            this.Par_label.TabIndex = 2;
            this.Par_label.Text = "label2";
            // 
            // Enter_label
            // 
            this.Enter_label.AutoSize = true;
            this.Enter_label.Location = new System.Drawing.Point(348, 20);
            this.Enter_label.Name = "Enter_label";
            this.Enter_label.Size = new System.Drawing.Size(60, 20);
            this.Enter_label.TabIndex = 1;
            this.Enter_label.Text = "Введіть";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Par_box);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(527, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 59);
            this.panel2.TabIndex = 0;
            // 
            // Par_box
            // 
            this.Par_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Par_box.Location = new System.Drawing.Point(0, 0);
            this.Par_box.Multiline = true;
            this.Par_box.Name = "Par_box";
            this.Par_box.Size = new System.Drawing.Size(273, 59);
            this.Par_box.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Delete_Genre);
            this.panel3.Controls.Add(this.Update_Sub_Prise);
            this.panel3.Controls.Add(this.Delete_Sub_Radio);
            this.panel3.Controls.Add(this.Delete_Movie);
            this.panel3.Controls.Add(this.Update_Movie_Sub_Radio);
            this.panel3.Controls.Add(this.Update_Movie_Prise_Radio);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 391);
            this.panel3.TabIndex = 1;
            // 
            // Delete_Genre
            // 
            this.Delete_Genre.AutoSize = true;
            this.Delete_Genre.Location = new System.Drawing.Point(531, 219);
            this.Delete_Genre.Name = "Delete_Genre";
            this.Delete_Genre.Size = new System.Drawing.Size(137, 24);
            this.Delete_Genre.TabIndex = 3;
            this.Delete_Genre.TabStop = true;
            this.Delete_Genre.Text = "Видалити жанр";
            this.Delete_Genre.UseVisualStyleBackColor = true;
            this.Delete_Genre.CheckedChanged += new System.EventHandler(this.Delete_Genre_CheckedChanged);
            // 
            // Update_Sub_Prise
            // 
            this.Update_Sub_Prise.AutoSize = true;
            this.Update_Sub_Prise.Location = new System.Drawing.Point(90, 265);
            this.Update_Sub_Prise.Name = "Update_Sub_Prise";
            this.Update_Sub_Prise.Size = new System.Drawing.Size(196, 24);
            this.Update_Sub_Prise.TabIndex = 3;
            this.Update_Sub_Prise.TabStop = true;
            this.Update_Sub_Prise.Text = "Оновитит ціну підписки";
            this.Update_Sub_Prise.UseVisualStyleBackColor = true;
            this.Update_Sub_Prise.CheckedChanged += new System.EventHandler(this.Update_Sub_Prise_CheckedChanged);
            // 
            // Delete_Sub_Radio
            // 
            this.Delete_Sub_Radio.AutoSize = true;
            this.Delete_Sub_Radio.Location = new System.Drawing.Point(527, 265);
            this.Delete_Sub_Radio.Name = "Delete_Sub_Radio";
            this.Delete_Sub_Radio.Size = new System.Drawing.Size(160, 24);
            this.Delete_Sub_Radio.TabIndex = 1;
            this.Delete_Sub_Radio.TabStop = true;
            this.Delete_Sub_Radio.Text = "Видалити підписку";
            this.Delete_Sub_Radio.UseVisualStyleBackColor = true;
            this.Delete_Sub_Radio.CheckedChanged += new System.EventHandler(this.Delete_Sub_Radio_CheckedChanged);
            // 
            // Delete_Movie
            // 
            this.Delete_Movie.AutoSize = true;
            this.Delete_Movie.Location = new System.Drawing.Point(531, 173);
            this.Delete_Movie.Name = "Delete_Movie";
            this.Delete_Movie.Size = new System.Drawing.Size(141, 24);
            this.Delete_Movie.TabIndex = 2;
            this.Delete_Movie.TabStop = true;
            this.Delete_Movie.Text = "Видалити фільм";
            this.Delete_Movie.UseVisualStyleBackColor = true;
            this.Delete_Movie.CheckedChanged += new System.EventHandler(this.Delete_Movie_CheckedChanged);
            // 
            // Update_Movie_Sub_Radio
            // 
            this.Update_Movie_Sub_Radio.AutoSize = true;
            this.Update_Movie_Sub_Radio.Location = new System.Drawing.Point(90, 219);
            this.Update_Movie_Sub_Radio.Name = "Update_Movie_Sub_Radio";
            this.Update_Movie_Sub_Radio.Size = new System.Drawing.Size(207, 24);
            this.Update_Movie_Sub_Radio.TabIndex = 2;
            this.Update_Movie_Sub_Radio.TabStop = true;
            this.Update_Movie_Sub_Radio.Text = "Оновити підписку фільму";
            this.Update_Movie_Sub_Radio.UseVisualStyleBackColor = true;
            this.Update_Movie_Sub_Radio.CheckedChanged += new System.EventHandler(this.Update_Movie_Sub_Radio_CheckedChanged);
            // 
            // Update_Movie_Prise_Radio
            // 
            this.Update_Movie_Prise_Radio.AutoSize = true;
            this.Update_Movie_Prise_Radio.Location = new System.Drawing.Point(90, 173);
            this.Update_Movie_Prise_Radio.Name = "Update_Movie_Prise_Radio";
            this.Update_Movie_Prise_Radio.Size = new System.Drawing.Size(176, 24);
            this.Update_Movie_Prise_Radio.TabIndex = 1;
            this.Update_Movie_Prise_Radio.TabStop = true;
            this.Update_Movie_Prise_Radio.Text = "Оновити ціну фільму";
            this.Update_Movie_Prise_Radio.UseVisualStyleBackColor = true;
            this.Update_Movie_Prise_Radio.CheckedChanged += new System.EventHandler(this.Update_Movie_Prise_Radio_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.Delete_label);
            this.panel5.Controls.Add(this.Update_label);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(800, 87);
            this.panel5.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(370, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(72, 87);
            this.panel4.TabIndex = 4;
            // 
            // Delete_label
            // 
            this.Delete_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Delete_label.Dock = System.Windows.Forms.DockStyle.Right;
            this.Delete_label.Location = new System.Drawing.Point(442, 0);
            this.Delete_label.Name = "Delete_label";
            this.Delete_label.Size = new System.Drawing.Size(358, 87);
            this.Delete_label.TabIndex = 0;
            this.Delete_label.Text = "Видалення інформації";
            this.Delete_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Update_label
            // 
            this.Update_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Update_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Update_label.Location = new System.Drawing.Point(0, 0);
            this.Update_label.Name = "Update_label";
            this.Update_label.Size = new System.Drawing.Size(370, 87);
            this.Update_label.TabIndex = 0;
            this.Update_label.Text = "Оновлення інформації";
            this.Update_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Update_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Update_form";
            this.Text = "Update_form";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label Par_label;
        private Label Enter_label;
        private Panel panel2;
        private TextBox Par_box;
        private Panel panel3;
        private Panel panel5;
        private Label Update_label;
        private Label Delete_label;
        private RadioButton Update_Sub_Prise;
        private RadioButton Update_Movie_Sub_Radio;
        private RadioButton Update_Movie_Prise_Radio;
        private RadioButton Delete_Genre;
        private RadioButton Delete_Movie;
        private RadioButton Delete_Sub_Radio;
        private Panel panel4;
    }
}