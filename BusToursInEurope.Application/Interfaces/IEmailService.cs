using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendBulkEmailAsync(List<string> emails, string subject, Tour tour);
    }
}
