using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using Model;

namespace Controller
{
    class Controller_Mail
    {
        private static Random random = new Random();
        private MailMessage email;

        //TODO: Refactor
        public void SendMail(Model_Mail Mail)
        {
            //Instanciate email
            email = new MailMessage();
            //Set sender
            email.From = new MailAddress(Mail.SenderEmail);
            //Set receiver
            email.To.Add(new MailAddress(Mail.RecipientEmail));
            //Set subjec
            email.Subject = Mail.Subject;
            //Set email messagew
            email.Body = Mail.Message;
            //Set priority
            email.Priority = MailPriority.High;

            SmtpClient emailSender = new SmtpClient("mail.software-essentials.nl");

            // Set mail credentials
            emailSender.Credentials = new NetworkCredential("username", "password");
            // Send the email
            emailSender.Send(email);
        }

        public void SendValidationMail(string adres)
        {
            string code = CreateCode();

            Model_Mail mail = new Model_Mail(adres, "dylanroubos@software-essentials.nl", "Mail verification", $"To verify your mail, insert code: {code}, \n\n kind regards, \n\n Windify ");
            SendMail(mail);
        }

        public string CreateCode()
        {
            //Linq statement to create random string based on the given chars and amount
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}


