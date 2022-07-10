using BlockLuster.Accessors.Interfaces;
using BlockLuster.EntityFramework;
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


        public void SignUpUser(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public void DeactivateUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void ReactivateUser(string userId)
        {
            throw new NotImplementedException();
        }

        public string TestMe(string input)
        {
            return $"{nameof(UserManager)} : {_userAccessor.TestMe(input)}";
        }
    }
}