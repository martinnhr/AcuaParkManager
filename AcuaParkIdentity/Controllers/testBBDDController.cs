using Microsoft.AspNetCore.Mvc;
using AcuaParkRepository;
using Microsoft.AspNetCore.Authorization;

namespace AcuaParkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class testBBDDController: ControllerBase
    {

        private readonly ItestBBDDRepository _testBBDDRepository;

        public testBBDDController(ItestBBDDRepository testBBDDRepository)
        {
            _testBBDDRepository = testBBDDRepository;
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetName")]
        public async Task<IActionResult> getName()
        {

            return Ok(await _testBBDDRepository.GetName());
        }
    }
}
