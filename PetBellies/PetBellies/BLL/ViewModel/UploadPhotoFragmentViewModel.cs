﻿using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class UploadPhotoFragmentViewModel
    {
        public List<Pet> GetMyPets(int userid)
        {
            return GlobalVariables.databaseConnection.GetPetsByUserID(userid);
        }

        public User GetUserByEmail(string userEmail)
        {
            return GlobalVariables.databaseConnection.GetUserByEmail(userEmail);
        }

        public string UploadPictureAsync(bool addedPhoto, Stream f, int petid, string hashtag)
        {
            if (petid == -1)
            {
                return English.ChooseAnimal();
            }
            if (addedPhoto)
            {
                Petpictures petpictures = new Petpictures()
                {
                    PetID = petid,
                    PictureURL = new Segédfüggvények().ReadFully(f),
                    UploadDate = DateTime.UtcNow.ToString("")
                };

                int success = GlobalVariables.databaseConnection.UploadPhoto(petpictures);

                if (success == -1)
                {
                    return English.SomethingWentWrong();
                }
                else
                {
                    if (!string.IsNullOrEmpty(hashtag))
                    {
                        var hashtags = hashtag.Trim().Split('#');

                        bool uploadedHashtag = true;

                        foreach (var item in hashtags)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                Hashtags ahashtag = new Hashtags();

                                ahashtag.hashtag = item.Replace(" ", string.Empty);
                                ahashtag.petpicturesid = success;

                                uploadedHashtag = GlobalVariables.databaseConnection.InsertHashtags(ahashtag);

                                if (!uploadedHashtag)
                                {
                                    return English.SomethingWentWrong();
                                }
                            }
                        }
                    }

                    return English.Empty();
                }
            }
            else
            {
                return English.ChooseAPicture();
            }
        }
    }
}
