using BlockLuster.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BlockLuster.Common.Shared.ResponsesAndRequests;

namespace BlockLuster.Common.SecurityService
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<AspNetUser> _userManager;

        public SecurityService(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> SignUpAsync(AspNetUser user, string password)
        {
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) { 
                var loaded = await _userManager.FindByEmailAsync(user.Id);
            }
            else
            {
                throw new InvalidOperationException("Unable To Create Email");
            }

            return password;
        }

        public async Task UpdatePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Unable To Update User");
            }
        }

        public async Task<SigninResponse> SignInAsync(string email, string password)
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
                        Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoidGVzdCIsIklzc3VlciI6InRlc3QiLCJVc2VybmFtZSI6InRlc3QiLCJleHAiOjE3NTc0MjA3NDMsImlhdCI6MTY1NzQyMDc0M30.kdH3wDBWGy7WOcjV6au6wuqMC-qSPmwegHNfnJ8iNos")), 
                    SecurityAlgorithms.HmacSha256Signature);

                var jwtToken = tokenHandler.CreateJwtSecurityToken("http://localhost", "BlockLuster", claims, DateTime.UtcNow, DateTime.Now.AddDays(1), DateTime.UtcNow, signingCreds);

                return new SigninResponse
                {
                    Success = true,
                    User = new User()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    },
                    isAdmin = user.IsAdmin,
                    Token = tokenHandler.WriteToken(jwtToken)
                };
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