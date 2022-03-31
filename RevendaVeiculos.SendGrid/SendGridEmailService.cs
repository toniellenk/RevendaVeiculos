using RevendaVeiculos.SendGrid.Abstractions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace RevendaVeiculos.SendGrid
{
    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly EmailOptions _emailOptions;


        public SendGridEmailService(ISendGridClient sendGridClient, EmailOptions emailOptions)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
            _emailOptions = emailOptions ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        public async Task SendAsync(
            string toEmail,
            string toName,
            string subject,
            string plainTextContent,
            string htmlContent,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(toEmail))
            {
                throw new ArgumentNullException(nameof(toEmail));
            }

            if (string.IsNullOrEmpty(toName))
            {
                throw new ArgumentNullException(nameof(toName));
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrEmpty(plainTextContent))
            {
                throw new ArgumentNullException(nameof(plainTextContent));
            }

            if (string.IsNullOrEmpty(htmlContent))
            {
                throw new ArgumentNullException(nameof(htmlContent));
            }

            var from = new EmailAddress(_emailOptions.FromEmail, _emailOptions.FromName);
            var to = new EmailAddress(toEmail, toName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                var exception = new EmailException("Fail to send email");
                exception.Data[nameof(response.StatusCode)] = response.StatusCode;
                exception.Data[nameof(response.Body)] = responseBody;
                throw exception;
            }
        }
    }
}