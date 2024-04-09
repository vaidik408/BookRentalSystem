using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BRS.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string? _senderName;
        private readonly string _senderEmail;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["MailSettings:Server"];
            _port = configuration.GetValue<int>("MailSettings:Port");
            _username = configuration["MailSettings:UserName"];
            _password = configuration["MailSettings:Password"];
            _senderName = configuration["MailSettings:SenderName"];
            _senderEmail = configuration["MailSettings:SenderEmail"];
        }

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_senderName, _senderEmail));
                message.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
                message.Subject = subject;

                var builder = new BodyBuilder();
                builder.TextBody = body;

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_username, _password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
