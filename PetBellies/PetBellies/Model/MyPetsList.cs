using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class MyPetsList
    {
        public int id { get; set; }

        public int petid { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PetType { get; set; }

        public int HaveAnOwner { get; set; }

        public int Uploader { get; set; }

        public byte[] ProfilePictureURL { get; set; }
    }
}
