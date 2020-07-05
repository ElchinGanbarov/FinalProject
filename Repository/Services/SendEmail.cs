using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Repository.Services
{
    public interface ISendEmail
    {
        void VerificationEmail(string email, string link, string emailActivationCode, string userFullname);
    }

    public class SendEmail : ISendEmail
    {
        public void VerificationEmail(string email, string link, string emailActivationCode, string userFullname)
        {
            var fromEmail = new MailAddress("parvinkhp@code.edu.az", "Messenger App");
            var fromEmailPassword = "Pervin_1997";
            var toEmail = new MailAddress(email);
            var appeal = "Dear, " + userFullname + "! ";
            var subject = "Messenger Account Successfully Created";

            var messageBody = "</br> " +
                "<div style=' background-color: #665dfe; padding: 20px 0px;'> " +
                "<h2 style='padding: 10px 30px; font-size: 29px; color: #fff;'>" + appeal +
                "Thank you for creating your new Messanger App account! Please, Click the below button to Verify Your Account </h2>" +
                "<center><a style='display: inline-block; background-color: #28a745; font-weight: bold; color: #fff; padding: 10px; " +
                "text-align: center; text-decoration: none; border: 1px solid transparent; font-size: 22px; border-radius: 5px; line-height: 1.5;' " +
                "href=" + link + ">Verify Account</a></center>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            /*using*/ var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true
            };
            smtp.Send(message);
        }
    }
}
