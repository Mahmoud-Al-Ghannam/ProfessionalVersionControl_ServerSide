using MimeKit;
using MailKit.Net.Smtp;
using PVC_Server.Application.Interfaces;
using PVC_Server.Application.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MailKit.Security;

namespace PVC_Server.Infrastructure.Services {
	public class EmailService : IEmailService {

		private readonly EmailOptions _emailOptions;

		public EmailService(IOptions<EmailOptions> emailOptions) {
			_emailOptions = emailOptions.Value;
		}

		public async Task SendAsync(string toEmail, string subject, string body, bool isHtml) {
			MimeMessage email = new MimeMessage();
			email.From.Add(new MailboxAddress(_emailOptions.DisplayName, _emailOptions.From));
			email.To.Add(MailboxAddress.Parse(toEmail));
			email.Subject = subject;
			BodyBuilder bodyBuilder = new BodyBuilder();
			if (isHtml) {
				bodyBuilder.HtmlBody = body;
			} else {
				bodyBuilder.TextBody = body;
			}
			email.Body = bodyBuilder.ToMessageBody();

			using SmtpClient smtp = new SmtpClient();
			await smtp.ConnectAsync(_emailOptions.SmtpServer,_emailOptions.Port,_emailOptions.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
			if (!_emailOptions.UseDefaultCredentials) {
				await smtp.AuthenticateAsync(_emailOptions.Username, _emailOptions.Password);
			}

			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}

		public async Task SendAsync(IEnumerable<string> toEmails, string subject, string body, bool isHtml) {
			foreach (var toEmail in toEmails) {
				await SendAsync(toEmail, subject, body, isHtml);
			}
		}
	}
}
