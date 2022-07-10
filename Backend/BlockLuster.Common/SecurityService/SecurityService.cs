using BlockLuster.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BlockLuster.Common.SecurityService
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<AspNetUser> _userManager;

        public SecurityService(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);

            if (checkPasswordResult)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.Email, email));
                claims.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
                if (user.IsAdmin)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }

                var signingCreds = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY1NzQyMDc0MywiaWF0IjoxNjU3NDIwNzQzfQ.rjKh6QJYAHpUdxONj0ycuFEgO_Pzq8N2KIav5NCPjsk")), SecurityAlgorithms.HmacSha256Signature);
                var jwtToken = tokenHandler.CreateJwtSecurityToken(null, null, null, DateTime.UtcNow, DateTime.Now.AddDays(1), DateTime.UtcNow, signingCreds);

                return tokenHandler.WriteToken(jwtToken);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public string TestMe(string input)
        {
            string result = $"{input} : {GetType().Name}";
            Console.WriteLine(result);
            return result;
        }
    }
}