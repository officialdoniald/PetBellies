using PetBellies.BLL.FileStoreAndLoad;
using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class UpdateProfileFragmentViewModel
    {
        private string UpdateUser(User user)
        {
            bool success = GlobalVariables.databaseConnection.UpdateUser(user.id, user);

            if (success)
            {
                GlobalVariables.ActualUser = user;

                return English.Empty();
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }

        public async Task<string> UpdateEmailAsync(string newEmail)
        {
            if (GlobalVariables.ActualUser.Email == newEmail)
            {
                return English.ThisEmailIsYourEmail();
            }
            if (!String.IsNullOrEmpty(newEmail))
            {
                GlobalVariables.ActualUser.Email = newEmail;

                User checkEmailExist = GlobalVariables.databaseConnection.GetUserByEmail(newEmail);

                if (!String.IsNullOrEmpty(checkEmailExist.Email))
                {
                    return English.ThisEmailIsExist();
                }
                else
                {
                    GlobalVariables.ActualUsersEmail = GlobalVariables.ActualUser.Email;

                    string url = String.Format("http://petbellies.com/php/petbellieschangeemail.php?email={0}&nev={1}", GlobalVariables.ActualUser.Email, GlobalVariables.ActualUser.FirstName);
                    Uri uri = new Uri(url);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    WebResponse res = await request.GetResponseAsync();

                    DependencyService.Get<IFileStoreAndLoad>().SaveText(GlobalVariables.logintxt, GlobalVariables.ActualUsersEmail);

                    return UpdateUser(GlobalVariables.ActualUser);
                }

            }

            return English.SomethingWentWrong();
        }

        public string UpdateProfile(string firstname, string lastname)
        {
            if (!String.IsNullOrEmpty(firstname))
            {
                GlobalVariables.ActualUser.FirstName = firstname;
            }
            if (!String.IsNullOrEmpty(lastname))
            {
                GlobalVariables.ActualUser.LastName = lastname;
            }

            return UpdateUser(GlobalVariables.ActualUser);
        }

        public async Task<string> UpdateProfilePicture(string uri, Stream stream)
        {
            if (!String.IsNullOrEmpty(uri))
            {
                string uniqueBlobName = await GlobalVariables.blobStorage.UploadFileAsync(uri, stream);

                uniqueBlobName = GlobalVariables.blobstorageurl + uniqueBlobName;

                GlobalVariables.ActualUser.ProfilePictureURL = uniqueBlobName;
            }
            else GlobalVariables.ActualUser.ProfilePictureURL = "";

            return UpdateUser(GlobalVariables.ActualUser);
        }

        public string UpdatePassword(string oldpassword, string newPassword)
        {
            if (String.IsNullOrEmpty(oldpassword) || String.IsNullOrEmpty(newPassword))
            {
                return English.ThisEmailIsExist();
            }

            if (GlobalVariables.ActualUser.Password != oldpassword)
            {
                return English.BadPasswordLength();
            }

            if (newPassword.Length < 6 && newPassword.Length > 16)
            {
                return English.BadPasswordLength();
            }

            GlobalVariables.ActualUser.Password = newPassword;

            return UpdateUser(GlobalVariables.ActualUser);
        }
    }
}
