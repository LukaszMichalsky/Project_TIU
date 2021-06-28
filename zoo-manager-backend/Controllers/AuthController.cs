using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo_manager_backend.Controllers {
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase {
        public class AuthRequest {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class AuthResponse {
            public string Token { get; set; }
        }

        private Dictionary<string, string> validUsers = new Dictionary<string, string>() { // These data should be retrieved from database.
            { "admin", "admin123" }
        };

        [HttpGet]
        public IActionResult Validate() {
            if (Request.Headers.ContainsKey("Authorization") == true) {
                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];

                try {
                    new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters() {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateTokenReplay = false,

                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zoo-manager-backend"))
                    }, out _);

                    return Ok();
                } catch {
                    return Unauthorized();
                }
            } else {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Login( [FromBody] AuthRequest authRequest ) {
            string username = authRequest.Username;
            string password = authRequest.Password;

            if (validUsers.ContainsKey(username) == true) {
                if (validUsers[username] != password) {
                    return Unauthorized();
                }
            } else {
                return Unauthorized();
            }

            AuthResponse authResponse = new AuthResponse();
            JwtSecurityToken apiToken = new JwtSecurityToken(null, null, null, null, DateTime.Now.AddMinutes(30), new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zoo-manager-backend")),
                SecurityAlgorithms.HmacSha256
            ));

            authResponse.Token = new JwtSecurityTokenHandler().WriteToken(apiToken);
            return Ok(authResponse);
        }
    }
}
