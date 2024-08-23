using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Models.Dto;
using IMS.API.Models.Domain.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IMS.API.Repository.Implementations.Auth
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> EmailSendAsync(SendEmailRequestDto requestDto)
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
                    mailMessage.Subject = requestDto.Subject;
                    mailMessage.Body = requestDto.Body;
                    mailMessage.To.Add(requestDto.Email);
                    mailMessage.IsBodyHtml = true;

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
