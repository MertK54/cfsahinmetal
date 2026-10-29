using SahinMetal.Api.Entities;

namespace SahinMetal.Api.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}
