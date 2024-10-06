using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.myexceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userID)
            : base($"User with ID {userID} not found in the database.")
        {
        }
    }
}
