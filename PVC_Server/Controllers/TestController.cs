using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PVC_Server.Application.Interfaces;
using PVC_Server.Application.Options;

namespace PVC_Server.Controllers {

	public class SendEmailDTO {
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public bool IsHtml { get; set; }
	}

	[ApiController]
	[Route("/api/test")]
	public class TestController : ControllerBase {

		private readonly IEmailService _emailService;

		public TestController(IEmailService emailService) {
			_emailService = emailService;
		}

		[HttpPost("send-mail")]
		public async Task<IActionResult> SendEmail(SendEmailDTO dto) {
			await _emailService.SendAsync(dto.To, dto.Subject, dto.Body, dto.IsHtml);
			return Ok();
		}
	}
}
