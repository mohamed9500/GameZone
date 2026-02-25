using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel: GameFormViewModel
    {
        public int Id {  get; set; }
        public string CurrentCover {  get; set; }   
        [AllowExtensions(FileSettings.AllowExtentions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
