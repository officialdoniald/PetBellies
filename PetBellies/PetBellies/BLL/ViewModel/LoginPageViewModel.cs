using PetBellies.BLL.Helper;
using System;

namespace PetBellies.BLL.ViewModel
{
    public class LoginPageViewModel
    {
        private Segédfüggvények segédfüggvények =
            new Segédfüggvények();

        //1:üres mind a kettő
        //2:első üres
        //3:második üres
        //4:nem jó az email formátuma
        //5:a pw nem olyan hosszban van 6-16
        //6:nem található ilyen email a DBben vagy rossz a jelszó
        //7:minden jó
        public string Login(string EMAIL, string PASSWORD)
        {
            if (String.IsNullOrEmpty(EMAIL) && String.IsNullOrEmpty(PASSWORD))
            {
                return English.YouHaveToFillAllEntries();
            }
            if (String.IsNullOrEmpty(EMAIL))
            {
                return English.EmailIsEmpty();
            }
            if (String.IsNullOrEmpty(PASSWORD))
            {
                return English.PasswordIsEmpty();
            }
            if (!segédfüggvények.IsValidEmailAddress(EMAIL.ToLower()))
            {
                return English.BadEmailFormat();
            }
            if (PASSWORD.Length < 6 || PASSWORD.Length > 16)
            {
                return English.BadPasswordLength();
            }

            var user = GlobalVariables.databaseConnection.GetUserByEmail(EMAIL.ToLower());

            if (user is null || user.Password != segédfüggvények.EncryptPassword(PASSWORD))
            {
                return English.BadPasswordOrEmail();
            }

            return English.Empty();
        }
    }
}
