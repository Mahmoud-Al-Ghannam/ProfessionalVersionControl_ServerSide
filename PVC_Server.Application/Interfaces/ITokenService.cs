using PVC_Server.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVC_Server.Application.Interfaces {
	public interface ITokenService {
		public string GenerateJwtToken(User user, IEnumerable<Role> roles);
	}
}
