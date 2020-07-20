using Repository.Data;
using Repository.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Repository.Services
{
    public interface ISendEmail
    {
        //sign up - verificarion email
        void VerificationEmail(string email, string link, string emailActivationCode, string userFullname);

        //reset password
        void ResetPassword(Account account);
    }

    public class SendEmail : ISendEmail
    {
        private readonly MessengerDbContext _context;
        public SendEmail(MessengerDbContext context)
        {
            _context = context;
        }

        //sign up - verificarion email
        public void VerificationEmail(string email, string link, string emailActivationCode, string userFullname)
        {
            var fromEmail = new MailAddress("parvinkhp@code.edu.az", "Messenger App");
            var fromEmailPassword = "Pervin_1997";
            var toEmail = new MailAddress(email);
            var appeal = "Dear, " + userFullname + "! ";
            var subject = "Messenger Account Successfully Created! ";

            var messageBody = "</br> " +
                "<div style=' background-color: #665dfe; padding: 20px 0px;'> " +
                "<h2 style='padding: 10px 30px; font-size: 29px; color: #fff;'>" + appeal +
                "Thank you for creating your new Messanger App account! Please, Click the below button to Verify Your Account </h2>" +
                "<center><a style='display: inline-block; background-color: #28a745; font-weight: bold; color: #fff; padding: 10px; " +
                "text-align: center; text-decoration: none; border: 1px solid transparent; font-size: 22px; border-radius: 5px; line-height: 1.5;' " +
                "href=" + link + ">Verify Account</a></center>" +
                "</br><h3 style='margin: 0px; padding: 10px 30px; font-size: 15px; color: #fff;'>" + DateTime.Now.Year +
                " Messenger Application © by Elchin Ganbarov & Pervin Pashazade </h3>";

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

        //reset password
        public void ResetPassword(Account account)
        {
            account.ResetPasswordCode = new Random().Next(1, 999999).ToString("D6");
            account.ForgetToken = Guid.NewGuid().ToString();
            _context.SaveChanges();

            var userFullname = account.Name + " " + account.Surname;
            var fromEmail = new MailAddress("parvinkhp@code.edu.az", "Messenger App");
            var fromEmailPassword = "Pervin_1997";
            var toEmail = new MailAddress(account.Email);
            var appeal = "Dear, " + userFullname + "! ";
            var subject = "Messenger Account Reset Password! ";

            var messageBody = "</br> " +
                "<div style=' background-color: #665dfe; padding: 20px 0px;'> " +
                "<h2 style='padding: 10px 30px; font-size: 22px; color: #fff;'>" + appeal +
                "Your request for a password reset has been accepted. " +
                "Use the <strong>Password Reset Code</strong> below to complete the operation and set a new password. </h2>" +
                "<p style='padding: 10px 30px; font-size: 17px; color: #fff; background-color: brown;'>" +
                "If this request was not made by you, " +
                "<a style='color: #fff!important; font-weght: bold;' href='#'>ignore this operation</a>" +
                ". </p>" +
                "<center><p style='display: inline-block; background-color: #28a745; font-weight: bold; color: #fff; padding: 10px; " +
                "text-align: center; text-decoration: none; border: 1px solid transparent; font-size: 22px; border-radius: 5px; " +
                "line-height: 1.5;' >Code: " + account.ResetPasswordCode + "</p></center>" +
                "</br><h3 style='margin: 0px; padding: 10px 30px; font-size: 15px; color: #fff;'>" + DateTime.Now.Year +
                " Messenger Application © by Elchin Ganbarov & Pervin Pashazade </h3>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true
            };
            smtp.Send(message);
        }
    }
}
