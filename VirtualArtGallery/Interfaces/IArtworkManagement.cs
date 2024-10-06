using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Modals;

namespace VirtualArtGallery.Interfaces
{
    public interface IArtworkManagement
    {
        int AddArtwork(Artwork artwork);
        bool UpdateArtwork(Artwork artwork);
        bool RemoveArtwork(int artworkID);
        Artwork GetArtworkById(int artworkID);
        List<Artwork> SearchArtworks(string keyword);

        public string GetArtistNameByArtworkId(int artworkID);

    }
}
