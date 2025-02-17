using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context)
{
    private readonly DataContext _context = context;

    // Create
    public async Task<StatusTypeEntity> CreateAsync(StatusTypeEntity statusType)
    {
        _context.StatusTypes.Add(statusType);
        await _context.SaveChangesAsync();
        return statusType;
    }
    // Read
    public async Task<IEnumerable<StatusTypeEntity>> GetAllAsync()
    {
        return await _context.StatusTypes.ToListAsync();
    }

    public async Task<StatusTypeEntity?> GetByIdAsync(int id)
    {
        return await _context.StatusTypes.FirstOrDefaultAsync(s => s.Id == id);
    }
    //Update
    public async Task<StatusTypeEntity?> UpdateAsync(StatusTypeEntity updatedStatusType)
    {
        var existingStatusType = await _context.StatusTypes.FirstOrDefaultAsync(s => s.Id == updatedStatusType.Id);
        if (existingStatusType != null)
        {
            existingStatusType.Id = updatedStatusType.Id;
            existingStatusType.StatusName = updatedStatusType.StatusName;

            
            await _context.SaveChangesAsync();
            return existingStatusType; 
        }
        return null;
    }



    //Delete klar
    public async Task<bool> DeleteAsync(int id)
    {
        var statusType = await _context.StatusTypes.FirstOrDefaultAsync(s => s.Id == id);
        if (statusType != null)
        {
            _context.StatusTypes.Remove(statusType);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}


