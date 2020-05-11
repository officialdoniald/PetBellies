using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Net.Http;
using Newtonsoft.Json;

namespace PetBellies.BLL.Helper
{
    public class Segédfüggvények
    {
        public string RequestJson(string uri)
        {
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(GlobalVariables.WebApiURL + uri);
                request.Method = HttpMethod.Get;//Get Put Post Delete
                request.Headers.Add("Accept", "aaplication/json");//we would like JSON as response
                var client = new HttpClient();
                HttpResponseMessage response = client.SendAsync(request).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK) { }

                HttpContent content = response.Content;
                var json = content.ReadAsStringAsync().Result;

                return json;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public HttpResponseMessage PostPut(HttpMethod method, object sendingObject, string uri)
        {
            try
            {
                string json = JsonConvert.SerializeObject(sendingObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(GlobalVariables.WebApiURL + uri);
                request.Method = method;
                request.Content = content;

                var client = new HttpClient();
                return client.SendAsync(request).Result;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NoContent };
            }
        }

        public HttpResponseMessage Delete(string url, object sendingObject = null)
        {
            try
            {
                string json = JsonConvert.SerializeObject(sendingObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(GlobalVariables.WebApiURL + url);
                request.Method = HttpMethod.Delete;

                if (sendingObject != null)
                {
                    request.Content = content;
                }

                var client = new HttpClient();
                return client.SendAsync(request).Result;
            }
            catch (Exception)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.NoContent };
            }
        }

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

        public byte[] ReadFully(System.IO.Stream input)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    input.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                try
                {
                    using (FileStream SourceStream = File.Open(GlobalVariables.SourceSelectedImageFromGallery, FileMode.Open))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        SourceStream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            //byte[] buffer = new byte[16 * 1024];
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    int read;
            //    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, 0, read);
            //    }
            //    return ms.ToArray();
            //}
        }

        public string EncryptPassword(string password)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8Encoding = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8Encoding.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

        public int HowOld(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;

            return age;
        }
    }
}