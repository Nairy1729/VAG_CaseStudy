﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Modals
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public List<int> FavoriteArtworks { get; set; } 

        public User() { }

        public User(int userID, string username, string password, string email, string firstName, string lastName, string dateOfBirth, string profilePicture)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ProfilePicture = profilePicture;
            FavoriteArtworks = new List<int>();
        }
    }
}
