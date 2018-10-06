using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class Pet
    {
        public int id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PetType { get; set; }

        public int HaveAnOwner { get; set; }

        public string ProfilePictureURL { get; set; }

        public int Uploader { get; set; }
    }
}
