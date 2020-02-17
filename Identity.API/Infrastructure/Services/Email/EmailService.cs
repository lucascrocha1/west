namespace Identity.API.Infrastructure.Services.Email
{
    using Identity.API.Model;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.IO;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string email, string subject, string message)
        {
            var sendgridSettings = await GetSendgridSettings();

            var client = new SendGridClient(sendgridSettings.SendgridKey);

            var from = new EmailAddress(sendgridSettings.SendgridEmail, "WEST");

            var to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            await client.SendEmailAsync(msg);
        }

        private async Task<SendGridDto> GetSendgridSettings()
        {
            var json = await File.ReadAllTextAsync(_configuration["SendgridConfigurationPath"]);

            return JsonConvert.DeserializeObject<SendGridDto>(json);
        }
    }
}