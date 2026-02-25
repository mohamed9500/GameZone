using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public readonly ICategoriesService _CategoriesService;
        public readonly IDevicesService _DevicesService;
        public readonly IGamesServices _GamesServices;
        public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IDevicesService devicesService, IGamesServices gamesServices)
        {
            _CategoriesService = categoriesService;
            _Context = context;
            _DevicesService = devicesService;
            _GamesServices = gamesServices;
        }



        public IActionResult Index()
        {
            var games = _GamesServices.GetAll();

            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _GamesServices.GetById(id);
            if (game is null)
                return NotFound();
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormViewModel viewModel = new()
            {

                Categories = _CategoriesService.GetSelectList(),

                Devices = _DevicesService.GetSelectList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!
                ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();
                model.Devices = _DevicesService.GetSelectList();

                return View(model);
            }

            await _GamesServices.Create(model);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _GamesServices.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                CategoryId = game.CategoryId,
                Description = game.Description,
                SelectDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _CategoriesService.GetSelectList(),
                Devices = _DevicesService.GetSelectList(),
                CurrentCover = game.Cover,
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!
                ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();
                model.Devices = _DevicesService.GetSelectList();

                return View(model);
            }

            var game=await _GamesServices.Update(model);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            
            var isDeleted= _GamesServices.Delete(id);

            return isDeleted ? Ok():BadRequest();
        }

    }
}