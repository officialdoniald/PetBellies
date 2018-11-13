using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;

namespace PetBellies.BLL.ViewModel
{
    public class MyAccountPageViewModel
    {
        public List<Following> GetMyFollowing()
        {
            return GlobalVariables.databaseConnection.GetFollowingByuserID(GlobalVariables.ActualUser.id);
        }
    }
}