using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetBellies.Model
{
    public class Wall
    {
        //majd amikor forecheljük, számlálót veszünk fel és az lesz az id
        public int id { get; set; }

        public Petpictures petpictures { get; set; }

        public int howmanylikes { get; set; }

        public bool haveILiked { get; set; }

        public string name { get; set; }

        public byte[] ProfilePictureURL { get; set; }
    }
}
