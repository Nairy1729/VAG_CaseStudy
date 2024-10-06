
create database VirtualArtGallery
use VirtualArtGallery

CREATE TABLE Artwork (
    ArtworkID INT PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    CreationDate DATE,
    Medium NVARCHAR(100),
    ImageURL NVARCHAR(255),
    ArtistID INT,
    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
);

CREATE TABLE Artist (
    ArtistID INT PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Biography NVARCHAR(MAX),
    BirthDate DATE,
    Nationality NVARCHAR(100),
    Website NVARCHAR(255),
    ContactInformation NVARCHAR(255)
);


CREATE TABLE Users(
    UserID INT PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    DateOfBirth DATE,
    ProfilePicture NVARCHAR(255)
);


CREATE TABLE Gallery (
    GalleryID INT PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Location NVARCHAR(255),
    Curator INT,
    OpeningHours NVARCHAR(100),
    FOREIGN KEY (Curator) REFERENCES Artist(ArtistID)
);


CREATE TABLE User_Favorite_Artwork (
    UserID INT,
    ArtworkID INT,
    PRIMARY KEY (UserID, ArtworkID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);


CREATE TABLE Artwork_Gallery (
    ArtworkID INT,
    GalleryID INT,
    PRIMARY KEY (ArtworkID, GalleryID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (GalleryID) REFERENCES Gallery(GalleryID)
);




-- Insert data into Artist table
INSERT INTO Artist (ArtistID, Name, Biography, BirthDate, Nationality, Website, ContactInformation)
VALUES 
(1, 'Raja Ravi Varma', 'Famous Indian painter known for his depiction of Indian myths.', '1848-04-29', 'Indian', 'https://rajavarma.com', 'raja.varma@example.com'),
(2, 'Leonardo da Vinci', 'Renaissance polymath, painter of Mona Lisa and The Last Supper.', '1452-04-15', 'Italian', 'https://davinci.com', 'leo.davinci@example.com'),
(3, 'Frida Kahlo', 'Mexican painter known for her self-portraits.', '1907-07-06', 'Mexican', 'https://fridakahlo.com', 'frida.kahlo@example.com'),
(4, 'Vincent van Gogh', 'Post-impressionist painter, famous for Starry Night.', '1853-03-30', 'Dutch', 'https://vangogh.com', 'vincent.vangogh@example.com'),
(5, 'Pablo Picasso', 'Spanish painter and sculptor, co-founder of the Cubist movement.', '1881-10-25', 'Spanish', 'https://picasso.com', 'pablo.picasso@example.com'),
(6, 'Claude Monet', 'Founder of French Impressionism.', '1840-11-14', 'French', 'https://monet.com', 'claude.monet@example.com'),
(7, 'Salvador Dalí', 'Spanish surrealist artist known for The Persistence of Memory.', '1904-05-11', 'Spanish', 'https://dali.com', 'salvador.dali@example.com'),
(8, 'Georgia O Keeffe', 'American artist known for her paintings of enlarged flowers.', '1887-11-15', 'American', 'https://okeeffe.com', 'georgia.okeeffe@example.com'),
(9, 'Michelangelo', 'Italian sculptor, painter, architect, and poet of the Renaissance.', '1475-03-06', 'Italian', 'https://michelangelo.com', 'michelangelo@example.com'),
(10, 'Amrita Sher-Gil', 'Indian-Hungarian painter considered as a pioneer of modern Indian art.', '1913-01-30', 'Indian', 'https://amritashergil.com', 'amrita.shergil@example.com');

-- Insert data into Artwork table
INSERT INTO Artwork (ArtworkID, Title, Description, CreationDate, Medium, ImageURL, ArtistID)
VALUES 
(1, 'Mona Lisa', 'Portrait of a woman with an enigmatic expression.', '1503-06-01', 'Oil on canvas', 'https://monalisa.com/mona_lisa.jpg', 2),
(2, 'The Starry Night', 'Depiction of a swirling night sky.', '1889-06-01', 'Oil on canvas', 'https://starrynight.com/starry_night.jpg', 4),
(3, 'Self-Portrait with Thorn Necklace', 'Frida Kahlos self-portrait with thorn necklace.', '1940-09-01', 'Oil on canvas', 'https://frida.com/self_portrait.jpg', 3),
(4, 'The Persistence of Memory', 'Surrealist painting featuring melting clocks.', '1931-04-01', 'Oil on canvas', 'https://dali.com/persistence_of_memory.jpg', 7),
(5, 'Guernica', 'A mural-sized oil painting depicting the bombing of Guernica.', '1937-06-01', 'Oil on canvas', 'https://picasso.com/guernica.jpg', 5),
(6, 'The Last Supper', 'Depiction of Jesus and his disciples.', '1498-01-01', 'Oil and tempera on plaster', 'https://davinci.com/last_supper.jpg', 2),
(7, 'Water Lilies', 'Series of paintings depicting Monets flower garden.', '1920-01-01', 'Oil on canvas', 'https://monet.com/water_lilies.jpg', 6),
(8, 'The Creation of Adam', 'Detail of the Sistine Chapel ceiling.', '1512-01-01', 'Fresco', 'https://michelangelo.com/creation_of_adam.jpg', 9),
(9, 'Young Girls', 'Depiction of young girls in rural India.', '1932-01-01', 'Oil on canvas', 'https://amritashergil.com/young_girls.jpg', 10),
(10, 'Bharat Mata', 'Depiction of the Indian mother goddess.', '1905-01-01', 'Watercolor on paper', 'https://rajavarma.com/bharat_mata.jpg', 1);

-- Insert data into Users table
INSERT INTO Users (UserID, Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
VALUES 
(1, 'art_lover01', 'password123', 'artlover01@example.com', 'Rahul', 'Sharma', '1990-02-15', 'https://profilepictures.com/rahul.jpg'),
(2, 'frida_fan', 'password123', 'fridafan@example.com', 'Aditi', 'Verma', '1985-07-20', 'https://profilepictures.com/aditi.jpg'),
(3, 'van_gogh_viewer', 'password123', 'vangoghviewer@example.com', 'Ravi', 'Kumar', '1992-11-11', 'https://profilepictures.com/ravi.jpg'),
(4, 'monet_enthusiast', 'password123', 'monetenthusiast@example.com', 'Megha', 'Singh', '1994-03-29', 'https://profilepictures.com/megha.jpg'),
(5, 'picasso_fan', 'password123', 'picassofan@example.com', 'Rohan', 'Mishra', '1988-06-10', 'https://profilepictures.com/rohan.jpg'),
(6, 'dali_dreamer', 'password123', 'dalidreamer@example.com', 'Sneha', 'Kapoor', '1991-09-01', 'https://profilepictures.com/sneha.jpg'),
(7, 'okeeffe_admirer', 'password123', 'okeeffeadmirer@example.com', 'Arjun', 'Jain', '1993-05-17', 'https://profilepictures.com/arjun.jpg'),
(8, 'da_vinci_lover', 'password123', 'davincilover@example.com', 'Vishal', 'Patel', '1989-08-25', 'https://profilepictures.com/vishal.jpg'),
(9, 'michelangelo_fan', 'password123', 'michelangelofan@example.com', 'Priya', 'Saxena', '1995-12-10', 'https://profilepictures.com/priya.jpg'),
(10, 'amrita_art', 'password123', 'amritaart@example.com', 'Anita', 'Rao', '1990-01-25', 'https://profilepictures.com/anita.jpg');

-- Insert data into Gallery table
INSERT INTO Gallery (GalleryID, Name, Description, Location, Curator, OpeningHours)
VALUES 
(1, 'Louvre', 'Famous museum in Paris housing Mona Lisa.', 'Paris, France', 2, '09:00-18:00'),
(2, 'Van Gogh Museum', 'Museum dedicated to the works of Vincent van Gogh.', 'Amsterdam, Netherlands', 4, '10:00-17:00'),
(3, 'Frida Kahlo Museum', 'Museum dedicated to the life and works of Frida Kahlo.', 'Mexico City, Mexico', 3, '10:00-18:00'),
(4, 'Museum of Modern Art', 'American museum housing modern and contemporary art.', 'New York, USA', 7, '10:30-17:30'),
(5, 'The National Gallery', 'Houses a rich collection of Western European art.', 'London, UK', 6, '10:00-18:00'),
(6, 'Picasso Museum', 'Museum showcasing Picassos works.', 'Barcelona, Spain', 5, '09:00-19:00'),
(7, 'The Uffizi', 'Prominent art museum in Florence.', 'Florence, Italy', 9, '08:15-18:50'),
(8, 'The Art Institute of Chicago', 'Famous art museum in Chicago.', 'Chicago, USA', 8, '10:30-17:00'),
(9, 'National Gallery of Modern Art', 'Museum featuring modern Indian art.', 'New Delhi, India', 1, '11:00-18:30'),
(10, 'Amrita Sher-Gil Gallery', 'Dedicated to the works of Amrita Sher-Gil.', 'Mumbai, India', 10, '10:00-17:00');

-- Insert data into User_Favorite_Artwork table
INSERT INTO User_Favorite_Artwork (UserID, ArtworkID)
VALUES 
(1, 1), (1, 5), 
(2, 3), (2, 4), 
(3, 2), (3, 7), 
(4, 6), (4, 8), 
(5, 5), (5, 9), 
(6, 4), (6, 10), 
(7, 7), (7, 1), 
(8, 6), (8, 2), 
(9, 9), (9, 8), 
(10, 10), (10, 3);

delete from User_Favorite_Artwork

-- Insert data into Artwork_Gallery table
INSERT INTO Artwork_Gallery (ArtworkID, GalleryID)
VALUES 
(1, 1), (1, 7), 
(2, 2), (2, 5), 
(3, 3), (3, 10), 
(4, 4), (4, 8), 
(5, 6), (5, 9), 
(6, 1), (6, 7), 
(7, 5), (7, 8), 
(8, 7), (8, 2), 
(9, 9), (9, 3), 
(10, 10), (10, 4);




