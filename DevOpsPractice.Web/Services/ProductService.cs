using DevOpsPractice.Web.Data;
using DevOpsPractice.Web.Models;
using DevOpsPractice.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DevOpsPractice.Web.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        return await _context.Products
            .OrderBy(x => x.Name)
            .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            })
            .ToListAsync();
    }

    public async Task<ProductViewModel?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Where(x => x.Id == id)
            .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ProductViewModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            CreatedDate = DateTime.UtcNow
        };

        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductViewModel model)
    {
        var product = await _context.Products.FindAsync(model.Id);

        if (product == null)
            return;

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}