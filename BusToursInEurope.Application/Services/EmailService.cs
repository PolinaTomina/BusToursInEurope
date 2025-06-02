using BusToursInEurope.Application.Configurations;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Core.Entites;
using MailKit.Net.Smtp;
using MailKit.Security;
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

        public async Task SendBulkEmailAsync(List<string> emails, string subject, Tour tour)
        {
            string message = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        .container {{ max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; }}
                        h2 {{ color: #2E86C1; text-align: center; }}
                        .details {{ background: #f9f9f9; padding: 15px; border-radius: 8px; }}
                        .button {{ display: block; width: 200px; margin: 20px auto; padding: 10px; background: #2E86C1; color: white; 
                                   text-align: center; text-decoration: none; font-weight: bold; border-radius: 5px; }}
                        .footer {{ font-size: 12px; color: #555; text-align: center; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>🌍 Новый тур: {tour.Name} 🚍</h2>
                        <div class='details'>
                            <p><strong>Цена:</strong> {tour.Price} руб.</p>
                            <p><strong>Даты:</strong> {tour.StartDate:dd MMMM yyyy} - {tour.EndDate:dd MMMM yyyy}</p>
                            <p><strong>Описание:</strong> {tour.Description}</p>
                        </div>
                        <a href='https://bustoursineurope.com/tour/{tour.Id}' class='button'>Подробнее</a>
                        <p class='footer'>Это автоматическое уведомление. Спасибо, что путешествуете с нами! 🚀</p>
                    </div>
                </body>
                </html>";

            foreach (var email in emails)
            {
                try
                {
                    await SendEmailAsync(email, subject, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки письма на {email}: {ex.Message}");
                }
            }
        }
    }
}