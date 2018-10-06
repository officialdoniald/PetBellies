using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class Petpictures
    {
        public int id { get; set; }

        public int PetID { get; set; }

        public string PictureURL { get; set; }

        public string UploadDate { get; set; }
    }
}
