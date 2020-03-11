using System;
using System.Collections.Generic;

namespace Deskrs.DAL.EF.Entities
{
    public partial class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
