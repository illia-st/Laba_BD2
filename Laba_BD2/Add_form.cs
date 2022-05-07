using Laba_BD2.Data;

namespace Laba_BD2
{
    enum Adding
    {
        Genre,
        Movie,
        Sub_Plan,
        Like
    }

    public partial class Add_form : Form
    {
        Adding Option { get; set; }

        private void ShowItems(bool isBoth)
        {
            First_param_Label.Visible = true;
            First_box.Visible = true;
            if (isBoth)
            {
                Second_param_Label.Visible = true;
                Second_box.Visible = true;
            }
        }

        public Add_form()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                var connection = new DB();
                var value1 = First_box.Text;
                var value2 = Second_box.Text;
                switch (this.Option)
                {
                    case Adding.Genre:
                        var words = ParserOfAddedInfo.TrimValue(value1);
                        if (Equals(words.Count(), 1))
                        {
                            //call db method
                            connection.AddGenre(words.First());
                            break;
                        }
                        // кинути ексепшн
                        throw new Exception("Не коректно введені параметри в текстове поле");
                    case Adding.Movie:
                        words = ParserOfAddedInfo.TrimValue(value1, value2);
                        if (Equals(4, words.Count()))
                        {
                            //call db method
                            connection.AddMovie(words);
                            break;
                        }
                        // throw an exception
                        throw new Exception("Не коректно введені параметри в текстове поле");
                    case Adding.Like:
                        words = ParserOfAddedInfo.TrimValue(value1, value2);
                        if (Equals(2, words.Count()))
                        {
                            //call db method
                            connection.AddLike(words);
                            break;
                        }
                        throw new Exception("Не коректно введені параметри в текстове поле");
                    case Adding.Sub_Plan:
                        words = ParserOfAddedInfo.TrimValue(value1, value2);
                        if (Equals(2, words.Count()))
                        {
                            connection.AddSub(words);
                            break;
                        }
                        throw new Exception("Не коректно введені параметри в текстове поле");
                }
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Add_Genre_Radio_CheckedChanged(object sender, EventArgs e)
        {
            Option = Adding.Genre;
            this.ShowItems(isBoth: false);
            Second_param_Label.Visible = false;
            Second_box.Visible = false;
            First_param_Label.Text = "Введіть назву жанру";
        }

        private void Add_Sub_Radio_CheckedChanged(object sender, EventArgs e)
        {
            Option = Adding.Sub_Plan;
            this.ShowItems(isBoth: true);
            First_param_Label.Text = "Введіть назву підписки";
            Second_param_Label.Text = "Введіть ціну підписки";
        }

        private void Add_Movie_Radio_CheckedChanged(object sender, EventArgs e)
        {
            Option = Adding.Movie;
            this.ShowItems(isBoth: true);
            First_param_Label.Text = "Введіть назву фільму та його жанр(через кому)";
            Second_param_Label.Text = "Введіть ціну фільму та назву підписки, в якій фільм буде знаходитись";
        }

        private void Add_Like_Radio_CheckedChanged(object sender, EventArgs e)
        {
            Option = Adding.Like;
            this.ShowItems(isBoth: true);
            First_param_Label.Text = "Введіть логін коричтувача";
            Second_param_Label.Text = "Введіть назву фільму";
        }
    }
}
