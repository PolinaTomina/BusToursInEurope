using BusToursInEurope.Application.Configurations;
using BusToursInEurope.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BusToursInEurope.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> emailOptions)
        {
            _emailConfig = emailOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("BusToursInEurope", _emailConfig.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(email, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync(_emailConfig.SenderEmail, _emailConfig.SenderPassword);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}