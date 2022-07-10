using BlockLuster.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Accessors.Interfaces
{
    public interface IUserAccessor : IAccessorBase
    {
        bool SignUp(AspNetUser user);
        bool DeactivateAccount(string userId);
        bool ReactivateAccount(string userId);
    }
}
