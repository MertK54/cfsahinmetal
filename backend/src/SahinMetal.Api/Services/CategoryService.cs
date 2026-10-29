using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Data;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}
