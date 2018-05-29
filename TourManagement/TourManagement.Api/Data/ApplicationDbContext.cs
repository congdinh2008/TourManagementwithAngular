using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourManagement.Api.Models;
using TourManagement.Api.Services;

namespace TourManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserInfoService _userInfoService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IUserInfoService userInfoService) : base(options)
        {
            _userInfoService = userInfoService ?? throw new
                ArgumentNullException(nameof(userInfoService));
        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Show> Shows { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // get added or updated entries
            var addedOrUpdatedEntries = ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            // fill out the audit fields
            foreach (var entry in addedOrUpdatedEntries)
            {
                var entity = entry.Entity as AuditableModel;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = _userInfoService.UserId;
                    entity.CreatedOn = DateTime.UtcNow;
                }

                entity.UpdatedBy = _userInfoService.UserId;
                entity.UpdatedOn = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
