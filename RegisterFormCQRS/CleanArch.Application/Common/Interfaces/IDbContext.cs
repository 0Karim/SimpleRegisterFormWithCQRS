using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Common.Interfaces
{
    public interface IDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Governate> Governate { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<AddressInfo> AddressInfo { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
