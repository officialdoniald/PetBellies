using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class AddpetFragmentViewModel
    {
        public async Task<string> AddPetAsync(string pathf, Stream f, Pet pet)
        {
            if (String.IsNullOrEmpty(pet.Name) || String.IsNullOrEmpty(pet.PetType))
            {
                return English.YouHaveToFillAllEntries();
            }
            else if (pet.Age < 0)
            {
                return English.NotNegNumber();
            }
            else if (!String.IsNullOrEmpty(pathf))
            {
                pet.ProfilePictureURL = new Segédfüggvények().ReadFully(f);
            }
            else pet.ProfilePictureURL = null;

            pet.Uploader = GlobalVariables.ActualUser.id;

            int success = GlobalVariables.databaseConnection.InsertPet(pet);

            if (success == -1)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                pet.id = success;

                var myPetList = GlobalVariables.ConvertPetToMyPetList(pet);

                GlobalVariables.Mypetlist.Add(myPetList);

                GlobalVariables.MyPetsString.Add(myPetList.Name);

                //GlobalVariables.SetMyPetListString();

                GlobalVariables.AddedPet = true;

                //GlobalVariables.LocalSQLiteDatabase.InsertMyPetsList(myPetList);

                return English.Empty();
            }
        }
    }
}
