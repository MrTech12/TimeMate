﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    class Mail
    {
        public void SendMail(string mailUser)
        {
            try
            {
                MailMessage message = new MailMessage("sina1240@gmail.com", mailUser);
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}