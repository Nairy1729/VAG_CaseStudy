using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Modals;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    public class GalleryManagement
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public static string filepath = "C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json";

        public GalleryManagement()
        {
            sqlConnection = DBConnection.GetConnection(filepath);
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public int AddArtworkToGallery(int artworkID, int galleryID)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Artwork_Gallery (ArtworkID, GalleryID) VALUES (@ArtworkID, @GalleryID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int result = cmd.ExecuteNonQuery();
                return result;
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

        public List<List<string>> GetArtworksInGallery(int galleryID)
        {
            List<List<string>> artworks = new List<List<string>>(); 

            try
            {
                cmd.CommandText = @"SELECT Artwork.ArtworkID, Artwork.Title, Artwork.Description
                            FROM Artwork
                            INNER JOIN Artwork_Gallery 
                            ON Artwork.ArtworkID = Artwork_Gallery.ArtworkID
                            WHERE Artwork_Gallery.GalleryID = @GalleryID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int artworkID = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string description = reader.GetString(2);

                    List<string> artwork1 = new List<string>(); 
                    artwork1.Add(artworkID.ToString()); 
                    artwork1.Add(title);
                    artwork1.Add(description);

                    artworks.Add(artwork1); 
                }

                reader.Close();
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

        public List<List<string>> SearchArtworkInGallery(int galleryID, string title)
        {
            List<List<string>> artworks = new List<List<string>>(); 

            try
            {
                cmd.CommandText = @"SELECT ArtworkID, Title ,Description
                            FROM Artwork 
                            WHERE Title LIKE @Title 
                            AND ArtworkID IN 
                            (SELECT ArtworkID FROM Artwork_Gallery WHERE GalleryID = @GalleryID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Title", "%" + title + "%");
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int artworkID = reader.GetInt32(0);
                    string artworkTitle = reader.GetString(1);
                    string description = reader.GetString(2);

                    List<string> artwork1 = new List<string>(); 
                    artwork1.Add(artworkID.ToString()); 
                    artwork1.Add(artworkTitle); 
                    artwork1.Add(description);

                    artworks.Add(artwork1); 
                }

                reader.Close();
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


        public int RemoveArtworkFromGallery(int artworkID, int galleryID)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Artwork_Gallery WHERE ArtworkID = @ArtworkID AND GalleryID = @GalleryID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artworkID);
                cmd.Parameters.AddWithValue("@GalleryID", galleryID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int result = cmd.ExecuteNonQuery();
                return result;
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

        public List<List<string>> GetGalleriesCuratedByArtist(int curatorID)
        {
            List<List<string>> galleries = new List<List<string>>(); 

            try
            {
                cmd.CommandText = "SELECT GalleryID, Name FROM Gallery WHERE Curator = @CuratorID";
                cmd.Parameters.Clear(); 
                cmd.Parameters.AddWithValue("@CuratorID", curatorID); 

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open(); 
                }

                SqlDataReader reader = cmd.ExecuteReader(); 

                while (reader.Read())
                {
                    int galleryID = reader.GetInt32(0); 
                    string galleryName = reader.GetString(1); 

                    List<string> galleryDetails = new List<string>(); 
                    galleryDetails.Add(galleryID.ToString()); 
                    galleryDetails.Add(galleryName); 

                    galleries.Add(galleryDetails); 
                }

                reader.Close(); 
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

            return galleries; 
        }

        public int RemoveArtworkFromAllGalleries(int artworkID)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Artwork_Gallery WHERE ArtworkID = @ArtworkID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int result = cmd.ExecuteNonQuery();
                return result;
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












    }
}
