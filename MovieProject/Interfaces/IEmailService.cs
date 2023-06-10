using MovieProject.Models;

namespace MovieProject.Interfaces
{
    public interface IEmailService
    {
      Task<bool> SendAsync(EmailData emailData, CancellationToken ct = default);
    }
}
