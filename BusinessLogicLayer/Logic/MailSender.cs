using BusinessLogicLayer;
using DataAccessLayer.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;

namespace DataAccessLayer.Containers
{
    public class MailSender : ISender
    {
        private readonly IConfiguration Configuration;

        public MailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendAccountCreationMail(string recipient)
        {
            try
            {
                MailMessage message = new MailMessage("primedsoon12@gmail.com", recipient);
                message.Subject = "Registratie bij TimeMate";
                message.IsBodyHtml = true;
                message.Body = "Uw mailadres is gebruikt om een account te maken bij TimeMate.";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Configuration["Mailserver:Username"], Configuration["Mailserver:Password"]);
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
