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
        Task<string> SignUpUserAsync(string firstName, string lastName, string email, string password);

        void DeactivateUser(string userId);

        void ReactivateUser(string userId);

        string TestMe(string input);
    }
}
