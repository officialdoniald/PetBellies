using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetBellies.BLL.ViewModel
{
    public class ForgotPasswordPageViewModel
    {
        private Segédfüggvények segédfüggvények =
            new Segédfüggvények();

        public async Task<string> SendEmailAsync(string EMAIL)
        {
            if (String.IsNullOrEmpty(EMAIL))
            {
                return English.GiveYourEmail();
            }

            EMAIL = EMAIL.ToLower();

            User user = GlobalVariables.databaseConnection.GetUserByEmail(EMAIL);

            if (user.Email is null)
            {
                return English.NoAcoountFoundWithThisEmail();
            }

            string passwordWithoutEncrypt = segédfüggvények.RandomString(8, false);

            user.Password = segédfüggvények.EncryptPassword(passwordWithoutEncrypt);

            bool success = GlobalVariables.databaseConnection.ForgotPasswordAsync(user.id, user);

            if (!success)
            {
                return English.SomethingWentWrong();
            }

            return string.Empty;
        }
    }
}
