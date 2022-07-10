using BlockLuster.Accessors.Interfaces;

namespace BlockLuster.Accessors.Accessors
{
    public class UserAccessor : AccessorBase, IUserAccessor
    {
        public bool SignUp() { throw new NotImplementedException(); }

        public void DeactivateAccount() { throw new NotImplementedException(); }
    }
}