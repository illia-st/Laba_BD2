using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_BD2
{
    enum Editing
    {
        Update_Movie_Prise,
        Update_Movie_Sub,
        Update_Sub_Prise,
        Delete_Movie,
        Delete_Sub,
        Delete_Genre
    }
    public partial class Update_form : Form
    {
        private Editing editing { get; set; }
        public Update_form()
        {
            InitializeComponent();
            editing = Editing.Update_Movie_Prise;
            Update_Movie_Prise_Radio.Checked = true;
            UpdateLabelValue();
        }
        private void UpdateLabelValue()
        {
            switch (editing)
            {
                case Editing.Update_Movie_Prise:
                    Par_label.Text = "назву фільму";
                    break;
                case Editing.Update_Movie_Sub:
                    Par_label.Text = "назву фільму та його нову підписку";
                    break;
                case Editing.Update_Sub_Prise:
                    Par_label.Text = "назву підписки та її нову ціну";
                    break;
                case Editing.Delete_Movie:
                    Par_label.Text = "назву фільму";
                    break;
                case Editing.Delete_Sub:
                    Par_label.Text = "назву підписки";
                    break;
                case Editing.Delete_Genre:
                    Par_label.Text = "назву жанру";
                    break;
            }
        }
        private void Update_Movie_Prise_Radio_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Update_Movie_Prise;
            UpdateLabelValue();
        }

        private void Update_Movie_Sub_Radio_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Update_Movie_Sub;
            UpdateLabelValue();
        }

        private void Update_Sub_Prise_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Update_Sub_Prise;
            UpdateLabelValue();
        }

        private void Delete_Movie_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Delete_Movie;
            UpdateLabelValue();
        }

        private void Delete_Genre_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Delete_Genre;
            UpdateLabelValue();
        }

        private void Delete_Sub_Radio_CheckedChanged(object sender, EventArgs e)
        {
            editing = Editing.Delete_Sub;
            UpdateLabelValue();
        }
    }
}
