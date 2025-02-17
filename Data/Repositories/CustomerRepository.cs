using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context)
{
    private readonly DataContext _context = context;

    //Create
    public async Task<CustomerEntity> Createasync(CustomerEntity customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
    //Read
    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }
    //Update
    public async Task<CustomerEntity?> UpdateAsync(CustomerEntity updatedcustomer)
    {
        var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id  == updatedcustomer.Id);
        if (existingCustomer != null)
        {
            existingCustomer.CustomerName = updatedcustomer.CustomerName;
            existingCustomer.CustomerEmail = updatedcustomer.CustomerEmail;
            existingCustomer.CustomerPhoneNumber = updatedcustomer.CustomerPhoneNumber;

            await _context.SaveChangesAsync();
            return existingCustomer;
        }
        return null;
    }
    //Delete klar
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}


