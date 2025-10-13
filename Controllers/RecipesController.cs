using Microsoft.AspNetCore.Mvc;
using P3A2025WebApiEF01.Data;
using P3A2025WebApiEF01.Models;

namespace P3A2025WebApiEF01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly RecipeDbContext _dbc;

        public RecipesController(ILogger<RecipesController> logger, RecipeDbContext dbc)
        {
            _logger = logger;
            _dbc = dbc;

        }

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _dbc.Recipes.AsEnumerable();
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetOne(int id)
        {
            var result = _dbc.Recipes.Find(id);
            return (result is null) ? NotFound() : Ok(result);
        }

        [HttpPost]
        public ActionResult<Recipe> InsertOne([FromBody] Recipe newRp)
        {
            _dbc.Recipes.Add(newRp);
            _dbc.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newRp);
        }
    }
}
