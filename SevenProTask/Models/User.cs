using Microsoft.AspNetCore.Identity;

namespace SevenProTask.Models
{
    public class User : IdentityUser
    {
        public string UserLastName { get; set; }
    }
}