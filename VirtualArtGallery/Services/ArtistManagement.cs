using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.myexceptions;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    internal class ArtistManagement
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public static string filepath = "C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json";

        public ArtistManagement()
        {
            sqlConnection = DBConnection.GetConnection(filepath);
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public string GetArtistNameByArtworkID(int artworkID)
        {
            string artistName = string.Empty;

            try
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = "SELECT a.Name FROM Artist a INNER JOIN Artwork aw ON a.ArtistID = aw.ArtistID WHERE aw.ArtworkID = @ArtworkID";
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        artistName = reader["Name"].ToString();
                    }
                    else
                    {
                        throw new ArtWorkNotFoundException(artworkID);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return artistName;
        }

    }
}
