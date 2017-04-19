using System;
using System.Net;
using System.Net.Mail;

namespace OnlineLibrary.Infrastructure
{
    public class EmailSender
    {
        const string SMTP = "smtp.mail.ru";
        const string EMAIL = "online.library@mail.ru";
        const string PASSWORD = "CorpArmstrong128";
        const string CAPTION = "Remainder from the OnlineLibrary";
        const string MESSAGE_START = "We remind you that you have borrowed a book: '";
        const string MESSAGE_END = "' in our library. Don't forget to return it.";

        public void SendMail(string mailto, string bookName = "", string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(EMAIL);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = CAPTION;
                mail.Body = MESSAGE_START + bookName + MESSAGE_END;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = SMTP;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(EMAIL.Split('@')[0], PASSWORD);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
            }
        }
    }
}