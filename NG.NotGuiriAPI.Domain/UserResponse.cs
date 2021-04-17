using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;
using System.Collections.Generic;

namespace NG.NotGuiriAPI.Domain
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public Guid? ImageId { get; set; }
        public IList<Commerce> Commerces { get; set; }
        public Image Image { get; set; }
    }
}
