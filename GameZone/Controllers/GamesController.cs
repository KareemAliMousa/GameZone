namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _deviceService;
        private readonly IGamesService _gamesService;
        public GamesController(ICategoriesService categoriesService, IDevicesService deviceService, IGamesService gameService)
        {
            _categoriesService = categoriesService;
            _deviceService = deviceService;
            _gamesService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _deviceService.GetSelectList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _deviceService.GetSelectList();
                return View(model);

            }
            //Save game to database
            //Save cover to server
            await _gamesService.create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var game = _gamesService.GetById(id);
            if (game is null)
            {
                return NotFound();
            }
            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Desctiption = game.Desctiption,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _deviceService.GetSelectList(),
                CurrentCover = game.Cover, 
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _deviceService.GetSelectList();
                return View(model);

            }

            var game = await _gamesService.Edit(model);
            if (game is null)
                return BadRequest(); 
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var isDeleted = _gamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
