using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetBellies.Model
{
    public class UserJustWithPicAndName
    {
        public int id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public byte[] ProfilePicture
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }
}
