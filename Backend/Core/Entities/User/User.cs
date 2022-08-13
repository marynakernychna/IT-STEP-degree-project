using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoLink { get; set; }
        public DateTimeOffset RegistrationDate { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Ware> Wares { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
