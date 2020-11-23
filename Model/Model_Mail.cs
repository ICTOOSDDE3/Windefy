using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Model_Mail
    {
        public string RecipientEmail { get; set; }
        public string SenderEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public Model_Mail(string recipientEmail, string senderEmail, string subject, string message)
        {
            RecipientEmail = recipientEmail;
            SenderEmail = senderEmail;
            Subject = subject;
            Message = message;
        }

    }
}


