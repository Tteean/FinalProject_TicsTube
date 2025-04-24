using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class SubscriptionsCheckService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SubscriptionsCheckService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<TicsTubeDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var now = DateTime.UtcNow;
                var expiredSubscriptions = await db.Subscriptions
                    .Where(s => s.EndDate < now)
                    .Include(s => s.User)
                    .ToListAsync();

                foreach (var subscription in expiredSubscriptions)
                {
                    await userManager.RemoveFromRoleAsync(subscription.User, subscription.Plan);
                    db.Subscriptions.Remove(subscription);
                }

                await db.SaveChangesAsync();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
