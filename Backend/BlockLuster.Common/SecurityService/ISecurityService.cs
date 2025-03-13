using BlockLuster.Common.Shared.ResponsesAndRequests;
using BlockLuster.EntityFramework;

namespace BlockLuster.Common.SecurityService
{
    public interface ISecurityService
    {
        Task<string> SignUpAsync(AspNetUser user, string password);

        Task UpdatePassword(string userId, string oldPassword, string newPassword);
        Task<SigninResponse> SignInAsync(string email, string password);
    }
}
