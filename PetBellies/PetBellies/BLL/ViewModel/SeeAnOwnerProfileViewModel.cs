using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class SeeAnOwnerProfileViewModel
    {
        public User GetUser(int userid)
        {
            return GlobalVariables.databaseConnection.GetUserByID(userid);
        }

        public User GetUserByEmail(string userEmail)
        {
            return GlobalVariables.databaseConnection.GetUserByEmail(userEmail);
        }

        public List<Pet> GetPet(int userid)
        {
            return GlobalVariables.databaseConnection.GetPetsByUserID(userid);
        }

        public bool IsItABlockedUser(int userid, int blockedid)
        {
            return GlobalVariables.databaseConnection.IsItABlockedUser(userid, blockedid);
        }

        public List<Following> GetUsersFollowing(int id)
        {
            return GlobalVariables.databaseConnection.GetFollowingByuserID(id);
        }
        
        public bool ReportUser(User userid)
        {
            bool reported = GlobalVariables.databaseConnection.UpdateUserReported(userid);

            if (reported)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
