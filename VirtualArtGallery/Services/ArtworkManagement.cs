using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualArtGallery.Interfaces;
using VirtualArtGallery.Modals;
using VirtualArtGallery.myexceptions;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    public class ArtworkManagement : IArtworkManagement
    {

        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public static string filepath = "C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json";

        public ArtworkManagement()
        {
            sqlConnection = DBConnection.GetConnection(filepath);
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }




        public int AddArtwork(Artwork artwork)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Artwork (ArtworkID, Title, Description, CreationDate, Medium, ImageURL, ArtistID) VALUES (@ArtworkID, @Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Description", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int addArtworkStatus = cmd.ExecuteNonQuery();
                return addArtworkStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }





        public string GetArtistNameByArtworkId(int artworkID)
        {
            throw new NotImplementedException();
        }




        public List<Artwork> GetArtwork()
        {
            List<Artwork> artwork = new List<Artwork>();
            try
            {
                cmd.CommandText = "SELECT * FROM Artwork";

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Artwork artworkItem = new Artwork
                        {
                            ArtworkID = (int)reader["ArtworkID"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            CreationDate = reader["CreationDate"].ToString(),
                            Medium = reader["Medium"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            ArtistID = (int)reader["ArtistID"]
                        };

                        artwork.Add(artworkItem);
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
            return artwork;
        }



        public Artwork GetArtworkById(int artworkID)
        {
            Artwork artwork = null;
            try
            {
                cmd.CommandText = "SELECT * FROM Artwork WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        artwork = new Artwork(
                            artworkID: (int)reader["ArtworkID"],
                            title: reader["Title"].ToString(),
                            description: reader["Description"].ToString(),
                            creationDate: reader["CreationDate"].ToString(),
                            medium: reader["Medium"].ToString(),
                            imageURL: reader["ImageURL"].ToString(),
                            artistID: (int)reader["ArtistID"]
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArtWorkNotFoundException(artworkID);
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return artwork;
        }



        public bool RemoveArtwork(int artworkID)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Artwork WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

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



        public List<Artwork> SearchArtworks(string keyword)
        {
            List<Artwork> artworks = new List<Artwork>();
            try
            {
                cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Artwork artwork = new Artwork(
                            artworkID: (int)reader["ArtworkID"],
                            title: reader["Title"].ToString(),
                            description: reader["Description"].ToString(),
                            creationDate: reader["CreationDate"].ToString(),
                            medium: reader["Medium"].ToString(),
                            imageURL: reader["ImageURL"].ToString(),
                            artistID: (int)reader["ArtistID"]
                        );
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


        //public bool UpdateArtwork(Artwork artwork)
        //{
        //    try
        //    {
        //        cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
        //        cmd.Parameters.AddWithValue("@Title", artwork.Title);
        //        cmd.Parameters.AddWithValue("@Description", artwork.Description);
        //        cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
        //        cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
        //        cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
        //        cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

        //        sqlConnection.Open();
        //        int rowsAffected = cmd.ExecuteNonQuery();
        //        return rowsAffected > 0; 
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //}


        public bool UpdateArtwork(Artwork artwork)
        {
            try
            {
                // Convert the CreationDate string to DateTime
                DateTime creationDate;
                if (!DateTime.TryParseExact(artwork.CreationDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out creationDate))
                {
                    Console.WriteLine("Invalid date format. Please use dd-MM-yyyy format.");
                    return false; // Return false if the date format is invalid
                }

                // Prepare SQL command
                cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Description", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", creationDate); // Use DateTime for CreationDate
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                // Open connection and execute query
                sqlConnection.Open();
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
                sqlConnection.Close();
            }
        }






    }
}
