﻿using System;
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
        public void SendMail(Model_Mail mail)
        {
            //Instanciate email
            email = new MailMessage();
            //Set sender
            email.From = new MailAddress("Windify80@gmail.com");
            //Set receiver
            email.To.Add(new MailAddress(mail.RecipientEmail));
            //Set subject
            email.Subject = mail.Subject;
            //Set email messagew
            email.Body = mail.Message;
            //Set priority
            email.Priority = MailPriority.High;

            SmtpClient emailSender = new SmtpClient("smtp.gmail.com", 587);

            // Set mail credentials
            emailSender.EnableSsl = true;
            emailSender.UseDefaultCredentials = false;
            emailSender.Credentials = new NetworkCredential("windify80@gmail.com", "Windesheim1");
            // Send the email
            emailSender.Send(email);
        }

        public void SendValidationMail(string adres)
        {
            string code = CreateCode();

            Model_Mail mail = new Model_Mail(adres, "Windify80@gmail.com", "Mail verification", $"To verify your mail, insert code: {code}, \n\n kind regards, \n\n Windify ");
            SendMail(mail);
        }

        public string CreateCode()
        {
            //Linq statement to create random string based on the given chars and amount
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            Console.WriteLine(code);
            return code;
        }
    }
}


