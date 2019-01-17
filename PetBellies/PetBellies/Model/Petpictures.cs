using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetBellies.Model
{
    public class Petpictures
    {
        public int id { get; set; }

        public int PetID { get; set; }

        public byte[] PictureURL { get; set; }

        public string UploadDate { get; set; }

        public int Reported { get; set; }
    }
}
