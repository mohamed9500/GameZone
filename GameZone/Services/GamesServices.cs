using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        public readonly ApplicationDbContext _Context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly string _iamgespath;

        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
            _iamgespath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
       public IEnumerable<Game> GetAll()
        {
            
            return _Context.games
                .Include(g => g.Category)
                 .Include(g => g.Devices)
                 .ThenInclude(d=>d.Device)
              .AsNoTracking()
              .ToList();
           
            
        }
        public Game? GetById(int id)
        {
            
           return _Context.games
             .Include(g => g.Category)
             .Include(g => g.Devices)
             .ThenInclude(d => d.Device)
             .AsNoTracking()
             .SingleOrDefault(g=>g.Id==id);
           
           
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName =$"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path=Path.Combine(_iamgespath, coverName);  
            using var stream=File.Create(path);
            await model.Cover.CopyToAsync(stream);
            
            Game game = new()
            {
                Name=model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=coverName,
                Devices=model.SelectDevices.Select(d=>new GameDevice { DeviceId=d}).ToList()
                
            };
            _Context.Add(game);
            _Context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _Context.games
                .Include(g => g.Category)
                .SingleOrDefault(g => g.Id == model.Id);
            if (game is null)
                return null;

            var hasnewcover = model.Cover is not null;
            var oldcover=game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasnewcover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _Context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasnewcover)
                {
                    var cover = Path.Combine(_iamgespath, oldcover);
                    File.Delete(cover);

                }
                return game;
            }
            else 
            {
                var cover = Path.Combine(_iamgespath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted=false;
            var game=_Context.games.Find(id);
            if (game is null)
             
                return false;
            _Context.games.Remove(game);
            var effectedRows=_Context.SaveChanges();
            if (effectedRows > 0) 
            { 
               isDeleted = true;
                var cover=Path.Combine(_iamgespath, game.Cover);
                File.Delete(cover);
            
            }
            
            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_iamgespath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }

       
    }
}
