using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class WhosLikedViewModel
    {
        public List<User> GetUserList(int petpicturesid)
        {
            List<User> userList = new List<User>();

            List<Likes> likeList = GlobalVariables.databaseConnection.GetLikesOnAPicture(petpicturesid);

            foreach (var item in likeList)
            {
                User user = new User();

                user = GlobalVariables.databaseConnection.GetUserByID(item.UserID);

                userList.Add(user);
            }

            return userList;
        }

        public bool IsMyPet(int petid)
        {
            var pet = GlobalVariables.databaseConnection.GetPetByID(petid);

            if (pet.Uploader == GlobalVariables.ActualUser.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Pet GetPetByPetId(int petpicturesid)
        {
            return GlobalVariables.databaseConnection.GetPetByID(GlobalVariables.databaseConnection.GetPetPictureByID(petpicturesid).PetID);
        }
    }
}
