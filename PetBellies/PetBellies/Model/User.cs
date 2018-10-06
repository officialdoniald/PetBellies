using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class User
    {
        public int id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FacebookId { get; set; }

        public string ProfilePictureURL { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
