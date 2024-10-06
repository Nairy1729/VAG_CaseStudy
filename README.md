# Virtual Art Gallery

## Overview
The **Virtual Art Gallery** project is a console-based application designed to provide users with an immersive platform to explore, manage, and interact with artworks and galleries. This system allows users to manage artworks, favorite their preferred pieces, and curate their own virtual collections. It is implemented using **C# OOP principles** with a strong focus on SQL, control flow statements, exception handling, and database interaction.

## Features
### 1. Artwork Management
- Add new artworks to the gallery.
- Update existing artwork details.
- Remove artwork from the gallery.
- Retrieve details of a specific artwork by its ID.
- Search for artworks by keywords.

### 2. Personal Galleries & Favorites
- Users can add artworks to their favorites.
- Remove artworks from their favorites.
- Retrieve a list of all favorite artworks of a user.

### Key Directories:
- **entity**: Contains the entity classes that represent the core objects (Artwork, Artist, User, Gallery).
- **dao**: Contains the interfaces and their implementation for handling the database interactions.
- **exception**: Houses custom exception classes.
- **util**: Contains utility classes for database connections and property file handling.
- **main**: Contains the `MainModule.cs` which drives the application using a menu-based system.
- **test**: Contains the unit tests to ensure the functionality of core features.

## Schema Design
The database schema for this project consists of the following tables and relationships:

### Tables:
- **Artwork** (ArtworkID, Title, Description, CreationDate, Medium, ImageURL, ArtistID)
- **Artist** (ArtistID, Name, Biography, BirthDate, Nationality, Website, ContactInfo)
- **User** (UserID, Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
- **Gallery** (GalleryID, Name, Description, Location, Curator, OpeningHours)
- **User_Favorite_Artwork** (junction table for many-to-many relationship between User and Artwork)
- **Artwork_Gallery** (junction table for many-to-many relationship between Artwork and Gallery)

## Database Connectivity
The application interacts with an SQL database to store and retrieve data. A utility class `DBConnection` is used to manage database connections, with the connection properties read from a file using the `DBPropertyUtil` class.

### DBConnection Class:
- Manages the connection to the database using connection properties from a property file.
  
### DBPropertyUtil Class:
- Reads the property file and returns the connection string, including details like hostname, dbname, username, and password.

## Exception Handling
Custom exceptions are used to handle errors and invalid input:
- **ArtworkNotFoundException**: Thrown when an artwork ID is not found in the database.
- **UserNotFoundException**: Thrown when a user ID is not found in the database.

## Unit Testing
Unit tests are written to validate the core functionality of the system, including:
- Adding, updating, and removing artworks.
- Managing galleries and users' favorite artworks.
- Database interaction and exception handling.

