using Microsoft.Extensions.Configuration;
using MimeKit;
using SocialMedia.Domain.Entities;
 
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
namespace SocialMedia.Application.Common
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string subject, string body);
    }
    public class EmailService : IEmailService

  
    { 
        private readonly IConfiguration _configuration;
        private readonly SmtpSettings _smtpSettings;
        public EmailService(IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
        {
            _configuration = configuration;
            _smtpSettings = smtpSettings.Value;


        }
        public async Task SendEmailAsync(string ToEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App Name", _smtpSettings.UserName));
            message.To.Add(new MailboxAddress("", ToEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
                await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                await client.SendAsync(message);
            }catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }


    }

}
