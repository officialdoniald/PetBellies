using PetBellies.BLL.Helper;
using PetBellies.Model;

namespace PetBellies.BLL.ViewModel
{
    public class OtherFragmentViewModel
    {
        public string DeleteAccount()
        {
            int i = 0;

            if (GlobalVariables.Mypetlist.Count != 0)
            {
                while (GlobalVariables.Mypetlist.Count != 0)
                {
                    if (!string.IsNullOrEmpty(GlobalVariables.updatePetFragmentViewModel.DeletePet(GlobalVariables.Mypetlist[i].petid)))
                    {
                        return English.SomethingWentWrong();
                    }

                    i++;
                }
            }

            GlobalVariables.Mypetlist = new System.Collections.Generic.List<MyPetsList>();

            if (!GlobalVariables.databaseConnection.DeleteAccount(GlobalVariables.ActualUser.id))
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
