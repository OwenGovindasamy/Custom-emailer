using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mailer.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
    }
}