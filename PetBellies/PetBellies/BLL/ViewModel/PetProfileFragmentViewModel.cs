using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class PetProfileFragmentViewModel
    {
        public string followText;
        public string unfollowText;

        public PetProfileFragmentViewModel()
        {
            followText = English.Follow();
            unfollowText = English.UnFollow();
        }

        public List<Petpictures> GetPetPictureURL(int petid)
        {
            List<Petpictures> petpictureList = new List<Petpictures>();

            petpictureList = GlobalVariables.databaseConnection.GetPetPicturesByPetID(petid);

            return petpictureList;
        }

        public Pet GetPetFromDBByID(int petid)
        {
            Pet pet = new Pet();

            pet = GlobalVariables.databaseConnection.GetPetByID(petid);

            return pet;
        }

        public string UnFollow(string userEmail, int petid)
        {
            int userid = GlobalVariables.databaseConnection.GetUserByEmail(userEmail).id;

            bool success = GlobalVariables.databaseConnection.DeleteFollowing(userid, petid);

            if (success)
            {
                return English.Empty();
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }

        public bool HaveIAlreadyFollow(string userEmail, int petid)
        {
            int userid = GlobalVariables.databaseConnection.GetUserByEmail(userEmail).id;

            return GlobalVariables.databaseConnection.HaveIFollowedThisPet(userid, petid);
        }

        public string FollowAPet(string userEmail, int petid)
        {
            int userid = GlobalVariables.databaseConnection.GetUserByEmail(userEmail).id;

            Following following = new Following()
            {
                FUserID = petid,
                UserID = userid
            };

            bool success = GlobalVariables.databaseConnection.InsertFollowing(following);

            if (success)
            {
                return English.Empty();
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }
    }
}
