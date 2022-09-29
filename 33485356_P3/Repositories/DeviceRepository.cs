using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repositories
{
    public class DeviceRepository : iRepository<Device, int>
    {
        private readonly ConnectedOfficeContext context;

        public DeviceRepository(ConnectedOfficeContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            return await context.Device.Include(b => b.DeviceId).Include(b => b.DeviceName).Include(b => b.CategoryId).Include(b => b.ZoneId).Include(b => b.Status).Include(b => b.IsActive).Include(b => b.DateCreated).ToListAsync();
        }

        public async Task<Device> GetById(int id)
        {
            return await context.Device.FindAsync(id);
        }

        public async Task<Device> Insert(Device entity)
        {
            context.Device.Add(entity);
            return entity;
        }
        public async Task Delete(int id)
        {
            var device = await context.Device.FirstOrDefaultAsync(b => b.DeviceId.ToString().Equals(id));
            if (device != null)
            {
                context.Remove(device);
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
