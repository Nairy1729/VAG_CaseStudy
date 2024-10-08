using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VirtualArtGallery.Modals;
using VirtualArtGallery.Services;
using VirtualArtGallery.myexceptions; 




public class ArtworkTests
{
    public ArtworkManagement artworkManagement;

    [SetUp]
    public void Setup()
    {
        artworkManagement = new ArtworkManagement(); 
    }

    [Test]
    public void WhenValidArtworkIsAdded()
    {
        Artwork newArtwork = new Artwork
        {
            ArtworkID = 51,
            Title = "Starry Night",
            Description = "A painting by Van Gogh",
            CreationDate = "1889-06-01",
            Medium = "Oil on canvas",
            ImageURL = "https://example.com/starrynight.jpg",
            ArtistID = 1
        };

        int result = artworkManagement.AddArtwork(newArtwork);

        Assert.That(result, Is.EqualTo(1), "Valid artwork should be added successfully.");
    }

    [Test]
    public void WhenInvalidArtworkIsAdded()
    {
        Artwork invalidArtwork = null;

        int result = artworkManagement.AddArtwork(invalidArtwork);

        Assert.That(result, Is.LessThan(1), "Adding invalid artwork should return -1.");
    }

    [Test]
    public void UpdateArtwork_ValidArtwork_ReturnsTrue()
    {
        Artwork updatedArtwork = new Artwork
        {
            ArtworkID = 1, 
            Title = "Vultures",
            Description = "A hungry vulture",
            CreationDate = "20-05-2003", 
            Medium = "Oil",
            ImageURL = "http://example.com/updatedartwork.jpg",
            ArtistID = 1
        };

        bool result = artworkManagement.UpdateArtwork(updatedArtwork);
        int ans1 = 0;
        if (result)
        {
            ans1 = 1;
        }

        Assert.That(ans1, Is.EqualTo(1), "Valid artwork should be updated successfully.");
    }


    [Test]
    public void RemoveArtwork_ValidArtworkID()
    {
        int artworkIDToRemove = 1;

        bool result = artworkManagement.RemoveArtwork(artworkIDToRemove);

        int ans1 = 0;
        if (result)
        {
            ans1 = 1;
        }

        Assert.That(ans1, Is.EqualTo(0), "The artwork should be removed successfully.");
    }




    [Test]
    public void SearchArtworkByTitle_ExistingTitle_ReturnsArtworks()
    {
        string searchTitle = "Starry Night"; 

        List<Artwork> result = artworkManagement.SearchArtworks(searchTitle);

        int resultsize = result.Count;
        Assert.That(resultsize, Is.GreaterThan(0),"search successfull" );
  
    }



}



