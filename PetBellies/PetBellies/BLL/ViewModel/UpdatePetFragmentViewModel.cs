using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class UpdatePetFragmentViewModel
    {
        public string DeletePet(int petid)
        {
            var petpictureList = GlobalVariables.databaseConnection.GetPetpictureByID(petid);

            foreach (var item in petpictureList)
            {
                bool successD = GlobalVariables.seePictureFragmentViewModel.DeletePicture(item);

                if (!successD)
                {
                    return English.SomethingWentWrong();
                }
            }

            Pet pet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(i => i.petid == petid).FirstOrDefault());

            bool success = GlobalVariables.databaseConnection.DeletePet(pet);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                int i = 0;

                foreach (var item in GlobalVariables.Mypetlist)
                {
                    if (item.petid == petid)
                    {
                        GlobalVariables.Mypetlist.RemoveAt(i);
                        GlobalVariables.MyPetsString.RemoveAt(i);

                        break;
                    }

                    i++;
                }

                GlobalVariables.AddedPet = true;

                return English.Empty();
            }
        }

        public string UpdatePetProfile(Pet pet)
        {
            bool success = GlobalVariables.databaseConnection.UpdatePet(pet.id, pet);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                int i = 0;

                foreach (var item in GlobalVariables.Mypetlist)
                {
                    if (item.petid == pet.id)
                    {
                        GlobalVariables.Mypetlist[i].PetType = pet.PetType;
                        GlobalVariables.Mypetlist[i].Age = pet.Age;
                        GlobalVariables.Mypetlist[i].HaveAnOwner = pet.HaveAnOwner;
                        GlobalVariables.Mypetlist[i].Name = pet.Name;

                        GlobalVariables.MyPetsString[i] = pet.Name;

                        break;
                    }

                    i++;
                }

                GlobalVariables.AddedPet = true;

                return English.Empty();
            }
        }

        public async Task<string> UpdatePetProfilePictureAsync(Pet pet, Stream f, string uri)
        {
            if (!String.IsNullOrEmpty(uri))
            {
                pet.ProfilePictureURL = new Segédfüggvények().ReadFully(f);

                bool success = GlobalVariables.databaseConnection.UpdatePet(pet.id, pet);

                if (!success)
                {
                    return English.SomethingWentWrong();
                }
                else
                {
                    int i = 0;

                    foreach (var item in GlobalVariables.Mypetlist)
                    {
                        if (item.petid == pet.id)
                        {
                            GlobalVariables.Mypetlist[i].ProfilePictureURL = pet.ProfilePictureURL;

                            break;
                        }

                        i++;
                    }

                    GlobalVariables.AddedPet = true;

                    return English.Empty();
                }
            }
            else return English.Empty();
        }

        public Pet GetThisPet(int petid)
        {
            return GlobalVariables.databaseConnection.GetPetByID(petid);
        }
    }
}
