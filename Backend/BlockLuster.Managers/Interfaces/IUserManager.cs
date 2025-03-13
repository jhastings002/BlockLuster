using BlockLuster.Common.Shared.ResponsesAndRequests;
using BlockLuster.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<SigninResponse> SignUpUserAsync(string firstName, string lastName, string email, string password);

        void UpdateProfile(string userId, string firstName, string lastName);

        Task UpdatePassword(string userId, string oldPassword, string newPassword);

        void DeactivateUser(string userId);

        void ReactivateUser(string userId);

        string TestMe(string input);
    }
}
