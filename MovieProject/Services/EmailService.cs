using Azure;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MovieProject.Configuration;
using MovieProject.Interfaces;
using MovieProject.Models;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Net;
using System.Net.Mail;
using System.Runtime;

namespace MovieProject.Services
{
    public class EmailService : IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task<bool> SendAsync(EmailData emailData, CancellationToken ct = default)
        {
            try
            {
     
                using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient())
                {
                    var basicCredential = new NetworkCredential("username", "password");
                    using (MailMessage message = new MailMessage())
                    {
                        MailAddress fromAddress = new MailAddress(emailData.From);

                        smtpClient.Host = "mail.mydomain.com";
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;

                        message.From = fromAddress;
                        message.Subject =emailData.Subject;
                        message.IsBodyHtml = true;
                        message.Body = emailData.Body;
                        message.To.Add(emailData.To);

                        try
                        {
                            smtpClient.Send(message);
                        }
                        catch (Exception ex)
                        {
                            //Error, could not send the message
                            return false;
                        }
                    }
                }

                    return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

