using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.Model
{
    public class Donates
    {
        public int id { get; set; }

        public int UserID { get; set; }

        public string DonateDate { get; set; }

        public int HowMany { get; set; }

        public string CashType { get; set; }

        public int PetID { get; set; }
    }
}
