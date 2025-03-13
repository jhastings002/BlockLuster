using BlockLuster.Accessors.Interfaces;
using BlockLuster.EntityFramework;

namespace BlockLuster.Accessors.Accessors
{
    public class UserAccessor : AccessorBase, IUserAccessor
    {
        public AspNetUser GetUser(string userId)
        {
            return UsingDatabaseContext(db => {
                return db.AspNetUsers.Where(x => x.Id == userId).First();
            });
        }

        public bool UpdateUser(AspNetUser updatingUser)
        {
            return UsingDatabaseContext(db => {

                var user = db.AspNetUsers.Where(x => x.Id == updatingUser.Id).FirstOrDefault();
                if(user != null)
                {
                    user.FirstName = updatingUser.FirstName;
                    user.LastName = updatingUser.LastName;
                    db.AspNetUsers.Update(user);
                    db.SaveChanges();
                }

                return false;
            });
        }

    }
}