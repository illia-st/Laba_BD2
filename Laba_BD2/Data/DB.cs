using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Laba_BD2.Data
{
    internal class DB
    {
        private SqlConnection connection = new SqlConnection("Data Source = DESKTOP-1B5F523; Initial Catalog=Laba_DB; Integrated Security = True;");
        private void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

        }
        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public void AddGenre(string value)
        {
            try
            {
                StringBuilder genre_name = new StringBuilder();
                genre_name.Append(value.Trim().ToLower());
                genre_name[0] = char.ToUpper(genre_name[0]);
                string find_Genre = "select id from Genre where genre_name = @Genre_name";
                SqlCommand Genre_cm = new SqlCommand(find_Genre, connection);
                Genre_cm.Parameters.AddWithValue("@Genre_name", genre_name.ToString());
                
                this.OpenConnection();
                int? genre_id = null;
                ReadValue(Genre_cm, out genre_id);

                if(genre_id != null)
                {
                    throw new Exception("Такий жанр вже існує в БД");
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                string sqlCommand =
                    "insert into Genre (genre_name) values(@Genre_name)";
                Genre_cm = new SqlCommand(sqlCommand, connection);
                Genre_cm.Parameters.AddWithValue("@Genre_name", genre_name.ToString());
                this.OpenConnection();
                sqlDataAdapter.InsertCommand = Genre_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви додали жанр " + genre_name.ToString();
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void AddSub(List<string> values)
        {
            try
            {
                StringBuilder sub = new StringBuilder();
                uint prise = new uint();

                sub.Append(values.First().Trim().ToLower());
                sub[0] = char.ToUpper(sub[0]);
                if (!UInt32.TryParse(values.Last(), out prise))
                {
                    throw new Exception("Введіть валідне значення ціни(число)");
                }
                string find_SubId = "select id from Sub_Plan where sub_name = @Sub_name";
                SqlCommand SubId_cm = new SqlCommand(find_SubId, connection);
                SubId_cm.Parameters.AddWithValue("@Sub_name", sub.ToString());

                int? sub_id = null;

                this.OpenConnection();
                ReadValue(SubId_cm, out sub_id);
                if (sub_id != null)
                {
                    throw new Exception("Така підписка вже існує в БД");
                }
                string insertSub = "insert into Sub_Plan(sub_name, prise) values(@Sub_name, @Prise)";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                SqlCommand insert_cm = new SqlCommand(insertSub, connection);

                insert_cm.Parameters.AddWithValue("@Sub_name", sub.ToString());
                insert_cm.Parameters.AddWithValue("@Prise", Convert.ToInt32(prise));

                sqlDataAdapter.InsertCommand = insert_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви додали підписку " + sub;
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
        }
        private void ReadValue(SqlCommand com, out int? value)
        {
            value = null;
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    value = Convert.ToInt32(reader["id"]);
                }
            }
        }
        public void AddMovie(List<string> values)
        {
            try
            {
                StringBuilder genre = new StringBuilder()
                    , movie_name = new StringBuilder()
                    , sub_name = new StringBuilder();
                genre.Append(values[1].Trim().ToLower());
                genre[0] = char.ToUpper(genre[0]);
                movie_name.Append(values.First().Trim().ToLower());
                movie_name[0] = char.ToUpper(movie_name[0]);
                sub_name.Append(values.Last().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                uint movie_prise = new uint();
                if (!UInt32.TryParse(values[2], out movie_prise))
                {
                    throw new Exception("Введіть валідне значення ціни(число)");
                }
                // написати вирази
                string getGenreId = "select id from Genre where genre_name = @Genre_name";
                string getSubId = "select id from Sub_Plan where sub_name = @Sub_name";
                string checkIfFilmExist = "select id from Movie where name = @Film_name";
                // вставити значення у вирази
                int? GenreId = null, SubId = null, MovieId = null;

                SqlCommand GetGenreId_cm = new SqlCommand(getGenreId, connection)
                    , GetSubId_cm = new SqlCommand(getSubId, connection)
                    , GetMovieIf_cm = new SqlCommand(checkIfFilmExist, connection);

                GetGenreId_cm.Parameters.AddWithValue("@Genre_name", genre.ToString());
                GetSubId_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                GetMovieIf_cm.Parameters.AddWithValue("@Film_Name", movie_name.ToString());

                this.OpenConnection();

                ReadValue(GetGenreId_cm, out GenreId);
                ReadValue(GetSubId_cm, out SubId);
                ReadValue(GetMovieIf_cm, out MovieId);

                if (!Equals(MovieId, null))
                {
                    throw new Exception("Такий фільм вже існує в БД");
                }
                if(Equals(SubId, null))
                {
                    throw new Exception("Такої підписки не існує в БД");
                }
                if (Equals(GenreId, null))
                {
                    throw new Exception("Такого жанру не існує в БД");
                }
                if (!Equals(SubId, 5) && movie_prise != 0)
                {
                    throw new Exception("Фільм з підпискою не може мати власної ціни,\n " +
                        "Введіть в назві підписки \"without sub\", щоб додати фільм з окремою ціною.");
                }
                if (Equals(SubId, 5) && Equals(movie_prise, 0))
                {
                    throw new Exception("Фільми без підписки не може бути безкоштовним,\n " +
                        "Введіть в назві підписки \"Base plan\" та ціну 0, щоб додати безкоштовний фільм.");
                }
                string insertCommand = "insert into Movie(name, prise, sub_id, genre_id) values(@Name, @Prise, @Sub_id, @Genre_id)";
                SqlCommand Insert_cm = new SqlCommand(insertCommand, connection);
                Insert_cm.Parameters.AddWithValue("@Name", movie_name.ToString());
                Insert_cm.Parameters.AddWithValue("@Prise", Convert.ToInt32(movie_prise));
                Insert_cm.Parameters.AddWithValue("@Sub_id", SubId);
                Insert_cm.Parameters.AddWithValue("@Genre_id", GenreId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Insert_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви додали фільм " + movie_name.ToString();
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void AddLike(List<string> values)
        {
            try
            {
                StringBuilder login = new StringBuilder(), movie_name= new StringBuilder();
                login.Append(values.First().Trim());
                movie_name.Append(values.Last().Trim());

                string find_UserId = "select id from User_Info where login = @Login";
                string find_MovieId = "select id from Movie where name = @Movie_name";

                int? login_id = null, movie_id = null, like_id = null;

                SqlCommand UserID_cm = new SqlCommand(find_UserId, connection)
                    , MovieID_cm = new SqlCommand(find_MovieId, connection);
                UserID_cm.Parameters.AddWithValue("@Login", login.ToString());
                MovieID_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());

                this.OpenConnection();

                ReadValue(UserID_cm, out login_id);
                ReadValue(MovieID_cm, out movie_id);
                
                if(login_id == null)
                {
                    throw new Exception("Немає такого користувача");
                }
                if(movie_id == null)
                {
                    throw new Exception("Немає такого фільму");
                }

                string find_like = "select id from liked_film where user_id = @User_id and film_id = @Movie_id ";
                SqlCommand Like_cm = new SqlCommand(find_like, connection);

                Like_cm.Parameters.AddWithValue("@User_id", login_id);
                Like_cm.Parameters.AddWithValue("@Movie_id", movie_id);

                ReadValue(Like_cm, out like_id);

                if(like_id != null)
                {
                    throw new Exception("Цей користувач вже вподобав цей фільм");
                }

                string insertLike = "insert into liked_film(user_id, film_id) values(@User_id, @Movie_id)";
                SqlCommand Insert_cm = new SqlCommand(insertLike, connection);
                Insert_cm.Parameters.AddWithValue("@User_id", login_id);
                Insert_cm.Parameters.AddWithValue("@Movie_id", movie_id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Insert_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Ви додали вподобання", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    
        
    }
}
