using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PetBellies.Model
{
    public class ListViewWithPictureAndSomeText
    {
        public string Name { get; set; }

        public ImageSource ProfilePicture { get; set; }

        public User user { get; set; }

        public Pet pet { get; set; }
    }
}
