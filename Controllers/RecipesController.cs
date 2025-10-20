using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            _logger.LogInformation("vypsány všechny recepty");
            return _dbc.Recipes.AsNoTracking().AsEnumerable();
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetOne(int id)
        {
            Recipe result = _dbc.Recipes.Find(id);
            return (result is null) ? NotFound() : Ok(result);
        }

        [HttpPost]
        public ActionResult<Recipe> InsertOne([FromBody] Recipe newRp)
        {
            _dbc.Recipes.Add(newRp);
            _dbc.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newRp);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOne(int id, [FromBody] Recipe newRp) {
            //var A
            //Recipe? newDataR = _dbc.Recipes.Find(id);

            //if (newDataR == null)
            //{
            //    return BadRequest();
            //}
            //newDataR.Title = newRp.Title;
            //newDataR.Description = newRp.Description;
            //_dbc.SaveChanges();

            //var B
            //newRp.RecipeId = id;
            //_dbc.Entry<Recipe>(newRp);
            //_dbc.Entry(newRp).State = EntityState.Modified;
            //_dbc.SaveChanges();

            //var C
            _dbc.Recipes.Update(newRp);
            _dbc.SaveChanges();

            return NoContent();
        }
    }
}
