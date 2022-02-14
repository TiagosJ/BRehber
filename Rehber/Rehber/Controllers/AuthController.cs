using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rehber.Data;
using Rehber.Dtos;
using Rehber.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rehber.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthRepository  _authRepository;
        private IConfiguration _configuration;//tokenlar için

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            if (await _authRepository.UserExists(userForRegisterDto.UserName))//kullanıcı daha önce kaydolmuş mu ?
            {
                ModelState.AddModelError("UserName", "Username already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                firstName = userForRegisterDto.firstName,
                surName =userForRegisterDto.surName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);//kullanıcı oluştu
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserForLoginDto userForLoginDto)//giriş bilgileri isteniyor
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);//veri tabanında böyle bir kullanıcı var mı?

            if(user == null)
            {
                return Unauthorized();//kullanıcı yok
            }

            var tokenHandler = new JwtSecurityTokenHandler();//kullanıcı var kullanıcıya token gönder.
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);//gizli anahtar ile token oluşturuyoruz
            var tokenDescriptor = new SecurityTokenDescriptor//token'ın tutacağı şeyler
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),//tokenin geçerlilik süresi
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),//kullanılan algoritma
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);//token oluşturuluyor
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }

        [HttpGet]
        [Route("getUser")]
        public ActionResult GetUserById(int userId)
        {
            var user = _authRepository.GetUserById(userId);
            return Ok(user);
        }
    }
}
