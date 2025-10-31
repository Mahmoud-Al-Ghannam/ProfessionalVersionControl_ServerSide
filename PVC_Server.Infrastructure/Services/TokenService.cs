using Microsoft.IdentityModel.Tokens;
using PVC_Server.Application.Interfaces;
using PVC_Server.Application.Options;
using PVC_Server.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PVC_Server.Infrastructure.Services {
	public class TokenService : ITokenService {

		private readonly JwtOption _jwtOption;

		public TokenService(JwtOption jwtOption) {
			_jwtOption = jwtOption;
		}

		public string GenerateJwtToken(User user, IEnumerable<Role> roles) {
			List<Claim> claims =
			[
				new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.Email,user.Email),
			];

			claims.AddRange(roles.Select(
				r => new Claim(ClaimTypes.Role, r.Name)
			));

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor() {
				Audience = _jwtOption.Audience,
				Issuer = _jwtOption.Issuer,
				Expires = DateTime.UtcNow.AddMinutes(_jwtOption.LifeTimeMin),

				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key)),
					SecurityAlgorithms.HmacSha256
				),
				Subject = new ClaimsIdentity(claims)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			string accessToken = tokenHandler.WriteToken(token);
			return accessToken;
		}
	}
}
