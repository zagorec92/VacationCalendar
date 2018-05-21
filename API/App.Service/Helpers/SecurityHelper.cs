using App.Service.DTO;
using App.Service.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace App.Service.Helpers
{
	public static class SecurityHelper
	{
		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userDto"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static string CreateToken(UserDto userDto, IConfiguration configuration)
		{
			string configKey = configuration.GetData<string>("Security", "Key");
			string configIssuer = configuration.GetData<string>("Security", "Issuer");
			int tokenDuration = configuration.GetData<int>("Security", "TokenDuration");

			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
			SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			IEnumerable<Claim> roleClaims = userDto.Roles.Select(x =>
				new Claim(ClaimTypes.Role, x.Name, ClaimValueTypes.String, configIssuer)
			);

			List<Claim> claims = new List<Claim>(new[] {
				new Claim(JwtRegisteredClaimNames.Sub, userDto.FullName),
				new Claim(JwtRegisteredClaimNames.Email, userDto.Email),
				new Claim(JwtRegisteredClaimNames.Jti, userDto.ID.ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
			});

			claims.AddRange(roleClaims);

			SecurityToken token = new JwtSecurityToken(
				configIssuer,
				configIssuer,
				claims,
				expires: DateTime.Now.AddMinutes(tokenDuration),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// Hashes data.
		/// </summary>
		/// <param name="data">The <see cref="String"/> instance.</param>
		/// <returns>Hashed product.</returns>
		public static string Hash(string data)
		{
			byte[] hashed;

			using (SHA512Managed algorithm = new SHA512Managed())
			{
				hashed = algorithm.ComputeHash(new UTF8Encoding().GetBytes(data));
			}

			return Convert.ToBase64String(hashed);
		}

		#endregion
	}
}
