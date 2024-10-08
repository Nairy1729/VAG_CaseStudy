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
                Console.WriteLine("1. Add Artwork");
                Console.WriteLine("2. Get Artwork by ID");
                Console.WriteLine("3. Update Artwork");
                Console.WriteLine("4. Remove Artwork");
                Console.WriteLine("5. Search Artworks");
                Console.WriteLine("6. Add Artwork to Favorites");
                Console.WriteLine("7. Get User Favorite Artworks");
                Console.WriteLine("8. Remove Artwork from Favorites");
                Console.WriteLine("9. Exit");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Your choice: ");
                Console.ResetColor();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GetArtwork1();
                        AddArtwork();
                        break;
                    case "2":
                        GetArtworkById();
                        break;
                    case "3":
                        GetArtwork1();
                        UpdateArtwork();
                        break;
                    case "4":
                        GetArtwork1();
                        RemoveArtwork();
                        break;
                    case "5":
                        SearchArtworks();
                        break;
                    case "6":
                        AddArtworkToFavorites();
                        break;

                    case "7":
                        GetUserFavoriteArtworks();
                        break;
                    case "8":
                        RemoveArtworkFromFavorites();
                        break;

                    case "9":
                        Console.WriteLine("Exiting the program. Thank you!");
                        return; 
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }

        ArtistManagement art = new ArtistManagement();

     

        private void GetArtwork1()
        {
            List<Artwork> artworkList = artworkManagement.GetArtwork();

            if (artworkList.Count != 0)
            {
                const int idWidth = 5;
                const int titleWidth = 40;
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
            Console.Write("Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());
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

            Artwork newArtwork = new Artwork(artworkID, title, description, creationDate, medium, imageURL, artistID);
            int result = artworkManagement.AddArtwork(newArtwork);
            if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Artwork added successfull!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
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
            bool continueAdding = true;  // Variable to control whether the user wants to continue
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            do
            {
                //Console.Write("Enter User ID: ");
                //int userId = int.Parse(Console.ReadLine());
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

                // Ask if the user wants to add another artwork
                Console.WriteLine("Do you want to add another artwork to favorites? (y/n): ");
                string choice = Console.ReadLine().ToLower();

                if (choice != "y")
                {
                    continueAdding = false;  // Exit the loop if the user does not want to continue
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
    }

    
}
