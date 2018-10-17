using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class SeePictureFragmentViewModel
    {
        public string likeText;
        public string unlikeText;

        public SeePictureFragmentViewModel()
        {
            likeText = English.Like();
            unlikeText = English.UnLike();
        }

        public string GetHashtags(int petpicturesid)
        {
            var hashtags = GlobalVariables.databaseConnection.GetHashtagsByPetpictureID(petpicturesid);

            string returnString = "";

            foreach (var item in hashtags)
            {
                returnString = returnString + "#" + item.hashtag + " ";
            }

            return returnString;
        }

        public string LikeClick(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = GlobalVariables.databaseConnection.InsertLikes(likes);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }

        public string UnLikeClick(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = GlobalVariables.databaseConnection.DeleteLikesNotByParam(likes);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }

        public Pet GetPetById(int petid)
        {
            return GlobalVariables.databaseConnection.GetPetByID(petid);
        }

        public List<Likes> GetLikes(int petpicturesid)
        {
            return GlobalVariables.databaseConnection.GetLikeByPetpicturesID(petpicturesid);
        }

        public bool HaveILiked(int petpicturesid)
        {
            Likes like = GlobalVariables.databaseConnection.GetLikeByUserID(GlobalVariables.ActualUser.id, petpicturesid);

            if (like is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Petpictures GetPetpicturesByID(int id)
        {
            return GlobalVariables.databaseConnection.GetOnePetpicturesByID(id);
        }

        public bool DeletePicture(Petpictures petpictures)
        {
            //TODO: Delet from DB
            return GlobalVariables.databaseConnection.DeletePetpictures(petpictures);
        }
    }
}
