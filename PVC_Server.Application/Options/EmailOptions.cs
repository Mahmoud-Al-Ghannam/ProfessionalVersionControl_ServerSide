using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVC_Server.Application.Options {
	public class EmailOptions {

		[Required]
		public string From { get; set; } = string.Empty;
		[Required]
		public string SmtpServer { get; set; } = string.Empty;
		[Required]
		public int Port { get; set; }
		[Required]
		public string Username { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public bool EnableSsl { get; set; } = true;
		public string DisplayName { get; set; } = string.Empty;
		public bool UseDefaultCredentials { get; set; } = false;
	}
}
