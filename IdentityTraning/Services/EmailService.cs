using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace IdentityTraning.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridAsync(message);
        }

        private async Task configSendGridAsync(IdentityMessage identityMessage)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(identityMessage.Destination);
            mailMessage.From = new MailAddress("003ovavos@gmail.com", "Basalai Timofei");
            mailMessage.Subject = identityMessage.Subject;
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(identityMessage.Body));

            NetworkCredential networkCredential = new NetworkCredential(
                ConfigurationManager.AppSettings["Plagin_Pain"],
                ConfigurationManager.AppSettings["29ovavosWitcher3"]);

            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587))
            {
                Credentials = networkCredential
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}