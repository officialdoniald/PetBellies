using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class WallFromDB
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int PetID { get; set; }
        public int PetPicturesID { get; set; }
    }
}
