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
        void SignUpUser(AspNetUser user);

        void DeactivateUser(string userId);

        void ReactivateUser(string userId);

        string TestMe(string input);
    }
}
