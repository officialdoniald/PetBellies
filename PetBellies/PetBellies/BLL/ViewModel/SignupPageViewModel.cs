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
        public async Task<string> SignUpAsync(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Password))
            {
                return English.YouHaveToFillAllEntries();
            }
            if (user.Password.Length < 6 || user.Password.Length > 16)
            {
                return English.BadPasswordLength();
            }

            user.Email = user.Email.ToLower();

            var isItAUser = GlobalVariables.databaseConnection.GetUserByEmail(user.Email);

            if (isItAUser is null)
            {
                GlobalVariables.InitializeGlobalCasualImage();

                user.ProfilePicture = GlobalVariables.GlobalCasualImage;
                user.Password = new Segédfüggvények().EncryptPassword(user.Password);

                var success = GlobalVariables.databaseConnection.InsertUserAsync(user);

                if (success)
                {
                    try
                    {
                        string url = string.Format("http://petbellies.com/php/petbelliesreg.php?email={0}&nev={1}", user.Email, user.FirstName);
                        Uri uri = new Uri(url);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        WebResponse res = await request.GetResponseAsync();
                    }
                    catch (Exception) { }

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