using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Laba_BD2.Data
{
    internal class DB
    {
        ~DB(){
            CloseConnection();
        }
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
        private void ReadValue(SqlCommand com, out int? value, string column_name)
        {
            value = null;
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    value = Convert.ToInt32(reader[column_name]);
                }
            }
        }
        private void ReadValue(SqlCommand com, ref List<string> value, string column_name)
        {
            value.Clear();
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    value.Add(reader[column_name].ToString());
                }
            }
            value.OrderBy(s => s);
        }
        public List<string> Simple_1(List<string> values)
        {
            var logins = new List<string>();
            try
            {
                string simple_1 = "select login "
                                     + "from User_Info "
                                     + "where sub_id = (select sub_id from Movie where name = @Movie_Name) "
                                     + "order by login; ";
                StringBuilder movie_name = new StringBuilder();
                movie_name.Append(values.First().Trim());
                string find_movie = "select id from Movie where name = @Movie_name;";
                SqlCommand Find_cm = new SqlCommand(find_movie, connection);
                Find_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());
                int? movie_id = null;
                this.OpenConnection();
                ReadValue(Find_cm, out movie_id);
                if (movie_id == null)
                {
                    throw new Exception("Не існує такого фільму");
                }
                SqlCommand Get_logins_cm = new SqlCommand(simple_1, connection);
                Get_logins_cm.Parameters.AddWithValue("@Movie_Name", movie_name.ToString());
                ReadValue(Get_logins_cm, ref logins, "login");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return logins;
        }
        public List<string> Simple_2(List<string> values)
        {
            var genres = new List<string>();
            try
            {
                string simple_2 = "select genre_name "
                                     + "from Genre "
                                     + "where id in (select DISTINCT genre_id from Movie where sub_id = (select DISTINCT id from Sub_Plan where sub_name = @Sub_name)) "
                                     + "order by genre_name; ";
                StringBuilder sub_name = new StringBuilder();
                sub_name.Append(values.First().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                string find_sub = "select id from Sub_Plan where sub_name = @Sub_name;";
                SqlCommand Find_cm = new SqlCommand(find_sub, connection);
                Find_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                int? sub_id = null;
                this.OpenConnection();
                ReadValue(Find_cm, out sub_id);
                if (sub_id == null)
                {
                    throw new Exception("Не існує такого жанру");
                }
                SqlCommand Get_genres_cm = new SqlCommand(simple_2, connection);
                Get_genres_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                ReadValue(Get_genres_cm, ref genres, "genre_name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return genres;
        } 
        public List<string> Simple_3(List<string> values)
        {
            var movies = new List<string>();
            try
            {
                string simple_3 = "select name "
                                     + "from Movie "
                                     + "where sub_id in (select id from Sub_Plan where prise < @Prise and prise != -1) "
                                     + "order by name; ";
                uint sub_prise = new uint();
                if(!UInt32.TryParse(values.First(), out sub_prise))
                {
                    throw new Exception("Введений параметр має бути додатним числом");
                }
                this.OpenConnection();
                SqlCommand Get_movies_cm = new SqlCommand(simple_3, connection);
                Get_movies_cm.Parameters.AddWithValue("@Prise", Convert.ToInt32(sub_prise));
                ReadValue(Get_movies_cm, ref movies, "name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return movies;
        }
        public List<string> Simple_4(List<string> values)
        {
            var logins = new List<string>();
            try
            {
                string simple_4 = "select User_Info.login "
                                     + "from "
                                     + "User_Info "
                                     + "inner join liked_film on liked_film.user_id = User_Info.id "
                                     + "inner join Movie on liked_film.film_id = Movie.id "
                                     + "inner join Sub_Plan on Sub_Plan.id = User_Info.sub_id "
                                + "where "
                                    + "Movie.name = @Movie_name and Sub_Plan.sub_name = @Sub_name ;";
                StringBuilder movie_name = new StringBuilder()
                    , sub_name = new StringBuilder();

                movie_name.Append(values.First().Trim());
                
                sub_name.Append(values.Last().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                string find_sub = "select id from Sub_Plan where sub_name = @Sub_name;";
                string find_movie = "select id from Movie where name = @Movie_name;";
                SqlCommand Find_sub_cm = new SqlCommand(find_sub, connection)
                    , Find_movie_cm = new SqlCommand(find_movie, connection);
                Find_sub_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                Find_movie_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());
                int? sub_id = null, movie_id = null;
                this.OpenConnection();
                ReadValue(Find_sub_cm, out sub_id);
                ReadValue(Find_movie_cm, out movie_id);
                if (sub_id == null)
                {
                    throw new Exception("Немає такого жанру в БД");
                }
                if(movie_id == null)
                {
                    throw new Exception("Немає такого фільму в БД");
                }
                SqlCommand Get_logins_cm = new SqlCommand(simple_4, connection);
                Get_logins_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                Get_logins_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());
                ReadValue(Get_logins_cm, ref logins, "login");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return logins;
        }
        public List<string> Simple_5(List<string> values)
        {
            var subs = new List<string>();
            try
            {
                string simple_5 = "select Sub_Plan.sub_name "
                                + "from "
                                     + "Sub_Plan "
                                     + "inner join User_Info on Sub_Plan.id = User_Info.sub_id "
                                     + "inner join liked_film on User_Info.id = liked_film.user_id "
                                     + "inner join Movie on liked_film.film_id = Movie.id "
                                + "where Movie.name = @Movie_name "
                                + "order by Sub_Plan.sub_name; ";
                StringBuilder movie_name = new StringBuilder();
                movie_name.Append(values.First().Trim());

                string find_movie = "select id from Movie where name = @Movie_name;";
                SqlCommand Find_cm = new SqlCommand(find_movie, connection);
                Find_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());
                int? movie_id = null;
                this.OpenConnection();
                ReadValue(Find_cm, out movie_id);
                if (movie_id == null)
                {
                    throw new Exception("Немає такого фільму в БД");
                }
                SqlCommand Get_subs_cm = new SqlCommand(simple_5, connection);
                Get_subs_cm.Parameters.AddWithValue("@Movie_name", movie_name.ToString());
                ReadValue(Get_subs_cm, ref subs, "sub_name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return subs;
        }
        public List<string> Complicated_1(List<string> values)
        {
            var logins = new List<string>();
            try
            {
                string com_1 = "SELECT "
                                       + "User_Info.login "
                                 + "FROM "
                                       + "User_Info "
                                       + "INNER JOIN liked_film ON User_Info.id = liked_film.user_id "
                                       + "INNER JOIN(SELECT Movie.id FROM Movie WHERE Movie.genre_id = @Genre_id) films ON liked_film.film_id = films.id "
                                 + "GROUP BY User_Info.login "
                                 + "HAVING COUNT(liked_film.film_id) >= (SELECT COUNT(Movie.id) FROM Movie WHERE Movie.genre_id = @Genre_id) "
                                 + "ORDER BY User_Info.login;";
                StringBuilder genre_name = new StringBuilder();
                genre_name.Append(values.First().Trim().ToLower());
                genre_name[0] = char.ToUpper(genre_name[0]);

                string checkIfGenreExist = "select id from Genre where genre_name = @Genre_name";
                SqlCommand GenreId_cm = new SqlCommand(checkIfGenreExist, connection);
                GenreId_cm.Parameters.AddWithValue("@Genre_name", genre_name.ToString());
                int? genre_id = null;
                this.OpenConnection();
                ReadValue(GenreId_cm, out genre_id);
                if (genre_id == null)
                {
                    throw new Exception("Такого жанру не існує в БД");
                }
                SqlCommand Get_logins = new SqlCommand(com_1, connection);
                Get_logins.Parameters.AddWithValue("@Genre_id", Convert.ToInt32(genre_id));
                ReadValue(Get_logins, ref logins, "login");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return logins;
        }
        public List<string> Complicated_2(List<string> values)
        {
            var logins = new List<string>();
            try
            {
                string com_3 = "SELECT "
                                       + "User_Info.login "
                                 + "FROM "
                                       + "User_Info "
                                       + "INNER JOIN liked_film ON User_Info.id = liked_film.user_id "
                                       + "INNER JOIN (SELECT Movie.id FROM Movie WHERE Movie.sub_id = @Sub_id) films ON liked_film.film_id = films.id "
                                 + "GROUP BY User_Info.login "
                                 + "HAVING COUNT(liked_film.film_id) >= (SELECT COUNT(Movie.id) FROM Movie WHERE Movie.sub_id = @Sub_id) "
                                 + "ORDER BY User_Info.login;";
                StringBuilder sub_name = new StringBuilder();
                sub_name.Append(values.Last().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);

                string find_sub = "select id from Sub_Plan where sub_name = @Sub_name;";
                SqlCommand Find_sub_cm = new SqlCommand(find_sub, connection);
                Find_sub_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                int? sub_id = null;
                this.OpenConnection();
                ReadValue(Find_sub_cm, out sub_id);
                if (sub_id == null)
                {
                    throw new Exception("Такої підписки не існує в БД");
                }
                SqlCommand Get_logins = new SqlCommand(com_3, connection);
                Get_logins.Parameters.AddWithValue("@Sub_id", Convert.ToInt32(sub_id));
                ReadValue(Get_logins, ref logins, "login");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return logins;
        }
        public List<string> Complicated_3(List<string> values)
        {
            var subs = new List<string>();
            try
            {
                string com_3 = "SELECT "
                                    + "Sub_Plan.sub_name "
                                 + "FROM "
                                    + "Sub_Plan "
                                    + "INNER JOIN Movie ON Movie.sub_id = Sub_Plan.id "
                                 + "WHERE Sub_Plan.id != @Sub_id "
                                 + "GROUP BY Sub_Plan.sub_name "
                                 + " HAVING Count(Movie.name) = (SELECT Count(Movie.id) FROM Movie WHERE Movie.sub_id = @Sub_id)";
                StringBuilder sub_name = new StringBuilder();
                sub_name.Append(values.Last().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);

                string find_sub = "select id from Sub_Plan where sub_name = @Sub_name;";
                SqlCommand Find_sub_cm = new SqlCommand(find_sub, connection);
                Find_sub_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                int? sub_id = null;
                this.OpenConnection();
                ReadValue(Find_sub_cm, out sub_id);
                if (sub_id == null)
                {
                    throw new Exception("Такої підписки не існує в БД");
                }
                SqlCommand Get_subs = new SqlCommand(com_3, connection);
                Get_subs.Parameters.AddWithValue("@Sub_id", Convert.ToInt32(sub_id));
                ReadValue(Get_subs, ref subs, "sub_name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.CloseConnection();
            }
            return subs;
        }
        public void Delete_Genre(List<string> values)
        {
            try
            {
                StringBuilder genre_name = new StringBuilder();
                genre_name.Append(values.First().Trim().ToLower());
                genre_name[0] = char.ToUpper(genre_name[0]);
                string checkIfGenreExist = "select id from Genre where genre_name = @Genre_name";
                SqlCommand GenreId_cm = new SqlCommand(checkIfGenreExist, connection);
                GenreId_cm.Parameters.AddWithValue("@Genre_name", genre_name.ToString());
                int? genre_id = null;
                this.OpenConnection();
                ReadValue(GenreId_cm, out genre_id);
                if (genre_id == null)
                {
                    throw new Exception("Такого жанру не існує в БД");
                }
                string deleteCommand = "delete from Genre where id = @Id";
                SqlCommand Delete_cm = new SqlCommand(deleteCommand, connection);
                Delete_cm.Parameters.AddWithValue("@Id", Convert.ToInt32(genre_id));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Delete_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви видалили жанр";
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void Delete_Sub(List<string> values)
        {
            try
            {
                StringBuilder sub_name = new StringBuilder();
                sub_name.Append(values.First().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                string checkIfSubExist = "select id from Sub_Plan where sub_name = @Sub_name";
                SqlCommand SubId_cm = new SqlCommand(checkIfSubExist, connection);
                SubId_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                int? sub_id = null;
                this.OpenConnection();
                ReadValue(SubId_cm, out sub_id);
                if (sub_id == null)
                {
                    throw new Exception("Такої підписки не існує в БД");
                }
                string deleteCommand = "delete from Sub_Plan where id = @Id";
                SqlCommand Delete_cm = new SqlCommand(deleteCommand, connection);
                Delete_cm.Parameters.AddWithValue("@Id", Convert.ToInt32(sub_id));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Delete_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви видалили підписку";
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void Delete_Movie(List<string> values)
        {
            try
            {
                StringBuilder movie_name = new StringBuilder();
                movie_name.Append(values.First().Trim());
                string checkIfFilmExist = "select id from Movie where name = @Film_name";
                SqlCommand FilmId_cm = new SqlCommand(checkIfFilmExist, connection);
                FilmId_cm.Parameters.AddWithValue("@Film_name", movie_name.ToString());
                int? film_id = null;
                this.OpenConnection();
                ReadValue(FilmId_cm, out film_id);
                if (film_id == null)
                {
                    throw new Exception("Такого фільму не існує в БД");
                }
                string deleteCommand = "delete from Movie where id = @Id";
                SqlCommand Delete_cm = new SqlCommand(deleteCommand, connection);
                Delete_cm.Parameters.AddWithValue("@Id", Convert.ToInt32(film_id));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Delete_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви видалили фільм";
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void Update_Sub_Prise(List<string> values)
        {
            try
            {
                StringBuilder sub_name = new StringBuilder();
                sub_name.Append(values.First().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                uint sub_prise = new uint();
                if (!UInt32.TryParse(values.Last(), out sub_prise))
                {
                    throw new Exception("Введіть валідне значення ціни(додатне число)");
                }
                string getSubFromFilm = "select id from Sub_Plan where sub_name = @Sub_Name";
                SqlCommand GetSub_cm = new SqlCommand(getSubFromFilm, connection);

                GetSub_cm.Parameters.AddWithValue("@Sub_Name", sub_name.ToString());
                int? sub_id = null;
                this.OpenConnection();
                ReadValue(GetSub_cm, out sub_id);

                if (sub_id == null)
                {
                    throw new Exception("Немає такої підписки в БД");
                }
                string updateCommand = "update Sub_Plan set prise = @Prise where id = @Sub_id";
                SqlCommand Update_cm = new SqlCommand(updateCommand, connection);
                Update_cm.Parameters.AddWithValue("@Prise", Convert.ToInt32(sub_prise));
                Update_cm.Parameters.AddWithValue("@Sub_id", Convert.ToInt32(sub_id));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Update_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви змінили ціну підписці";
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
        public void Update_Movie_Sub(List<string> values)
        {
            try
            {
                StringBuilder movie_name = new StringBuilder()
                    , sub_name = new StringBuilder();
                movie_name.Append(values.First().Trim());
                sub_name.Append(values.Last().Trim().ToLower());
                sub_name[0] = char.ToUpper(sub_name[0]);
                string checkIfFilmExist = "select id from Movie where name = @Film_name";
                string find_SubId = "select id from Sub_Plan where sub_name = @Sub_name";
                SqlCommand SubId_cm = new SqlCommand(find_SubId, connection),
                    FilmId_cm = new SqlCommand(checkIfFilmExist, connection);
                SubId_cm.Parameters.AddWithValue("@Sub_name", sub_name.ToString());
                FilmId_cm.Parameters.AddWithValue("@Film_name", movie_name.ToString());
                int? sub_id = null, film_id = null;

                this.OpenConnection();
                ReadValue(SubId_cm, out sub_id);
                ReadValue(FilmId_cm, out film_id);

                if(sub_id == null)
                {
                    throw new Exception("Такої підписки не існує в БД");
                }
                if(film_id == null)
                {
                    throw new Exception("Такого фільму не існує в БД");
                }
                string updateCommand = "update Movie set sub_id = @Sub_id ,prise = @Prise where id = @Movie_id";
                SqlCommand Update_cm = new SqlCommand(updateCommand, connection);
                Update_cm.Parameters.AddWithValue("@Sub_id", sub_id);
                Update_cm.Parameters.AddWithValue("@Movie_id", film_id);
                Update_cm.Parameters.AddWithValue("@Prise", sub_id == 5 ? 1 : 0);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Update_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви змінили підписку";
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void Update_Movie_Prise(List<string> values)
        {
            try
            {
                StringBuilder movie_name = new StringBuilder();
                movie_name.Append(values.First().Trim());
                uint movie_prise = new uint();
                if (!UInt32.TryParse(values.Last(), out movie_prise))
                {
                    throw new Exception("Введіть валідне значення ціни(число)");
                }
                string checkIfFilmExist = "select id from Movie where name = @Film_name";
                string getSubFromFilm = "select sub_id from Movie where id = @Film_id";
                int? MovieId = null, Sub_id = null;
                this.OpenConnection();
                SqlCommand GetMovieIf_cm = new SqlCommand(checkIfFilmExist, connection);
                GetMovieIf_cm.Parameters.AddWithValue("@Film_Name", movie_name.ToString());
                ReadValue(GetMovieIf_cm, out MovieId);

                if (Equals(MovieId, null))
                {
                    throw new Exception("Такий фільм не існує в БД. Спочатку додайте його.");
                }
                this.CloseConnection();
                this.OpenConnection();
                SqlCommand GetSubId_cm = new SqlCommand(getSubFromFilm, connection);
                GetSubId_cm.Parameters.AddWithValue("@Film_id", Convert.ToInt32(MovieId));
                ReadValue(GetSubId_cm, out Sub_id, "sub_id");
                if (!Equals(Sub_id, 5) && movie_prise != 0)
                {
                    throw new Exception("Фільм з підпискою не може мати власної ціни,\n " +
                        "Спочатку змініть підписку");
                }
                if (Equals(Sub_id, 5) && Equals(movie_prise, 0))
                {
                    throw new Exception("Фільм без підписки не може бути безкоштовним,\n " +
                        "Спочатку змініть підписку");
                }
                string updateCommand = "update Movie set prise = @Movie_prise where id = @Movie_id";
                SqlCommand Update_cm = new SqlCommand(updateCommand, connection);
                Update_cm.Parameters.AddWithValue("@Movie_id", Convert.ToInt32(MovieId));
                Update_cm.Parameters.AddWithValue("@Movie_prise", Convert.ToInt32(movie_prise));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.InsertCommand = Update_cm;
                sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                string message = "Ви змінили ціну фільму";
                MessageBox.Show(message, " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
