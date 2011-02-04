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
        public static void SendEmail(string subject, string bodyMessage, List<User> Users)
        {
            SmtpClient client = new SmtpClient("127.0.0.1");
            MailAddress fromAddress = new MailAddress("Admin@lu.com");
            MailMessage message = new MailMessage();
            message.From = fromAddress;
           
            foreach (User user in Users)
            {
                 message.To.Add(user.Email);
            }
           
            message.Body = bodyMessage;
            message.Subject = subject;            
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}
