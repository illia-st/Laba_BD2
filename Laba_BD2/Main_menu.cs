using System.Data;

namespace Laba_BD2
{
    public partial class Main_menu : Form
    {
        public Main_menu()
        {
            InitializeComponent();
        }

        private void Info_btn_Click(object sender, EventArgs e)
        {
            Info_form form = new Info_form();
            form.Show();
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            Update_form form = new Update_form();
            form.Show();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            Add_form form = new Add_form();
            form.Show();
        }
    }
}