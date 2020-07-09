using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CityManager.Repository
{
    public class CityRepository : IRepository<City>
    {
        private readonly CityManagerDbContext context;
        public CityRepository(CityManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<City> Add(City entity)
        {
            context.Set<City>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<City> Delete(int id)
        {
            var entity = await context.Set<City>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<City>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<City> Get(int id)
        {
            return await context.Set<City>()
                            .FindAsync(id);
        }

        public async Task<List<City>> GetAll()
        {
            return await context.Set<City>()
                            .Include(c => c.Country)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<City> Update(City entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}