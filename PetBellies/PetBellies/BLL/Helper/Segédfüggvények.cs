using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PetBellies.BLL.Helper
{
    public class Segédfüggvények
    {
        public bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(emailaddress);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        //public async void SendEmail(string url, string email, string firstname, string lastname, string eventname, string from, string to, string town, string place)
        //{
        //    string urls = "";

        //    if (url == "eventcreate" || url == "joinedevent" || url == "sendmailtosubscribeduser")
        //    {
        //        string timef = "";
        //        string timet = "";
        //        string finallytown = "";

        //        DateTimeOffset dto_begin = new DateTimeOffset();
        //        DateTimeOffset.TryParse(from, out dto_begin);
        //        dto_begin = dto_begin.ToLocalTime();

        //        timef = dto_begin.ToString("dddd, MMM dd yyyy HH:mm");

        //        if (from == to)
        //        {
        //            timet = "Dosn't matter";
        //        }
        //        else
        //        {
        //            DateTimeOffset dto_end = new DateTimeOffset();
        //            DateTimeOffset.TryParse(to, out dto_end);
        //            dto_end = dto_end.ToLocalTime();
        //            timet = dto_end.ToString("dddd, MMM dd yyyy HH:mm");
        //        }

        //        if (String.IsNullOrEmpty(town) || String.IsNullOrEmpty(place))
        //            finallytown = "Online";
        //        else finallytown = town + ", " + place;

        //        urls = String.Format("http://invme.eu/invmeapp/{0}.php?emaill={1}&nev={2}&eventname={3}&town={4}&from={5}&to={6}", url, email, firstname + "_" + lastname, eventname, finallytown, timef, timet);
        //    }
        //    else
        //    {
        //        urls = String.Format("http://invme.eu/invmeapp/{0}.php?emaill={1}&nev={2}", url, email, firstname + "_" + lastname);
        //    }

        //    Uri uri = new Uri(urls);
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urls);
        //    request.Method = "GET";
        //    WebResponse res = await request.GetResponseAsync();
        //}

        public byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
