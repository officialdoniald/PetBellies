using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace PetBellies.Model
{
    public class WallListViewAdapter : BindableObject
    {
        public Wall wallItem { get; set; }

        public ImageSource profilepictureURL { get; set; }

        public ImageSource pictureURL { get; set; }

        public string hashtags { get; set; }

        public string followButtonText { get; set; }

        public string petName { get; set; }

        public string howManyLikes { get; set; }
    }
}
