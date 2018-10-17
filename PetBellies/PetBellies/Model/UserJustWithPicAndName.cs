using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

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

        public ImageSource ProfilePicture
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
