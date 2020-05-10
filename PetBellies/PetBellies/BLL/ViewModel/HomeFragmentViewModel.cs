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

            List<WallFromDB> wallFromDB = new List<WallFromDB>();
            
            wallFromDB = GlobalVariables.databaseConnection.GetWallItemByUserIDBYRange();

            if (wallFromDB is null)
            {
                return new List<Wall>();
            }
            else
            {
                foreach (var item in wallFromDB)
                {
                    Wall wall = new Wall();
                    Pet pet = new Pet();

                    pet = GlobalVariables.databaseConnection.GetPetByID(item.PetID);
                    wall.petpictures = GlobalVariables.databaseConnection.GetPetPictureByID(item.PetPicturesID);
                    wall.name = pet.Name;
                    wall.howmanylikes = GlobalVariables.databaseConnection.GetLikesOnAPicture(item.PetPicturesID).Count;
                    wall.ProfilePictureURL = pet.Profilepicture;

                    wall.haveILiked = GlobalVariables.databaseConnection.GetLikesByPetpicturesIDAndUserID(GlobalVariables.ActualUser.id, item.PetPicturesID);

                    if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(GlobalVariables.ActualUser.id, pet.Uploader))
                    {
                        wallList.Add(wall);
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
