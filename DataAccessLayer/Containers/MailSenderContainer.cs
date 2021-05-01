using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class MailSenderContainer : ISenderContainer
    {
        public void SendAccountCreationMessage(string mail)
        {
            try
            {
                MailMessage message = new MailMessage("primedsoon12@gmail.com", mail);
                message.Subject = "Registratie bij TimeMate";
                message.IsBodyHtml = true;
                message.Body = "Uw mailadres is gebruikt om een account te maken bij TimeMate.";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("primedsoon12@gmail.com", "415ZejFfAC7u@r4m&FSdnw1pfe");
                smtp.EnableSsl = true;

                smtp.Send(message);
            }
            catch (Exception exception)
            {
                throw new MailException("Er is op dit moment een probleem met het versturen van de account creatie mail.", exception);
            }
        }
    }
}
