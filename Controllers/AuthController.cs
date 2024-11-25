using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StreamingAPI.Data;
using StreamingAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StreamingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly StreamingContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(StreamingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            // Valida o usuário no banco de dados
            var usuario = _context.Users.FirstOrDefault(u =>
                u.Email == loginDTO.Email &&
                u.Password == loginDTO.Password); // Para produção, use senhas criptografadas.

            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            // Gera o token JWT
            var token = GerarTokenJWT(usuario);

            return Ok(new { token });
        }

        private string GerarTokenJWT(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

