using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Linq;
using Model;

namespace Controller
{
    public class Mail
    {
        private MailMessage _email;

        /// <summary>
        /// sends email to the given mailadress
        /// </summary>
        /// <param name="mail"></param>
        public void SendMail(Model.Mail mail)
        {
            //Instanciate email
            _email = new MailMessage();
            //Set sender
            _email.From = new MailAddress("Windify80@gmail.com");
            //Set receiver
            _email.To.Add(new MailAddress(mail.RecipientEmail));
            //Set subject
            _email.Subject = mail.Subject;
            //Set email messagew
            _email.Body = mail.Message;
            //Set priority
            _email.Priority = MailPriority.High;

            SmtpClient emailSender = new SmtpClient("smtp.gmail.com", 587);

            // Set mail credentials
            emailSender.EnableSsl = true;
            emailSender.UseDefaultCredentials = false;
            emailSender.Credentials = new NetworkCredential("windify80@gmail.com", Passwords.GetPassword("Gmail"));
            // Send the email
            emailSender.Send(_email);
        }

        /// <summary>
        /// sends an email to the user with verification code
        /// </summary>
        /// <param name="adres"></param>
        /// <param name="code"></param>
        public void SendValidationMail(string adres, string code)
        {
            Model.Mail mail = new Model.Mail(adres, "Windify80@gmail.com", "Mail verification", $"To verify your mail, insert code: {code}, \n\n kind regards, \n\n Windify ");
            SendMail(mail);
        }
    }
}


