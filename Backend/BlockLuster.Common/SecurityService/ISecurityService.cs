using BlockLuster.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Common.SecurityService
{
    public interface ISecurityService
    {
        Task<string> SignUpAsync(AspNetUser user, string password);
        Task<string> SignInAsync(string email, string password);
    }
}
