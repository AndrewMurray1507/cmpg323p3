using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repositories
{
    public class CategoryRepository : iRepository<Category, int>
    {
        private readonly ConnectedOfficeContext context;
        public async Task Delete(int id)
        {
            var category = await context.Category.FirstOrDefaultAsync(b => b.CategoryId.ToString().Equals(id));
            if(category!=null)
            {
                context.Remove(category);
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await context.Category.Include(b => b.CategoryId).Include(b => b.CategoryName).Include(b => b.CategoryDescription).Include(b => b.DateCreated).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await context.Category.FindAsync(id);
        }

        public async Task<Category> Insert(Category entity)
        {
            context.Category.Add(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
