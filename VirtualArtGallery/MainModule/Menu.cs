using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VirtualArtGallery.Modals;
using VirtualArtGallery.Services;
using VirtualArtGallery.myexceptions;

namespace VirtualArtGallery.MainModule
{
    class Menu
    {
        private ArtworkManagement artworkManagement = new ArtworkManagement();
        private UserFav userFav = new UserFav();

        public void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Welcome to the Virtual Art Gallery!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please choose an option:");

                // Main menu options
                Console.WriteLine("1. Artwork Menu");
                Console.WriteLine("2. Favorite Artwork Menu");
                Console.WriteLine("3. Gallery Menu");
                Console.WriteLine("4. Users Menu");
                Console.WriteLine("5. Exit");

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Your choice: ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ArtworkMenu();
                        break;
                    case "2":
                        FavoriteArtworkMenu();
                        break;
                    case "3":
                        GalleryMenu();
                        break;
                    case "4":
                        UserMenu();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the program. Thank you!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ArtworkMenu()
        {
            while (true) 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Artwork Menu ===");
                Console.ResetColor();

                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Get Artwork by ID");
                Console.WriteLine("3. Update Artwork");
                Console.WriteLine("4. Remove Artwork");
                Console.WriteLine("5. Search Artworks");
                Console.WriteLine("6. Get All Artworks");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddArtwork();
                        break;
                    case "2":
                        GetArtworkById();
                        break;
                    case "3":
                        UpdateArtwork();
                        break;
                    case "4":
                        RemoveArtwork();
                        break;
                    case "5":
                        SearchArtworks();
                        break;
                    case "6":
                        GetArtwork1();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue; 
                }

