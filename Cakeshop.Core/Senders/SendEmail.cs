using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Cakeshop.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("Foroghefarda2020@gmail.com", "فروغ فردا");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("Foroghefarda2020@gmail.com", "13787813");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);

        }
    }
}