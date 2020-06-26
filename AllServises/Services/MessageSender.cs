using AllInterfaces.Interface;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AllServises.Services {
    public class MessageSender : IMessageSender {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool IsMessageHtml = false) {
            using (var client = new SmtpClient()) {
                var credentials = new NetworkCredential() {
                    UserName = "Safeb021",
                    Password = "(BLOOD)_(0)-(0)_"
                };
                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                using var emailMessage = new MailMessage() {
                    To = {new MailAddress(toEmail)},
                    From = new MailAddress("Safeb021@gmail.com"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = IsMessageHtml
                };
                client.Send(emailMessage);
            }
            return Task.CompletedTask;
        }
    }
}
//http://localhost:1394