using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        public readonly ApplicationDbContext _Context;

        public CategoriesService(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {     return _Context.Categories
                .AsNoTracking()
                 .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                  .OrderBy(c => c.Text)
                  
                  .ToList();
        }
    }
}
