using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Modals;
using VirtualArtGallery.util;

namespace VirtualArtGallery.Services
{
    public class UserManagement
    {

        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public static string filepath = "C:\\Users\\nairy\\source\\repos\\VAG\\VirtualArtGallery\\util\\dbconfig.json";

        public UserManagement()
        {
            sqlConnection = DBConnection.GetConnection(filepath);
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }


        public List<(string FirstName, string LastName, Artwork Artwork)> GetUserFavoriteArtworks(int userId)
        {
            List<(string FirstName, string LastName, Artwork Artwork)> favoriteArtworks = new List<(string FirstName, string LastName, Artwork Artwork)>();

            try
            {
                cmd.CommandText = "SELECT Users.FirstName, Users.LastName, Artwork.ArtworkID, Artwork.Title, Artwork.Description, Artwork.Medium " +
                                  "FROM Artwork " +
                                  "INNER JOIN User_Favorite_Artwork ON Artwork.ArtworkID = User_Favorite_Artwork.ArtworkID " +
                                  "INNER JOIN Users ON Users.UserID = User_Favorite_Artwork.UserID " +
                                  "WHERE Users.UserID = @UserID";
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
                        string firstName = reader.GetString(0);
                        string lastName = reader.GetString(1);

                        Artwork artwork = new Artwork
                        {
                            ArtworkID = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            Description = reader.GetString(4),
                            Medium = reader.GetString(5)
                        };

                        favoriteArtworks.Add((firstName, lastName, artwork));
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

            return favoriteArtworks;
        }

        public bool RegisterUser(User newUser)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Users (Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture) " +
                                  "VALUES (@Username, @Password, @Email, @FirstName, @LastName, @DateOfBirth, @ProfilePicture)";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Username", newUser.Username);
                cmd.Parameters.AddWithValue("@Password", newUser.Password); 
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", newUser.DateOfBirth);
                cmd.Parameters.AddWithValue("@ProfilePicture", (object)newUser.ProfilePicture ?? DBNull.Value);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering user: " + ex.Message);
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

        public int DeleteUser(int userId)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserID", userId);

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

        public User GetUserById(int userId)
        {
            User user = null;

            try
            {
                cmd.CommandText = "SELECT UserID, Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture " +
                                  "FROM Users WHERE UserID = @UserID";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserID", userId);

                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Email = reader.GetString(3),
                            FirstName = reader.GetString(4),
                            LastName = reader.GetString(5),
                            DateOfBirth = reader.GetDateTime(6).ToString("yyyy-MM-dd"), // Format the DateOfBirth as string
                            ProfilePicture = !reader.IsDBNull(7) ? reader.GetString(7) : null
                        };
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

            return user;
        }





    }
}
