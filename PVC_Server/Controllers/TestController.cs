using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PVC_Server.Application.Options;

namespace PVC_Server.Controllers {

	[ApiController]
	[Route("/api/test")]
	public class TestController : ControllerBase {
		
		private readonly JwtOption _jwtOption;
		private readonly EmailOptions _emailOption;

		public TestController(IOptions<JwtOption> jwtOption, IOptions<EmailOptions> emailOption) {
			_jwtOption = jwtOption.Value;
			_emailOption = emailOption.Value;
		}

		[HttpGet("jwt")]
		public IActionResult GetJwt () {
			return Ok(_jwtOption);
		}

		[HttpGet("email")]
		public IActionResult GetEmail() {
			return Ok(_emailOption);
		}
	}
}
