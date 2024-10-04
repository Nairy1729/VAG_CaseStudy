using System;
using System.Collections.Generic;
using VirtualArtGallery.Modals;
using VirtualArtGallery.Services;

namespace VirtualArtGallery
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
                Console.WriteLine("Welcome to the Virtual Art Gallery!");
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
                Console.Write("Your choice: ");

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
            Console.WriteLine(result > 0 ? "Artwork added successfully!" : "Failed to add artwork.");
        }

        private void GetArtworkById()
        {
            Console.Write("Enter Artwork ID: ");
            int artworkID = int.Parse(Console.ReadLine());
            Artwork artwork = artworkManagement.GetArtworkById(artworkID);
            if (artwork != null)
            {
                Console.WriteLine("Artwork Details:");
                Console.WriteLine($"ID: {artwork.ArtworkID}");
                Console.WriteLine($"Title: {artwork.Title}");
                Console.WriteLine($"Description: {artwork.Description}");
                Console.WriteLine($"Creation Date: {artwork.CreationDate}");
                Console.WriteLine($"Medium: {artwork.Medium}");
                Console.WriteLine($"Image URL: {artwork.ImageURL}");
                Console.WriteLine($"Artist ID: {artwork.ArtistID}");
            }
            else
            {
                Console.WriteLine("Artwork not found.");
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
                Console.Write("Creation Date (leave blank to keep current): ");
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
                Console.WriteLine(isUpdated ? "Artwork updated successfully!" : "Failed to update artwork.");
            }
            else
            {
                Console.WriteLine("Artwork not found.");
            }
        }

        private void RemoveArtwork()
        {
            Console.Write("Enter Artwork ID to remove: ");
            int artworkID = int.Parse(Console.ReadLine());
            bool isRemoved = artworkManagement.RemoveArtwork(artworkID);
            Console.WriteLine(isRemoved ? "Artwork removed successfully!" : "Failed to remove artwork.");
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
                    Console.WriteLine($"ID: {artwork.ArtworkID}, Title: {artwork.Title},Discription: {artwork.Description}, Artist ID: {artwork.ArtistID}");
                }
            }
            else
            {
                Console.WriteLine("No artworks found matching the given keyword.");
            }
        }

        private void AddArtworkToFavorites()
        {
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter Artwork ID to add to favorites: ");
            int artworkId = int.Parse(Console.ReadLine());

            bool isAdded = userFav.AddArtworkToFavorite(userId, artworkId);
            Console.WriteLine(isAdded ? "Artwork added to favorites successfully!" : "Failed to add artwork to favorites.");
        }

        private void GetUserFavoriteArtworks()
        {
            Console.Write("Enter User ID to retrieve favorite artworks: ");
            int userId = int.Parse(Console.ReadLine());
            List<Artwork> favoriteArtworks = new List<Artwork>(userFav.GetUserFavoriteArtworks(userId));

            int no = 1;

            
            if (favoriteArtworks.Count != 0)
            {
                foreach (var item in favoriteArtworks)
                {
                    Console.WriteLine($"Artwork Details:{no++}");
                    Console.WriteLine($"ID: {item.ArtworkID}");
                    Console.WriteLine($"Title: {item.Title}");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Creation Date: {item.CreationDate}");
                    Console.WriteLine($"Medium: {item.Medium}");
                    Console.WriteLine($"Image URL: {item.ImageURL}");
                    Console.WriteLine($"Artist ID: {item.ArtistID}");

                }

            }
            else
            {
                Console.WriteLine("Artwork not found.");
            }
        }

        private void RemoveArtworkFromFavorites()
        {
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter Artwork ID to remove from favorites: ");
            int artworkId = int.Parse(Console.ReadLine());

            bool isRemoved = userFav.RemoveArtworkFromFavorite(userId, artworkId);
            Console.WriteLine(isRemoved ? "Artwork removed from favorites successfully!" : "Failed to remove artwork from favorites.");
        }
    }

    
}
