using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Projects
                    .Include(p => p.Customer)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid hämtning av projekt: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                throw;
            }
        }

        public async Task<ProjectEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<ProjectEntity>()
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(ProjectEntity entity)
        {
            try
            {
                await _context.Projects.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid sparande av projekt: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                throw;
            }
        }

        public async Task UpdateAsync(ProjectEntity entity)
        {
            _context.Set<ProjectEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<ProjectEntity>().FirstOrDefaultAsync(p => p.Id == id);
            if (entity != null)
            {
                _context.Set<ProjectEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
