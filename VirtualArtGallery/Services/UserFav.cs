using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Interfaces;
using VirtualArtGallery.Modals;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    public class UserFav : IUserFav 
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        ArtistManagement art = new ArtistManagement();

        public UserFav()
        {
            sqlConnection = DBConnection.GetConnection("C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json");
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            try
            {
                cmd.CommandText = "INSERT INTO User_Favorite_Artwork (UserID, ArtworkID) VALUES (@UserID, @ArtworkID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            List<Artwork> artworks = new List<Artwork>(); 

            try
            {
                cmd.CommandText = @"
            SELECT A.ArtworkID, A.Title, A.Description, A.CreationDate, A.Medium, A.ImageURL, A.ArtistID
            FROM User_Favorite_Artwork UFA
            INNER JOIN Artwork A ON UFA.ArtworkID = A.ArtworkID
            WHERE UFA.UserID = @UserID";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserID", userId);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Artwork artwork = new Artwork
                        {
                            ArtworkID = (int)reader["ArtworkID"],


                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreationDate = reader["CreationDate"].ToString(),
                            Medium = reader["Medium"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            ArtistID = (int)reader["ArtistID"]
                        };

                        artworks.Add(artwork);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return artworks; 
        }




        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            try
            {
                cmd.CommandText = "DELETE FROM User_Favorite_Artwork WHERE UserID = @UserID AND ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }


    }
}
