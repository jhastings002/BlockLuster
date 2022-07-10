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

        public bool UpdateUser(AspNetUser updatingUser)
        {
            return UsingDatabaseContext(db => {

                var user = db.AspNetUsers.Where(x => x.Id == updatingUser.Id).FirstOrDefault();
                if(user != null)
                {
                    db.AspNetUsers.Update(user);
                    db.SaveChanges();
                }

                return false;
            });
        }

    }
}