namespace CarRentalApi.GmailService;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using CarRentalApi.Models;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
            throw new ArgumentNullException(nameof(toEmail), "Receiver email cannot be null or empty.");

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Car_Rental_System", _smtpSettings.Username));
        email.To.Add(new MailboxAddress(toEmail, toEmail));
        email.Subject = subject;

        // Send as HTML or plain text
        email.Body = new TextPart(isHtml ? "html" : "plain")
        {
            Text = body
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

}

