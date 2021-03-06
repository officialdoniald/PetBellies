﻿using PetBellies.BLL.ViewModel;
using PetBellies.DAL;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PetBellies.BLL.Helper
{
    public static class GlobalVariables
    {
        public static ObservableCollection<WallListViewAdapter> wallListViewAdapter =
            new ObservableCollection<WallListViewAdapter>();

        public static MyAccountPageViewModel myAccountPageViewModel =
            new MyAccountPageViewModel();

        public static SignupPageViewModel signupPageViewModel =
            new SignupPageViewModel();

        public static BlockedPeopleViewModel blockedPeopleViewModel =
            new BlockedPeopleViewModel();

        public static ForgotPasswordPageViewModel forgotPasswordPageViewModel =
            new ForgotPasswordPageViewModel();

        public static WhosLikedViewModel whosLikedViewModel =
            new WhosLikedViewModel();

        public static AddpetFragmentViewModel addpetFragmentViewModel =
                new AddpetFragmentViewModel();

        public static UploadPhotoFragmentViewModel uploadPhotoFragmentViewModel =
            new UploadPhotoFragmentViewModel();

        public static UpdateProfileFragmentViewModel updateProfileFragmentViewModel
            = new UpdateProfileFragmentViewModel();

        public static UpdatePetFragmentViewModel updatePetFragmentViewModel =
            new UpdatePetFragmentViewModel();

        public static LoginPageViewModel loginPageViewModel =
            new LoginPageViewModel();

        public static HomeFragmentViewModel homeFragmentViewModel =
            new HomeFragmentViewModel();

        public static SearchFragmentViewModel searchFragmentViewModel =
            new SearchFragmentViewModel();

        public static SeeAnOwnerProfileViewModel seeAnOwnerProfileViewModel =
                new SeeAnOwnerProfileViewModel();

        public static OtherFragmentViewModel otherFragmentViewModel =
            new OtherFragmentViewModel();

        public static PetProfileFragmentViewModel petProfileFragmentViewModel =
            new PetProfileFragmentViewModel();

        public static SeePictureFragmentViewModel seePictureFragmentViewModel =
            new SeePictureFragmentViewModel();

        public static PeopleSearchPageViewModel peopleSearchPageViewModel =
            new PeopleSearchPageViewModel();

        public static FollowersViewModel followersViewModel =
            new FollowersViewModel();

        public static string peoplepng = "people.png";

        public static string likepng = "unlike.png";

        public static string unlikepng = "like.png";

        public static string homepng = "home.png";

        public static string searchpng = "search.png";

        public static string camerapng = "camera.png";

        public static string profilepng = "profile.png";

        public static string explorepng = "explore.png";

        public static string WebApiURL { get; set; } = "http://193.39.13.216/api/";

        public static Style NormalLabel;
        public static Style NavigationPageStyle;

        public static DatabaseConnections databaseConnection =
            new DatabaseConnections();

        /// <summary>
        /// Amelyiktől induljon.
        /// </summary>
        public static int PetPicturesStartIndex { get; set; } = 0;

        /// <summary>
        /// Hány itemet szedjen ki egyszerre.
        /// </summary>
        public static int PetPicturesCount { get; set; } = 20;

        /// <summary>
        /// Amelyiktől induljon.
        /// </summary>
        public static int WallStartIndex { get; set; } = 0;

        /// <summary>
        /// Hány itemet szedjen ki egyszerre.
        /// </summary>
        public static int WallCount { get; set; } = 10;

        /// <summary>
        /// Can I go back with the Back Button?
        /// </summary>
        public static bool CanIGoBackWithTheBackButton { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public static string EMAIL_TOKEN { get; set; } = "loginmail";

        /// <summary>
        /// Application password.
        /// </summary>
        public static string GlobalPassword { get; set; }

        /// <summary>
        /// DBConnectionString.
        /// </summary>
        public static string AzureDBConnectionString { get; set; }

        /// <summary>
        /// GlobalCasualImage.
        /// </summary>
        public static byte[] GlobalCasualImage;

        /// <summary>
        /// Selected picture from galery Stream.
        /// </summary>
        public static Stream SelectedImageFromGallery;

        /// <summary>
        /// Source of the selected galery picture.
        /// </summary>
        public static string SourceSelectedImageFromGallery;

        /// <summary>
        /// Is Pet deleted?
        /// </summary>
        public static bool IsPetDeleted { get; set; }

        /// <summary>
        /// Is Picture deleted?
        /// </summary>
        public static bool IsPictureDeleted { get; set; }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        public static bool IsUpdatedMyProfile { get; set; }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        public static bool AddedPet { get; set; }

        /// <summary>
        /// Is Photo added?
        /// </summary>
        public static bool AddedPhoto { get; set; }

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        public static List<string> MyPetsString {  get; set; }

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        public static User ActualUser { get; set; }

        /// <summary>
        /// What is the actual user's email
        /// </summary>
        private static string actualusersemail;

        public static string ActualUsersEmail
        {
            get => actualusersemail.ToLower();
            set => actualusersemail = value;
        }

        /// <summary>
        /// Already logged in?
        /// </summary>
        public static bool HaveToLogin { get; set; }

        /// <summary>
        /// My pets list.
        /// </summary>
        public static List<MyPetsList> Mypetlist { get; set; }

        /// <summary>
        /// Initializes the user.
        /// </summary>
        public static void InitializeUser()
        {
            ActualUser = databaseConnection.GetUserByEmail(ActualUsersEmail);
        }

        /// <summary>
        /// Initializes the GlobalCasualImage.
        /// </summary>
        public static void InitializeGlobalCasualImage()
        {
            GlobalCasualImage = databaseConnection.GetCasualImage();
        }

        /// <summary>
        /// Initializes the users email.
        /// </summary>
        public static void InitializeUsersEmail()
        {
            try
            {
                ActualUsersEmail = SecureStorage.GetAsync(GlobalVariables.EMAIL_TOKEN).Result;

                if (!string.IsNullOrEmpty(ActualUsersEmail))
                {
                    User user = databaseConnection.GetUserByEmail(ActualUsersEmail);

                    if (user is null)
                    {
                        HaveToLogin = true;
                    }
                    else
                    {
                        HaveToLogin = false;
                    }

                }
                else
                {
                    HaveToLogin = true;
                }
            }
            catch (Exception)
            {
                HaveToLogin = true;
            }
        }

        /// <summary>
        /// Initializes the users email variable.
        /// </summary>
        public static void InitializeUsersEmailVariable()
        {
            ActualUsersEmail = SecureStorage.GetAsync(GlobalVariables.EMAIL_TOKEN).Result;
        }

        /// <summary>
        /// Gets my pets and store to the local list.
        /// </summary>
        public static void GetMyPets()
        {
            var myPetList = uploadPhotoFragmentViewModel.GetMyPets(ActualUser.id);

            Mypetlist = new List<MyPetsList>();

            if (myPetList != null)
            {
                foreach (var item in myPetList)
                {
                    var pet = ConvertPetToMyPetList(item);

                    Mypetlist.Add(pet);
                }
            }
        }

        /// <summary>
        /// Sets my pet list string.
        /// </summary>
        public static void SetMyPetListString()
        {
            MyPetsString = new List<string>();

            foreach (var item in Mypetlist)
            {
                MyPetsString.Add(item.Name);
            }
        }

        /// <summary>
        /// Converts Mypetlist to Pet.
        /// </summary>
        /// <returns>The my pet list to pet.</returns>
        /// <param name="myPetsList">My pets list.</param>
        public static Pet ConvertMyPetListToPet(MyPetsList myPetsList)
        {
            if (myPetsList is null)
            {
                return null;
            }
            else
            {
                return new Pet()
                {
                    id = myPetsList.petid,
                    Age = myPetsList.Age,
                    HaveAnOwner = myPetsList.HaveAnOwner,
                    Name = myPetsList.Name,
                    PetType = myPetsList.PetType,
                    Profilepicture = myPetsList.ProfilePictureURL,
                    Uploader = myPetsList.Uploader
                };
            }
        }

        /// <summary>
        /// Converts the Pet to Mypetlist.
        /// </summary>
        /// <returns>The pet to my pet list.</returns>
        /// <param name="pet">Pet.</param>
        public static MyPetsList ConvertPetToMyPetList(Pet pet)
        {
            return new MyPetsList()
            {
                Age = pet.Age,
                HaveAnOwner = pet.HaveAnOwner,
                Name = pet.Name,
                petid = pet.id,
                PetType = pet.PetType,
                ProfilePictureURL = pet.Profilepicture,
                Uploader = pet.Uploader
            };
        }
    }
}