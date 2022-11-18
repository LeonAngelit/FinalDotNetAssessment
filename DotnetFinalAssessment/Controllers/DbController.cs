using DotnetFinalAssessment.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFinalAssessment.Controllers
{

    [ApiController]
    public class DbController : Controller
    {
        private readonly ILogger<DbController> _logger;


        Db_Context dbContext;

        public DbController(
            ILogger<DbController> logger,
            Db_Context context
        )
        {
            _logger = logger;
            dbContext = context;
        }

        [HttpGet]
        [Route("api/createdb")]
        public IActionResult CreateDataBase()
        {
            dbContext.Database.EnsureCreated();
            return Ok();
        }

    }
}
