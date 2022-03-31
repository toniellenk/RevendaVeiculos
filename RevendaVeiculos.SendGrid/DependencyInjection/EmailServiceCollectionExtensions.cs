using System;
using RevendaVeiculos.SendGrid.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

namespace RevendaVeiculos.SendGrid.DependencyInjection
{
    public static class EmailServiceCollectionExtensions
    {
        public static IServiceCollection AddSendGridEmailService(this IServiceCollection services, string apiKey, Action<EmailOptions> configureOptions)
        {
            services.AddSendGrid(options =>
            {
                options.ApiKey = apiKey;
            });

            services.AddOptions<EmailOptions>()
                .Configure(configureOptions)
                .PostConfigure(options =>
                {
                    if (string.IsNullOrWhiteSpace(options.FromEmail))
                    {
                        ThrowInvalidConfigurationException(nameof(options.FromEmail));
                    }

                    if (string.IsNullOrWhiteSpace(options.FromName))
                    {
                        ThrowInvalidConfigurationException(nameof(options.FromName));
                    }
                });
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailOptions>>().Value);
            services.AddSingleton<IEmailService, SendGridEmailService>();
            return services;
        }

        private static void ThrowInvalidConfigurationException(string configurationName)
        {
            var exception = new EmailException("Invalid configuration");
            exception.Data["ConfigurationName"] = configurationName;
            throw exception;
        }
    }
}
