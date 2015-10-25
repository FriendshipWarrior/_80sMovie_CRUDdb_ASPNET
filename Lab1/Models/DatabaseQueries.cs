using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlServerCe;

namespace Lab1.Models
{
    public class DatabaseQueries
    {
        private static SqlCeConnection CreateConnetion()
        {
            SqlCeConnection conn = new SqlCeConnection();
            conn.ConnectionString =
                ConfigurationManager.ConnectionStrings["CRUDdb"].ConnectionString;
            conn.Open();

            return conn;
        }

        public static void CreateMovie(_80sMovieModel movie)
        {
            string cmdText =
                @"INSERT INTO _80sMovies (Title, Genre, Rating, ReleaseDate, Seen)
                   VALUES (@title, @genre, @rating, @releaseDate, @seen)";
            using (SqlCeConnection conn = CreateConnetion())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@rating", movie.Rating);
                    cmd.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate);
                    cmd.Parameters.AddWithValue("@seen", movie.Seen);

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<_80sMovieModel> ReadAllMovies()
        {
            string cmdText = @"Select Id, Title, Genre, Rating, ReleaseDate, Seen FROM _80sMovies ORDER BY ReleaseDate";

            List<_80sMovieModel> list = new List<_80sMovieModel>();

            using (SqlCeConnection conn = CreateConnetion())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    using (SqlCeDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _80sMovieModel item = new _80sMovieModel();
                            
                                item.Id = (int)reader["Id"];
                                item.Title = (string)reader["Title"];
                                item.Genre = (string)reader["Genre"];
                                item.Rating = (float)Convert.ToDouble(reader["Rating"]);
                                item.ReleaseDate = (DateTime)reader["ReleaseDate"];
                                item.Seen = (bool)reader["Seen"];
                            

                            list.Add(item);
                        }
                    }
                }
            }

            return list;
        }

        public static _80sMovieModel ReadMovie(int id)
        {
            string cmdText =
                @"SELECT
                    Id, Title, Genre, Rating, ReleaseDate, Seen FROM _80sMovies
                    WHERE Id = @movieId";
            _80sMovieModel item = null;

            using (SqlCeConnection conn = CreateConnetion())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@movieId", id);

                    using (SqlCeDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new _80sMovieModel();
                                item.Id = (int)reader["Id"];
                                item.Title = (string)reader["Title"];
                                item.Genre = (string)reader["Genre"];
                                item.Rating = (float)Convert.ToDouble(reader["Rating"]);
                                item.ReleaseDate = (DateTime)reader["ReleaseDate"];
                                item.Seen = (bool)reader["Seen"];
                        }
                    }
                }
            }
            return item;
        }

        public static void UpdateMovie(_80sMovieModel movie)
        {
            string cmdText =
                @"UPDATE _80sMovies 
                SET 
                    Title = @title, 
                    Genre = @genre, 
                    Rating = @rating, 
                    ReleaseDate = @releaseDate, 
                    Seen = @seen
                Where Id = @id";
            using (SqlCeConnection conn = CreateConnetion())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@id", movie.Id);
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@rating", movie.Rating);
                    cmd.Parameters.AddWithValue("@releaseDate", movie.ReleaseDate);
                    cmd.Parameters.AddWithValue("@seen", movie.Seen);

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteMovie(int id)
        {
            string cmdText = @"DELETE FROM _80sMovies WHERE Id = @movieId";

            using (SqlCeConnection conn = CreateConnetion())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@movieId", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}