using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVC_Server.Application.Interfaces {
	public interface IEmailService {
		public Task SendAsync (string toEmail,string subject,string body,bool isHtml);
		public Task SendAsync (IEnumerable<string> toEmails,string subject,string body,bool isHtml);
	}
}
