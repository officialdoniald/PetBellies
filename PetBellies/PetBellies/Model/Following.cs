using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class Following
    {
        public int id { get; set; }

        public int UserID { get; set; }

        public int FUserID { get; set; }
    }
}
