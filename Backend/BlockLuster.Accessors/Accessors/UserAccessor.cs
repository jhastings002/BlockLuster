using BlockLuster.Accessors.Interfaces;
using BlockLuster.EntityFramework;

namespace BlockLuster.Accessors.Accessors
{
    public class UserAccessor : AccessorBase, IUserAccessor
    {
        public bool SignUp(AspNetUser user) { 
            
            return UsingDatabaseContext(db => {
                db.AspNetUsers.Add(user);
                return true; 
            }); 
        
        }

        public bool DeactivateAccount(string userId)
        {
            return UsingDatabaseContext(db => {

                var user = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
                if(user != null)
                {
                    user.IsDeactivated = true;
                    db.SaveChanges();
                }

                return false;
            });
        }

        public bool ReactivateAccount(string userId)
        {
            return UsingDatabaseContext(db => {

                var user = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.IsDeactivated = false;
                    db.SaveChanges();
                }

                return false;
            });
        }
    }
}