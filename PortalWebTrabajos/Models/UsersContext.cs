using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PortalWebTrabajos.Models
{
    public class UsersContext : DbContext
    {

        public UsersContext() : base("name=UsersContext")
        {
        }
        public System.Data.Entity.DbSet<PortalWebTrabajos.Models.Users> Users { get; set; }
    }
}
