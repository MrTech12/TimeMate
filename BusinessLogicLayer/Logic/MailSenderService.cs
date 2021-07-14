using Core.Errors;
using Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace DataAccessLayer.Containers
{
    public class MailSenderService : ISender
    {
        MailMessage mail;
        SmtpClient smtp;
        private readonly IConfiguration Configuration;

        public MailSenderService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendAccountCreationMessage(string recipient)
        {
            try
            {
                CreateAccountCreationMessage(recipient);
                SetupSMTP();
                smtp.Send(mail);
            }
            catch (Exception exception)
            {
                throw new MailException("Er is op dit moment een probleem met het versturen van de account creatie mail.", exception);
            }
        }

        public void CreateAccountCreationMessage(string recipient)
        {
            mail = new MailMessage(Configuration["Mailserver:Username"], recipient);
            mail.Subject = "Registratie bij TimeMate";
            mail.IsBodyHtml = true;
            mail.Body = "Uw mailadres is gebruikt om een account te maken bij TimeMate.";
        }

        public void SetupSMTP()
        {
            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration["Mailserver:Username"], Configuration["Mailserver:Password"]);
            smtp.EnableSsl = true;
        }
    }
}
