namespace RevendaVeiculos.SendGrid.Abstractions
{
    public interface IEmailService
    {
        Task SendAsync(
            string toEmail,
            string toName,
            string subject,
            string plainTextContent,
            string htmlContent,
            CancellationToken cancellationToken = default);
    }
}
