using Microsoft.AspNetCore.Mvc;

namespace P3A2025WebApiEF01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(ILogger<RecipesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index => index.ToString())
            .ToArray();
        }
    }
}
