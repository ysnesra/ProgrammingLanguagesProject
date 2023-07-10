using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Kullanıcı için kontrolcüler.
    /// </summary>
    /// 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Kullanıcı kayıt işlemini yapar.
        /// </summary>
        /// <param name="userForRegisterDto">Kullanıcı kayıt bilgileri.</param>
        /// <returns>Kullanıcı kayıt işleminin sonucunu döndürür.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            //Register olan datası result objesinde tutulur:
            RegisteredDto result = await Mediator.Send(registerCommand);

            //oluşan RefreshToken ı aynı zamanda cookieye eklenir:
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        /// <summary>
        /// Çerez'e refresh token ekler.
        /// </summary>
        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
