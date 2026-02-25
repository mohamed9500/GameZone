using GameZone.Controllers;
using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {
        public readonly ApplicationDbContext _Context;
        public DevicesService(ApplicationDbContext context)
        {
            _Context = context; 
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
                 return _Context.Devices
                 .AsNoTracking()
                 .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                 .OrderBy(d => d.Text)
                
                 .ToList();
            
        }
    }
}