                if (ReturnToMainMenu())
                {
                    DisplayMenu(); 
                    break; 
                }
            }
        }

        UserManagement userManagement = new UserManagement();

        private void UserMenu()
        {
            while (true) 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Users Menu ===");
                Console.ResetColor();

                Console.WriteLine("1. Get user by Id");
                Console.WriteLine("2. Get users fav artwrok");
                Console.WriteLine("3. Regitser a user");
                Console.WriteLine("4. Delete a user");

                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetUserById();
                        break;
                    case "2":
                        GetUserFavArtwork();
                        break;
                    case "3":
                        RegisterUser();
                        break;
                    case "4":
                        DeleteUser();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue; 
                }

                if (ReturnToMainMenu())
                {
                    DisplayMenu(); 
                    break; 
                }
            }
        }

        private void GetUserById()
        {
            Console.WriteLine("Enter user ID : ");
            int userId =  int.Parse(Console.ReadLine());
            
            User user = userManagement.GetUserById(userId);

            if (user != null)
            {
                Console.WriteLine($"User: {user.FirstName} {user.LastName}, Email: {user.Email}, DOB: {user.DateOfBirth}");
            }
            else
            {
                Console.WriteLine("User not found.");
            }

        }

        private void DeleteUser()
        {
            Console.Write("Enter User ID to delete: ");
            int userId = int.Parse(Console.ReadLine());

            int result = userManagement.DeleteUser(userId);
            if (result > 0)
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete user.");
            }

        }

        private void RegisterUser()
        {
            try
            {
                Console.WriteLine("Enter User Registration Details:");

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Date of Birth (yyyy-mm-dd): ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

                Console.Write("Profile Picture URL (optional): ");
                string profilePicture = Console.ReadLine();

                string dateOfBirthStr = dateOfBirth.ToString("yyyy-MM-dd");

                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirthStr,
                    ProfilePicture = string.IsNullOrWhiteSpace(profilePicture) ? null : profilePicture
                };

                bool isRegistered = userManagement.RegisterUser(newUser);

                if (isRegistered)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("User registered successfully!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User registration failed.");
                }
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
                Console.ResetColor();
            }
        }


        private void GetUserFavArtwork()
        {
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            var favoriteArtworks = userManagement.GetUserFavoriteArtworks(userId);

            if (favoriteArtworks.Count > 0)
            {
                bool flag = true;
                foreach (var item in favoriteArtworks)
                {
                    if (flag)
                    {
                        Console.WriteLine($"User: {item.FirstName} {item.LastName}");
                        flag = false;
                    }
                    Console.WriteLine($"Artwork ID: {item.Artwork.ArtworkID}, Title: {item.Artwork.Title}, Medium: {item.Artwork.Medium}");
                }
            }
            else
            {
                Console.WriteLine("No favorite artworks found.");
            }

        }

        private void FavoriteArtworkMenu()
        {
            while (true) 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("=== Favorite Artwork Menu ===");
                Console.ResetColor();

                Console.WriteLine("1. Add Artwork to Favorites");
                Console.WriteLine("2. Get User Favorite Artworks");
                Console.WriteLine("3. Remove Artwork from Favorites");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddArtworkToFavorites();
                        break;
                    case "2":
                        GetUserFavoriteArtworks();
                        break;
                    case "3":
                        RemoveArtworkFromFavorites();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue; 
                }

                if (ReturnToMainMenu())
                {
                    DisplayMenu(); 
                    break; 
                }
            }
        }

        private void GalleryMenu()
        {
            while (true) 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Gallery Menu ===");
                Console.ResetColor();

                Console.WriteLine("1. Add Artwork to Gallery");
                Console.WriteLine("2. Get Artworks in Gallery");
                Console.WriteLine("3. Search Artwork in Gallery");
                Console.WriteLine("4. Remove Artwork from Gallery");
                Console.WriteLine("5. Get Galleries Curated by Artist");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddArtworkToGallery();
                        break;
                    case "2":
                        GetArtworksInGallery();
                        break;
                    case "3":
                        SearchArtworkInGallery();
                        break;
                    case "4":
                        RemoveArtworkFromGallery();
                        break;
                    case "5":
                        GetGalleriesCuratedByArtist();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue; 
                }

                
                if (ReturnToMainMenu())
                {
                    DisplayMenu(); 
                    break; 
                }
            }
        }

        private bool ReturnToMainMenu()
        {
            Console.WriteLine("Would you like to return to the main menu? (y/n): ");
            string input = Console.ReadLine();

            return input?.Trim().ToLower() == "y"; 
        }






        ArtistManagement art = new ArtistManagement();

     

        private void GetArtwork1()
        {
            List<Artwork> artworkList = artworkManagement.GetArtwork();

            if (artworkList.Count != 0)
            {
                const int idWidth = 5;
                const int titleWidth = 35;
                const int artistWidth = 20;
                const int mediumWidth = 30;
                const int artistid = 5;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"| {"ID",idWidth} | {"Title",titleWidth} | {"Artist Name",artistWidth} | {"Medium",mediumWidth} | {"ArtistID", artistid}");
                Console.WriteLine(new string('-', idWidth + titleWidth + artistWidth + mediumWidth + 14)); 
                Console.ResetColor();

                foreach (var artwork in artworkList)
                {
                    string artistName = art.GetArtistNameByArtworkID(artwork.ArtworkID);

                    Console.WriteLine($"| {artwork.ArtworkID.ToString().PadRight(idWidth)} | {artwork.Title.PadRight(titleWidth)} | {artistName.PadRight(artistWidth)} | {artwork.Medium.PadRight(mediumWidth)} | {artwork.ArtistID.ToString().PadRight(artistid)} |");
                }

                Console.WriteLine(new string('-', idWidth + titleWidth + artistWidth + mediumWidth + 14));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Artwork not found.");
            }

            Console.ResetColor();
        }






        private void AddArtwork()
        {
            Console.WriteLine("Enter Artwork Details:");

            int lastArtworkID = art.GetLastArtworkID();
            if (lastArtworkID == -1)
            {
                Console.WriteLine("Error retrieving last ArtworkID.");
                return;
            }
            int newArtworkID = lastArtworkID + 1;

            Console.WriteLine("Automatically generated Artwork ID: " + newArtworkID);

            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Creation Date (yyyy-mm-dd): ");
            string creationDate = Console.ReadLine();
            Console.Write("Medium: ");
            string medium = Console.ReadLine();
            Console.Write("Image URL: ");
            string imageURL = Console.ReadLine();
            Console.Write("Artist ID: ");
            int artistID = int.Parse(Console.ReadLine());

            Artwork newArtwork = new Artwork
            {
                ArtworkID = newArtworkID,  
                Title = title,
                Description = description,
                CreationDate = creationDate,
                Medium = medium,
                ImageURL = imageURL,
                ArtistID = artistID
            };

            int result = artworkManagement.AddArtwork(newArtwork);
            if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Artwork added successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Failed to add artwork.");
                Console.ResetColor();
            }
        }








        private void GetArtworkById()
        {
            Console.Write("Enter Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());
            Artwork artwork = artworkManagement.GetArtworkById(artworkID);
            
            if (artwork != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Artwork Details:");
                Console.WriteLine($"ID: {artwork.ArtworkID}");
                Console.WriteLine($"Title: {artwork.Title}");
                Console.WriteLine($"Description: {artwork.Description}");
                Console.WriteLine($"Creation Date: {artwork.CreationDate}");
                Console.WriteLine($"Medium: {artwork.Medium}");
                Console.WriteLine($"Artist Name: {art.GetArtistNameByArtworkID(artwork.ArtistID)}");
                Console.ResetColor();
            }
            

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Artwork with ArtworkID {artworkID} is not found in the database.");
                Console.ResetColor();
            }
        }



        private void GetArtwork()
        {
            Console.Write("Enter Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());
            Artwork artwork = artworkManagement.GetArtworkById(artworkID);

            if (artwork != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Artwork Details:");
                Console.WriteLine($"ID: {artwork.ArtworkID}");
                Console.WriteLine($"Title: {artwork.Title}");
                Console.WriteLine($"Description: {artwork.Description}");
                Console.WriteLine($"Creation Date: {artwork.CreationDate}");
                Console.WriteLine($"Medium: {artwork.Medium}");
                Console.WriteLine($"Image URL: {artwork.ImageURL}");
                Console.WriteLine($"Artist ID: {artwork.ArtistID}");
                Console.ResetColor();
            }


            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Artwork not found.");
                Console.ResetColor();
            }
        }

        private void UpdateArtwork()
        {
            Console.Write("Enter Artwork ID to update: ");
            int artworkID = int.Parse(Console.ReadLine());
            Artwork existingArtwork = artworkManagement.GetArtworkById(artworkID);

            if (existingArtwork != null)
            {
                Console.WriteLine("Enter New Details for Artwork:");
                Console.Write("Title (leave blank to keep current): ");
                string title = Console.ReadLine();
                Console.Write("Description (leave blank to keep current): ");
                string description = Console.ReadLine();
                Console.Write("Creation Date (leave blank to keep current) (dd-MM-yyyy): ");
                string creationDate = Console.ReadLine();
                Console.Write("Medium (leave blank to keep current): ");
                string medium = Console.ReadLine();
                Console.Write("Image URL (leave blank to keep current): ");
                string imageURL = Console.ReadLine();
                Console.Write("Artist ID (leave blank to keep current): ");
                string artistIDInput = Console.ReadLine();

              
                if (!string.IsNullOrEmpty(title)) existingArtwork.Title = title;
                if (!string.IsNullOrEmpty(description)) existingArtwork.Description = description;
                if (!string.IsNullOrEmpty(creationDate)) existingArtwork.CreationDate = creationDate;
                if (!string.IsNullOrEmpty(medium)) existingArtwork.Medium = medium;
                if (!string.IsNullOrEmpty(imageURL)) existingArtwork.ImageURL = imageURL;
                if (int.TryParse(artistIDInput, out int artistID)) existingArtwork.ArtistID = artistID;

                bool isUpdated = artworkManagement.UpdateArtwork(existingArtwork);
                if (isUpdated)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Artwork updated successfully!");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Failed to update artwork!");
                    Console.ResetColor();

                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Artwork not found.");
                Console.ResetColor();
            }
        }

        private void RemoveArtwork()
        {

            
            Console.Write("Enter Artwork ID to remove: ");
            int artworkID = int.Parse(Console.ReadLine());
            
            List<int> list = new List<int>(artworkManagement.GetUsersByFavoriteArtwork(artworkID));

            foreach (int item in list)
            {
                userFav.RemoveArtworkFromFavorite(item, artworkID);
            }
            galleryManagement.RemoveArtworkFromAllGalleries(artworkID);
            bool isRemoved = artworkManagement.RemoveArtwork(artworkID);
            if (isRemoved)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Artwork removed successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Artwork with ID {artworkID} not found.");
                Console.ResetColor();
            }
        }

        private void SearchArtworks()
        {
            Console.Write("Enter keyword to search: ");
            string keyword = Console.ReadLine();
            List<Artwork> artworks = artworkManagement.SearchArtworks(keyword);
            if (artworks.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var artwork in artworks)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {artwork.ArtworkID}");
                    Console.WriteLine($"Title: {artwork.Title}");
                    Console.WriteLine($"Artist Name: {art.GetArtistNameByArtworkID(artwork.ArtworkID)}");
                    Console.WriteLine($"Discription: {artwork.Description}");
                    
                    Console.WriteLine(new string('\u2500', 100));
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No artworks found matching the given keyword.");
                Console.ResetColor();
            }
        }

        private void AddArtworkToFavorites()
        {
            bool continueAdding = true;  
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            do
            {
                Console.Write("Enter Artwork ID to add to favorites: ");
                int artworkId = int.Parse(Console.ReadLine());

                bool isAdded = userFav.AddArtworkToFavorite(userId, artworkId);
                if (isAdded)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Artwork added to favorites successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to add artwork to favorites.");
                    Console.ResetColor();
                }

                Console.WriteLine("Do you want to add another artwork to favorites? (y/n): ");
                string choice = Console.ReadLine().ToLower();

                if (choice != "y")
                {
                    continueAdding = false;
                }

            } while (continueAdding);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Thank you for updating your favorite artworks!");
            Console.ResetColor();
        }


        private void GetUserFavoriteArtworks()
        {
            Console.Write("Enter User ID to retrieve favorite artworks: ");
            int userId = int.Parse(Console.ReadLine());
            List<Artwork> favoriteArtworks = new List<Artwork>(userFav.GetUserFavoriteArtworks(userId));

            int no = 1;

            
            if (favoriteArtworks.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var item in favoriteArtworks)
                {
                    Console.WriteLine($"Artwork Details:{no++}");
                    Console.WriteLine($"ID: {item.ArtworkID}");
                    Console.WriteLine($"Title: {item.Title}");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Creation Date: {item.CreationDate}");
                    Console.WriteLine($"Medium: {item.Medium}");
                    Console.WriteLine($"Artist Name: {art.GetArtistNameByArtworkID(item.ArtistID)}");
                    Console.WriteLine(new string('\u2500', 100));

                }
                Console.ResetColor();

            }
            else
            {
                Console.WriteLine($"Artwork for UserId {userId} is not found.");
            }
        }

        private void RemoveArtworkFromFavorites()
        {
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter Artwork ID to remove from favorites: ");
            int artworkId = int.Parse(Console.ReadLine());

            bool isRemoved = userFav.RemoveArtworkFromFavorite(userId, artworkId);
            if (isRemoved)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Artwork removed from favorites successfully!");
                Console.ResetColor();


            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Failed to remove artwork from favorites.");
                Console.ResetColor();

            }
        }

        private GalleryManagement galleryManagement = new GalleryManagement();

        public void Gallery()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Artwork Gallery Menu ===");
                Console.WriteLine("1. Add Artwork to Gallery");
                Console.WriteLine("2. Get Artworks in Gallery");
                Console.WriteLine("3. Search Artwork in Gallery");
                Console.WriteLine("4. Remove Artwork from Gallery");
                Console.WriteLine("5. Get Galleries Curated by Artist");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddArtworkToGallery();
                        break;
                    case "2":
                        GetArtworksInGallery();
                        break;
                    case "3":
                        SearchArtworkInGallery();
                        break;
                    case "4":
                        RemoveArtworkFromGallery();
                        break;
                    case "5":
                        GetGalleriesCuratedByArtist();
                        break;
                    case "6":
                        return; 
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void AddArtworkToGallery()
        {
            Console.Write("Enter Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());

            Console.Write("Enter Gallery ID: ");
            int galleryID = int.Parse(Console.ReadLine());

            int result = galleryManagement.AddArtworkToGallery(artworkID, galleryID);
            Console.WriteLine(result > 0 ? "Artwork added successfully." : "Failed to add artwork.");
        }

        private void GetArtworksInGallery()
        {
            Console.Write("Enter Gallery ID: ");
            int galleryID = int.Parse(Console.ReadLine());

            List<List<string>> artworks = galleryManagement.GetArtworksInGallery(galleryID);
            Console.WriteLine($"Artworks in Gallery {galleryID}:");
            foreach (var artwork in artworks)
            {
                Console.WriteLine($"ID: {artwork[0]}, Title: {artwork[1]}, Description: {artwork[2]}");
            }
        }

        private void SearchArtworkInGallery()
        {
            Console.Write("Enter Gallery ID: ");
            int galleryID = int.Parse(Console.ReadLine());

            Console.Write("Enter Title to Search: ");
            string title = Console.ReadLine();

            List<List<string>> artworks = galleryManagement.SearchArtworkInGallery(galleryID, title);
            Console.WriteLine($"Search Results in Gallery {galleryID}:");
            foreach (var artwork in artworks)
            {
                Console.WriteLine($"ID: {artwork[0]}, Title: {artwork[1]}, Description: {artwork[2]}");
            }
        }

        private void RemoveArtworkFromGallery()
        {
            Console.Write("Enter Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());

            Console.Write("Enter Gallery ID: ");
            int galleryID = int.Parse(Console.ReadLine());

            int result = galleryManagement.RemoveArtworkFromGallery(artworkID, galleryID);
            Console.WriteLine(result > 0 ? "Artwork removed successfully." : "Failed to remove artwork.");
        }

        private void GetGalleriesCuratedByArtist()
        {
            Console.Write("Enter Curator ID: ");
            int curatorID = int.Parse(Console.ReadLine());

            List<List<string>> galleries = galleryManagement.GetGalleriesCuratedByArtist(curatorID);
            Console.WriteLine($"Galleries curated by Artist {curatorID}:");
            foreach (var gallery in galleries)
            {
                Console.WriteLine($"ID: {gallery[0]}, Name: {gallery[1]}");
            }
        }
    }   
}
