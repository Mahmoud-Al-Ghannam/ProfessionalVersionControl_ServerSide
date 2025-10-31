using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVC_Server.Application.Options {
	public class JwtOption {

		[Required]
		public string Issuer { get; set; } = string.Empty;

		[Required]
		public string Audience { get; set; } = string.Empty;

		[Required]
		public int LifeTimeMin { get; set; }

		[Required]
		public string Key { get; set; } = string.Empty;

	}
}
