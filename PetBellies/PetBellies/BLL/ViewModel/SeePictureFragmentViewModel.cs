using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Net;
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
            return GlobalVariables.databaseConnection.GetLikesOnAPicture(petpicturesid);
        }

        public bool HaveILiked(int petpicturesid)
        {
            return GlobalVariables.databaseConnection.GetLikesByPetpicturesIDAndUserID(GlobalVariables.ActualUser.id, petpicturesid);
        }

        public bool ReportPicture(Petpictures petpicturesid)
        {
            bool reported = GlobalVariables.databaseConnection.UpdatePetpicturesReported(petpicturesid);

            if (reported)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUser(int id)
        {
            var reportedPet = GlobalVariables.databaseConnection.GetPetByID(id);

            if (reportedPet.Name != null)
            {
                var reported = GlobalVariables.databaseConnection.GetUserByID(reportedPet.Uploader);

                if (reported.Email != null)
                {
                    return reported;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Petpictures GetPetpicturesByID(int id)
        {
            return GlobalVariables.databaseConnection.GetPetPictureByID(id);
        }

        public bool DeletePicture(Petpictures petpictures)
        {
            return GlobalVariables.databaseConnection.DeletePetpictures(petpictures);
        }
    }
}
