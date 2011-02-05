using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class UtilityFunctions
    {
        public static void SendEmail(string subject, string bodyMessage, List<User> users)
        {           
            foreach (User user in users)
            {
                SendEmail(subject, bodyMessage, user);
            }           
        }

        public static void SendEmail(string subject, string bodyMessage, User user)
        {
            //SmtpClient client = new SmtpClient("lynx.iss.nus.edu.sg");
            SmtpClient client = new SmtpClient("127.0.0.1");
            MailAddress fromAddress = new MailAddress("Admin@lu.com");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(user.Email);
            message.Body = bodyMessage;
            message.Subject = subject;
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}
