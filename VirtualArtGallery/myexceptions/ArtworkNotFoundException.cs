using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.myexceptions
{
    public class ArtWorkNotFoundException : Exception
    {
        public ArtWorkNotFoundException(int artworkID)
            : base($"Artwork with ArtworkID {artworkID} not found in the database.")
        {
        }
    }
}
