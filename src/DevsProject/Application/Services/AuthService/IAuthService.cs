using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
//AccessToken : Token oluşturan metot
//RefreshToken : Refresh Token oluşturan metot
//AddRefreshToken : Refresh Token ı veritabanına ekleyen metot. Doğrulama karşılaştırma yapmak için
