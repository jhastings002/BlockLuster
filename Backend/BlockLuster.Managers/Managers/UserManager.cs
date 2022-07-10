using BlockLuster.Accessors.Interfaces;
using BlockLuster.Managers.Interfaces;

namespace BlockLuster.Managers.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccessor _userAccessor;
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public string TestMe(string input)
        {
            return $"{nameof(UserManager)} : {_userAccessor.TestMe(input)}";
        }
    }
}