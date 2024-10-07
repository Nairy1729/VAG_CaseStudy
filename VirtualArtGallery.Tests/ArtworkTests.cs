using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VirtualArtGallery.Modals;  // Access to Artwork, Gallery, etc.
using VirtualArtGallery.Services; // Access to ArtworkManagement
using VirtualArtGallery.myexceptions; // Access to custom exceptions




[TestFixture]
public class ArtworkTests
{
    public ArtworkManagement _artworkManagement;

    [SetUp]
    public void Setup()
    {
        _artworkManagement = new ArtworkManagement(); // Ensure correct instantiation
    }

    [Test]
    public void WhenValidArtworkIsAdded()
    {
        Artwork newArtwork = new Artwork
        {
            ArtworkID = 103,
            Title = "Starry Night",
            Description = "A painting by Van Gogh",
            CreationDate = "1889-06-01",
            Medium = "Oil on canvas",
            ImageURL = "https://example.com/starrynight.jpg",
            ArtistID = 1
        };

        int result = _artworkManagement.AddArtwork(newArtwork);

        Assert.That(result, Is.GreaterThan(0), "Valid artwork should be added successfully.");
    }

    [Test]
    public void WhenInvalidArtworkIsAdded()
    {
        // Arrange
        Artwork invalidArtwork = null; // Simulating invalid artwork

        // Act
        int result = _artworkManagement.AddArtwork(invalidArtwork);

        // Assert
        Assert.That(result, Is.EqualTo(-1), "Adding invalid artwork should return -1.");
    }

    //[Test]
    //public void UpdateArtwork_ValidArtwork_ReturnsTrue()
    //{
    //    // Arrange
    //    Artwork updatedArtwork = new Artwork
    //    {
    //        ArtworkID = 1, // Assuming this artwork already exists in the DB
    //        Title = "Vultures",
    //        Description = "a hungry vulture",
    //        CreationDate = "20-05-2003",
    //        Medium = "Oil",
    //        ImageURL = "http://example.com/updatedartwork.jpg",
    //        ArtistID = 1
    //    };

    //    // Act
    //    bool result = _artworkManagement.UpdateArtwork(updatedArtwork);
    //    int ans1 = 0;
    //    if (result)
    //    {
    //        ans1 = 1;
    //    }

    //    // Assert

    //    //Assert.IsTrue(result, "Valid artwork should be updated successfully.");
    //    Assert.That(ans1, Is.EqualTo(1));
    //}

    //[Test]
    //public void RemoveArtwork_ValidArtworkID_ReturnsTrue()
    //{
    //    // Arrange
    //    int artworkIDToRemove = 1; // Assuming artwork with this ID exists

    //    // Act
    //    bool result = artworkManagement.RemoveArtwork(artworkIDToRemove);

    //    // Assert
    //    Assert.That(result, Is.True, "The artwork should be removed successfully.");
    //}




    //[Test]
    //public void SearchArtworkByTitle_ExistingTitle_ReturnsArtworks()
    //{
    //    // Arrange
    //    string searchTitle = "Starry Night"; // Assuming this title exists in the DB

    //    // Act
    //    List<Artwork> result = _artworkManagement.SearchArtworks(searchTitle);

    //    // Assert
    //    Assert.IsNotNull(result, "The search result should not be null.");
    //    Assert.IsTrue(result.Count > 0, "At least one artwork should be found with the given title.");
    //    Assert.IsTrue(result.Any(a => a.Title.Contains(searchTitle)), "Search should return artworks with matching titles.");
    //}



}



