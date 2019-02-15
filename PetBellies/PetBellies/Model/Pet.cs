using System;

namespace PetBellies.Model
{
    public class Pet
    {
        public int id { get; set; }

        public string Name { get; set; }

        public DateTime Age { get; set; }

        public string PetType { get; set; }

        public int HaveAnOwner { get; set; }

        public byte[] Profilepicture { get; set; }

        public int Uploader { get; set; }
    }
}