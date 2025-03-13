using Microsoft.AspNetCore.Identity;

namespace BlockLuster.EntityFramework
{
    public partial class AspNetUser : IdentityUser
    {
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public bool IsAdmin { get; set; }
        public bool IsDeactivated { get; set; }
    }
}
