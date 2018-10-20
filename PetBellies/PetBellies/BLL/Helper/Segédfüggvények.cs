﻿using System;
using System.IO;
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
                    using (FileStream SourceStream = File.Open(GlobalVariables.sstream, FileMode.Open))
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
    }
}
