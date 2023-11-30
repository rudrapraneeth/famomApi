using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeMade.Infrastructure.Data.DbContext
{
    public partial class FamomAuditContext: FamomContext
    {
        public FamomAuditContext(DbContextOptions<FamomContext> options)
            : base(options) { }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetAuditProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        private void SetAuditProperties()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditProperties entity)
                {
                    var now = DateTime.Now;
                    var user = "Famom";

                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entity.UpdateDateTime = now;
                            entity.UpdateBy = user;
                            break;
                        case EntityState.Added:
                            entity.CreateDateTime = now;
                            entity.CreatedBy = user;
                            entity.UpdateDateTime = now;
                            entity.UpdateBy = user;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
