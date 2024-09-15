using AcuaParkIdentity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AcuaParkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class testBBDDController: ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public testBBDDController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetName")]
        public async Task<IActionResult> getName()
        {
            var res = await _context.Tests.FindAsync(1);
            return Ok(res.Name);
        }
    }
}
