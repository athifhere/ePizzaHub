using Microsoft.AspNetCore.Identity;

namespace ePizzaHub.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
