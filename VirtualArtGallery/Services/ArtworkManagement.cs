using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualArtGallery.Interfaces;
using VirtualArtGallery.Modals;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    public class ArtworkManagement : IArtworkManagement
    {

        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public ArtworkManagement()
        {
            sqlConnection = DBConnection.GetConnection("C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json");
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public int AddArtwork(Artwork artwork)
        {
            try
            {
                // Ensure the command text is set correctly
                cmd.CommandText = "INSERT INTO Artwork (ArtworkID, Title, Description, CreationDate, Medium, ImageURL, ArtistID) VALUES (@ArtworkID, @Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";

                // Clear previous parameters to avoid duplication
                cmd.Parameters.Clear();

                // Add parameters for the artwork
                cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Description", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                // Open the connection if it's not already open
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                // Execute the command and return the number of affected rows
                int addArtworkStatus = cmd.ExecuteNonQuery();
                return addArtworkStatus;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1; // Return -1 to indicate an error
            }
            finally
            {
                // Ensure the connection is closed
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }


        //public List<Artwork> GetAllArtwork()
        //{
        //    List<Artwork> artwork = new List<Artwork>();

        //    try
        //    {
        //        cmd.CommandText = "SELECT * FROM Artwork";

        //        if (sqlConnection.State == System.Data.ConnectionState.Closed)
        //        {
        //            sqlConnection.Open();
        //        }

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Artwork artwork1 = new Artwork()
        //            {
        //                ArtistID = (int)reader["ArtistID"],
        //                Name = (string)reader["Name"],
        //                Biography = (string)reader["Biography"],
        //                BirthDate = reader["BirthDate"] != DBNull.Value
        //                ? Convert.ToDateTime(reader["BirthDate"]).ToString("yyyy-MM-dd") 
        //                : string.Empty ,
        //                Nationality = reader["Nationality"] != DBNull.Value ? (string)reader["Nationality"] : string.Empty,
        //                Website = reader["Website"] != DBNull.Value ? (string)reader["Website"] : string.Empty,
        //                ContactInformation = reader["ContactInformation"] != DBNull.Value ? (string)reader["ContactInformation"] : string.Empty
        //            };

        //            artwork.Add(artwork1);
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error retrieving artists: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (sqlConnection.State == System.Data.ConnectionState.Open)
        //        {
        //            sqlConnection.Close();
        //        }
        //    }

        //    return artwork;
        //}

        public List<Artwork> GetArtwork()
        {
            List<Artwork> artwork = new List<Artwork>();
            try
            {
                cmd.CommandText = "SELECT * FROM Artwork";

                // Open the connection if it's not already open
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new Artwork object for each row
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

                        // Add the artworkItem to the artwork list
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
                // Ensure the connection is closed
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

                // Open the connection if it's not already open
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
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
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
                return rowsAffected > 0; // Returns true if a row was deleted
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

        // Similar updates for UpdateArtwork and SearchArtworks methods


        public List<Artwork> SearchArtworks(string keyword)
        {
            List<Artwork> artworks = new List<Artwork>();
            try
            {
                cmd.CommandText = "SELECT * FROM Artwork WHERE Title LIKE @Keyword OR Description LIKE @Keyword";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                // Open the connection if it's not already open
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
                // Ensure the connection is closed
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return artworks;
        }


        public bool UpdateArtwork(Artwork artwork)
        {
            try
            {
                cmd.CommandText = "UPDATE Artwork SET Title = @Title, Description = @Description, CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                cmd.Parameters.AddWithValue("@Title", artwork.Title);
                cmd.Parameters.AddWithValue("@Description", artwork.Description);
                cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

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
