using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace PetBellies.BLL.Helper
{
    public class Mailer
    {
        public string SendEmail(string EMAIL, string NEWPW)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(GlobalVariables.SMTPUser.SMTPCLientHost);

                mail.From = new MailAddress(GlobalVariables.SMTPUser.SMTPEmail);
                mail.To.Add(EMAIL);

                if (string.IsNullOrEmpty(NEWPW))
                {
                    mail.Subject = "Registration";
                    mail.Body = "Welcome to " + English.AppName() + "!";
                }
                else
                {
                    mail.Subject = "Forgot password";
                    mail.Body = "Your new PW:" + NEWPW;
                }

                SmtpServer.Port = GlobalVariables.SMTPUser.SMTPServerPort;
                SmtpServer.Credentials = new System.Net.NetworkCredential(GlobalVariables.SMTPUser.SMTPUsername
                                                                          , GlobalVariables.SMTPUser.SMTPPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}