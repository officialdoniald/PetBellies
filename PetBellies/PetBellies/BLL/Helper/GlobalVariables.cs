using PetBellies.BLL.FileStoreAndLoad;
using PetBellies.BLL.ViewModel;
using PetBellies.DAL;
using PetBellies.Model;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PetBellies.BLL.Helper
{
    public static class GlobalVariables
    {
        public static List<WallListViewAdapter> wallListViewAdapter =
            new List<WallListViewAdapter>();

        public static SignupPageViewModel signupPageViewModel =
            new SignupPageViewModel();

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

        public static string logintxt = "login.txt";

        public static string peoplepng = "people.png";

        public static string likepng = "unlike.png";

        public static string unlikepng = "like.png";

        public static string homepng = "home.png";

        public static string searchpng = "search.png";

        public static string camerapng = "camera.png";

        public static string profilepng = "profile.png";

        public static string explorepng = "explore.png";

        public static Style NormalLabel;
        public static Style NavigationPageStyle;

        public static Stream f;

        public static string pathf = "";

        public static MediaFile mediaFile;

        public static string blobstorageurl =
            "https://officialdoniald.blob.core.windows.net/appmancs/";

        public static string databaseFileName =
            "LocalDatabaseTable.db3";

        public static string GlobalPassword = string.Empty;
        public static string AzureDBConnectionString = string.Empty;
        public static string AzureBlobStorageConnectionString = string.Empty;

        public static DatabaseConnections databaseConnection =
            new DatabaseConnections();

        public static BlobStorage blobStorage =
            new BlobStorage();

        public static Mailer mailer =
            new Mailer();

        /// <summary>
        /// The SMTP Mail server.
        /// </summary>
        public static SMTPUser SMTPUser = new SMTPUser()
        {
            SMTPCLientHost = "smtp.",
            SMTPEmail = "",
            SMTPPassword = GlobalPassword,
            SMTPServerPort = 587,
            SMTPUsername = ""
        };

        /// <summary>
        /// Is Pet deleted?
        /// </summary>
        private static bool isPetDeleted = false;

        public static bool IsPetDeleted
        {
            get => isPetDeleted;
            set => isPetDeleted = value;
        }

        /// <summary>
        /// Is Picture deleted?
        /// </summary>
        private static bool isPictureDeleted = false;

        public static bool IsPictureDeleted
        {
            get => isPictureDeleted;
            set => isPictureDeleted = value;
        }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        private static bool isUpdatedMyProfile = false;

        public static bool IsUpdatedMyProfile
        {
            get => isUpdatedMyProfile;
            set => isUpdatedMyProfile = value;
        }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        private static bool addedPet = false;

        public static bool AddedPet
        {
            get => addedPet;
            set => addedPet = value;
        }

        /// <summary>
        /// Is Photo added?
        /// </summary>
        private static bool addedPhoto = false;

        public static bool AddedPhoto
        {
            get => addedPhoto;
            set => addedPhoto = value;
        }

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        private static List<string> myPetsString;

        public static List<string> MyPetsString
        {
            get => myPetsString;
            set => myPetsString = value;
        }

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        private static User actualuser;

        public static User ActualUser
        {
            get => actualuser;
            set => actualuser = value;
        }

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
        private static bool havetologin;

        public static bool HaveToLogin
        {
            get => havetologin;
            set => havetologin = value;
        }

        /// <summary>
        /// My pets list.
        /// </summary>
        private static List<MyPetsList> mypetlist;

        public static List<MyPetsList> Mypetlist
        {
            get => mypetlist;
            set => mypetlist = value;
        }

        /// <summary>
        /// Initializes the user.
        /// </summary>
        public static void InitializeUser()
        {
            ActualUser = databaseConnection.GetUserByEmail(ActualUsersEmail);
        }

        /// <summary>
        /// Initializes the users email.
        /// </summary>
        public static void InitializeUsersEmail()
        {
            try
            {
                ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(logintxt);

                if (!String.IsNullOrEmpty(ActualUsersEmail))
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
            ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(logintxt);
        }

        /// <summary>
        /// Gets my pets and store to the local SQLite DB.
        /// </summary>
        public static void GetMyPets()
        {
            var myPetList = uploadPhotoFragmentViewModel.GetMyPets(ActualUser.id);

            Mypetlist = new List<MyPetsList>();

            foreach (var item in myPetList)
            {
                var pet = ConvertPetToMyPetList(item);

                Mypetlist.Add(pet);
            }
        }

        /// <summary>
        /// Sets my pet list string.
        /// </summary>
        public static void SetMyPetListString()
        {
            MyPetsString = new List<string>();

            foreach (var item in GlobalVariables.Mypetlist)
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
                    ProfilePictureURL = myPetsList.ProfilePictureURL,
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
                ProfilePictureURL = pet.ProfilePictureURL,
                Uploader = pet.Uploader
            };
        }

        /// <summary>
        /// Event handler when connection changes
        /// </summary>
        static event ConnectivityChangedEventHandler ConnectivityChanged;

        public class ConnectivityChangedEventArgs : EventArgs
        {
            public bool IsConnected { get; set; }

        }

        public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);

        public class MyPetsList
        {
            public int id { get; set; }

            public int petid { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }

            public string PetType { get; set; }

            public int HaveAnOwner { get; set; }

            public int Uploader { get; set; }

            public string ProfilePictureURL { get; set; }
        }
    }
}
