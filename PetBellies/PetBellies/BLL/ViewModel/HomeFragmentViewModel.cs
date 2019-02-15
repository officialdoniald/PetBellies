using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class HomeFragmentViewModel
    {
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

        public List<Wall> GetWallList()
        {
            List<Wall> wallList = new List<Wall>();

            List<Petpictures> petpictureList = new List<Petpictures>();

            List<Following> followingList = new List<Following>();

            followingList = GlobalVariables.databaseConnection.GetFollowingByuserID(GlobalVariables.ActualUser.id);

            List<Petpictures> petpictures = new List<Petpictures>();

            if (followingList is null)
            {
                return new List<Wall>();
            }
            else
            {
                foreach (var item in followingList)
                {
                    petpictures = new List<Petpictures>();

                    petpictures = GlobalVariables.databaseConnection.GetPetPicturesByPetID(item.FUserID);

                    foreach (var item2 in petpictures)
                    {
                        Wall wall = new Wall();
                        Pet pet = new Pet();

                        pet = GlobalVariables.databaseConnection.GetPetByID(item.FUserID);
                        wall.petpictures = item2;
                        wall.name = pet.Name;
                        wall.howmanylikes = GlobalVariables.databaseConnection.GetLikeByPetpicturesID(item2.id).Count;
                        wall.ProfilePictureURL = pet.Profilepicture;

                        wall.haveILiked = GlobalVariables.databaseConnection.GetLikeByUserID(GlobalVariables.ActualUser.id, item2.id);

                        if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(pet.Uploader))
                        {
                            wallList.Add(wall);
                        }
                    }
                }
                return wallList;
            }
        }

        public string Unlike(int petpicturesid)
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

        public string LikePicture(int petpicturesid)
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
    }
}
