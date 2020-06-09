using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DataAccessLayer.Contexts
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
                smtp.Credentials = new System.Net.NetworkCredential("primedsoon12@gmail.com", "!@Iskf23ND*Eb1t26oM*P0@x8");
                smtp.EnableSsl = true;

                smtp.Send(message);
            }
            catch (Exception exception)
            {
                throw new Exception("Er is op dit moment een probleem met het versturen van de account creatie mail.", exception);
            }
        }
    }
}
