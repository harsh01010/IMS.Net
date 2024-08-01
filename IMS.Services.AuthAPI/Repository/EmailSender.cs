using IMS.Services.AuthAPI.Models.Domain;
using IMS.Services.AuthAPI.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IMS.Services.AuthAPI.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EmailSendAsync(string email, string subject, string message)
        {
            bool status = false;
            try
            {
                var emailSettings = new GetEmailSetting()
                {
                    Secretkey = _configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = _configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = _configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL")
                };

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(emailSettings.From);
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    mailMessage.To.Add(email);

                    using (SmtpClient smtpClient = new SmtpClient(emailSettings.SmtpServer))
                    {
                        smtpClient.Port = emailSettings.Port;
                        smtpClient.Credentials = new NetworkCredential(emailSettings.From, emailSettings.Secretkey);
                        smtpClient.EnableSsl = emailSettings.EnableSSL;

                        await smtpClient.SendMailAsync(mailMessage);
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                status = false;
            }

            return status;
        }
    }
}
