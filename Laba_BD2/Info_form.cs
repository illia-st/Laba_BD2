using Laba_BD2.Data;

namespace Laba_BD2
{
    enum Query_Selection
    {
        Simple_1,
        Simple_2,
        Simple_3,
        Simple_4,
        Simple_5,
        Complicated_1,
        Complicated_2,
        Complicated_3
    }

    public partial class Info_form : Form
    {
        private Query_Selection _selection;
        private const int Def = 25;
        private int rows_ = 0;
        private int cols_ = 0;
        private void Create_DB(int c, int r)
        {
            dc.Rows.Clear();
            for (int j = 0; j < c; ++j)
            {
                var new_col = new DataGridViewColumn();
                string header = Make_ColHeader(j);
                new_col.HeaderText = header;
                new_col.Name = header;
                MyCell cella = new MyCell();
                new_col.CellTemplate = cella;
                dc.Columns.Add(new_col);
                ++cols_;
            }
            for (int i = 0; i < r; ++i)
            {
                var new_row = new DataGridViewRow();
                string header = Make_RowHeader(i);
                new_row.HeaderCell.Value = header;
                dc.Rows.Add(new_row);
                ++rows_;
            }
        }
        private string Make_ColHeader(int j)
        {
            const int alp = 26;
            string m = "";
            int n = j;
            if (j < alp)
            {
                char c = (char)(j + 65);
                return (m + c);
            }
            for (int i = j; i >= alp; i = j / alp)
            {
                int mod = i % alp;
                int div = i / alp - 1;
                m += (char)(div + 65);
                if (div < alp)
                {
                    m += (char)(mod + 65);
                }
            }
            return m;
        }
        private string Make_RowHeader(int i)
        {
            return Convert.ToString(i + 1);
        }
        private string SetCellName(int j, int i)
        {

            string name = Make_ColHeader(j) + Make_RowHeader(i);
            return name;
        }
        private void Change_label(string value)
        {
            Par_label.Text = value.Trim();
        }
        private void Fill_Cells(List<string> values, string attribute)
        {
            Create_DB(Def, Def);
            MyCell myCell = new MyCell();
            myCell.Value = attribute;
            dc.Rows[0].Cells[0] = myCell;
            for(int i = 0; i < values.Count(); ++i)
            {
                MyCell cell = new MyCell();
                cell.Value = values[i];
                dc.Rows[i + 1].Cells[0] = cell;
            }
        }
        private void Sub_btn_Click(object sender, EventArgs e)
        {
            var words = ParserOfAddedInfo.TrimValue(Par_box.Text);
            var result = new List<string>();
            DB connection = new DB();
            uint param = 1;
            switch (_selection)
            {
                case Query_Selection.Simple_1:
                    if (words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Simple_1(words);
                    Fill_Cells(result, "Logins");
                    break;
                case Query_Selection.Simple_2:

                    if (words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Simple_2(words);
                    Fill_Cells(result, "Жанри");
                    break;
                case Query_Selection.Simple_3:

                    if (words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Simple_3(words);
                    Fill_Cells(result, "Назви фільмів");
                    break;
                case Query_Selection.Simple_4:
                    param = 2;
                    if (words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Simple_4(words);
                    Fill_Cells(result, "Logins");
                    break;
                case Query_Selection.Simple_5:
                    if (words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Simple_5(words);
                    Fill_Cells(result, "Підписки");
                    break;
                case Query_Selection.Complicated_1:
                    if(words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Complicated_1(words);
                    Fill_Cells(result, "Logins");
                    break;
                case Query_Selection.Complicated_2:
                    if(words.Count() != 1)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Complicated_2(words);
                    Fill_Cells(result, "Logins");
                    break;
                case Query_Selection.Complicated_3:
                    if(words.Count() != param)
                    {
                        string msg = "Не вірна кількість параметрів. Має бути " + param.ToString();
                        throw new Exception(msg);
                    }
                    //call db method
                    result = connection.Complicated_3(words);
                    Fill_Cells(result, "Підписки");
                    break;
            }
        }
        public Info_form()
        {
            InitializeComponent();
            Login_film_x.Checked = true;
            Change_label("Введіть назву фільму ->");
            _selection = Query_Selection.Simple_1;
            Create_DB(Def, Def);
        }
        private void Login_film_x_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву фільму ->");
            _selection = Query_Selection.Simple_1;
        }
        private void genre_sub_x_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву підписки ->");
            _selection = Query_Selection.Simple_2;
        }
        private void movie_prise_x_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть ціну підписки ->");
            _selection = Query_Selection.Simple_3;
        }
        private void login_likeX_HaveY_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву фільму та назву підписки ->");
            _selection = Query_Selection.Simple_4;
        }
        private void UserSub_likeX_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву фільму ->");
            _selection = Query_Selection.Simple_5;
        }
        private void Com_1_btn_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву жанру");
            _selection = Query_Selection.Complicated_1;
        }
        private void Com_2_btn_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву підписки");
            _selection = Query_Selection.Complicated_2;
        }
        private void Com_3_btn_CheckedChanged(object sender, EventArgs e)
        {
            Change_label("Введіть назву підписки");
            _selection = Query_Selection.Complicated_3;
        }
        private void info__login_film_x_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логіни тих юзерів, що мають тип підписки, в яких міститься фільм Х", "Запит 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void genre_sub_x_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Жанри фільму, що містяться в підписці Х", "Запит 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Movie_prise_x_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Фільми, що знаходяться в підписках, ціна яких менша за Х", "Запит 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void login_LikeX_HaveY_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логіни юзерів, яким сподобався фільм Х та мають підписку Y", "Запит 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UserSub_likeX_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Назви підписок користувачів, яким сподобався фільм Х", "Запит 5", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Com_1_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логіни юзерів, яким сподобались всі фільми жанру Х", "Множинний запит 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Com_2_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Логіни юзерів, яким сподобались всі фільми підписки Х", "Множинний запит 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Com_3_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Назви підписок, що мають таку ж кількість фільмів, що і підписка Х", "Множинний запит 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
