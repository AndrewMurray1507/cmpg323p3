using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement_WebApp.Repositories
{
    public class ZoneRepository : iRepository<Zone, int>
    {
        private readonly ConnectedOfficeContext context;
        public async Task Delete(int id)
        {
            var zone = await context.Zone.FirstOrDefaultAsync(b => b.ZoneId.ToString().Equals(id));
            if (zone != null)
            {
                context.Remove(zone);
            }
        }

        public async Task<IEnumerable<Zone>> GetAll()
        {
            return await context.Zone.Include(b => b.ZoneId).Include(b => b.ZoneName).Include(b => b.ZoneDescription).Include(b => b.DateCreated).ToListAsync();
        }

        public async Task<Zone> GetById(int id)
        {
            return await context.Zone.FindAsync(id);
        }

        public async Task<Zone> Insert(Zone entity)
        {
            context.Zone.Add(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
