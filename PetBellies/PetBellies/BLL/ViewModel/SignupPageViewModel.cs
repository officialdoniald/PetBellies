using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetBellies.BLL.ViewModel
{
    public class SignupPageViewModel
    {
        public async Task<string> UploadFileAsync(string pathf, Stream f)
        {
            string uniqueBlobName = await GlobalVariables.blobStorage.UploadFileAsync(pathf, f);

            uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

            return uniqueBlobName;
        }

        public async Task<string> SignUpAsync(User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.FirstName) ||
                String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Password))
            {
                return English.YouHaveToFillAllEntries();
            }
            if (user.Password.Length < 6 && user.Password.Length > 16)
            {
                return English.BadPasswordLength();
            }

            user.Email = user.Email.ToLower();

            var isItAUser = GlobalVariables.databaseConnection.GetUserByEmail(user.Email);

            if (isItAUser.Email is null)
            {
                var success = GlobalVariables.databaseConnection.InsertUser(user);

                if (success)
                {
                    string url = String.Format("http://petbellies.com/php/petbelliesreg.php?email={0}&nev={1}", user.Email, user.FirstName);
                    Uri uri = new Uri(url);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    WebResponse res = await request.GetResponseAsync();

                    return English.Empty();
                }
            }
            else
            {
                return English.ThisEmailIsExist();
            }

            return English.SomethingWentWrong();
        }
    }
}
