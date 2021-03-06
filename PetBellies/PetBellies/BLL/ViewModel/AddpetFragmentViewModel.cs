﻿using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.IO;

namespace PetBellies.BLL.ViewModel
{
    public class AddpetFragmentViewModel
    {
        public string AddPetAsync(bool addedPhoto, Stream f, Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name) || string.IsNullOrEmpty(pet.PetType))
            {
                return English.YouHaveToFillAllEntries();
            }
            else if (addedPhoto)
            {
                pet.Profilepicture = new Segédfüggvények().ReadFully(f);
            }
            else pet.Profilepicture = null;

            pet.Uploader = GlobalVariables.ActualUser.id;

            int success = GlobalVariables.databaseConnection.AddPet(pet);

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

                GlobalVariables.AddedPet = true;

                return English.Empty();
            }
        }
    }
}