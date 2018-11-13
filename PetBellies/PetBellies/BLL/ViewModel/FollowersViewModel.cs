using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class FollowersViewModel
    {
        public List<User> GetUserList(int petpicturesid)
        {
            List<User> userList = new List<User>();

            List<Following> likeList = GlobalVariables.databaseConnection.GetFollowingByfuserID(petpicturesid);

            foreach (var item in likeList)
            {
                User user = new User();

                user = GlobalVariables.databaseConnection.GetUserByID(item.UserID);

                userList.Add(user);
            }

            return userList;
        }

        public List<Pet> GetPetList(List<Following> followings)
        {
            List<Pet> petList = new List<Pet>();

            foreach (var item in followings)
            {
                Pet pet = new Pet();

                pet = GlobalVariables.databaseConnection.GetPetByID(item.FUserID);

                petList.Add(pet);
            }

            return petList;
        }

        public bool IsMyPet(int petid)
        {
            return GlobalVariables.whosLikedViewModel.IsMyPet(petid);
        }
    }
}
