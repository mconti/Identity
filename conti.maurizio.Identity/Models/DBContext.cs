using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conti.maurizio.Models
{
    public class DBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public DBContext(DbContextOptions options): base(options)
        {
            _options = options;
            // userid posta@maurizioconti.com 040c83cf-6c8f-45fe-99f2-d94b39045ffb
            // roleid Manager 0ee41a14-7ec6-4297-a030-58952d8b528d

            // userid maurizio.conti eeec7aa2-21cf-4b91-9408-79b075bce1ad
            // roleid Administrator 467696ff-3966-4b0e-9124-1c52cc9c07ec


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
