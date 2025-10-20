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
        public ActionResult<Recipe> UpdateOne(int id, [FromBody] Recipe newRp) {
            //var A - implementace èásteèného update reflexí
            _logger.LogInformation($"Updating {id}", newRp);
            Recipe? newDataR = _dbc.Recipes.Find(id);

            if (newDataR == null)
            {
                return BadRequest();
            }

            updateNonEmptyProperties(source: newRp, target: newDataR);

            _dbc.SaveChanges();

            //var B (jednoduché, bez kontroly)
            //_logger.LogInformation($"Updating {id}", newRp);
            //newRp.RecipeId = id;
            //_dbc.Entry<Recipe>(newRp);
            //_dbc.Entry(newRp).State = EntityState.Modified;
            //_dbc.SaveChanges();

            //var C (komplexnìjší, s kontrolou existence id (UPDATE / INSERT))
            //newRp.RecipeId = id;
            //_dbc.Recipes.Update(newRp);
            //_dbc.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent, newDataR);
        }

        [HttpDelete("{id}")]
        public ActionResult<Recipe> DeleteRecipe(int id)
        {
            var recipe = _dbc.Recipes.FirstOrDefault((r) => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            _dbc.Recipes.Remove(recipe);
            _dbc.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent, recipe);
        }

        private void updateNonEmptyProperties<T>(T target, T source)
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .Where(p => !isNavigationOrCollectionProperty(p.PropertyType));

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source);

                if (isValueSet(value, prop.PropertyType))
                {
                    prop.SetValue(target, value);
                }
            }
        }
        private bool isNavigationOrCollectionProperty(Type type)
        {
            if (type == typeof(string)) return false;

            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type)) return true;

            return false;
        }
        private bool isValueSet(object? value, Type type)
        {
            if (value == null) return false;

            if (type == typeof(string))
            {
                return !string.IsNullOrEmpty((string)value);
            }

            return !value.Equals(Activator.CreateInstance(type));
        }
    }
}
