using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Store> Stores { get; set; }
    }
}