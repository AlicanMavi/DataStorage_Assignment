using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProductRepository(DataContext context)
{
    private readonly DataContext _context = context;

    //Create
    public async Task<ProductEntity> Createasync(ProductEntity product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    //Read
    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<ProductEntity?> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    //Update
    public async Task<ProductEntity?> UpdateAsync(ProductEntity updatedProduct)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
        if (existingProduct != null)
        {
            existingProduct.Id = updatedProduct.Id;
            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.ProductPrice = updatedProduct.ProductPrice;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
        return null;
    }


        //Delete klar
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}


