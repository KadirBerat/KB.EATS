using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KB.EATS.WebApi.Controllers
{
    [Route("api/shift")]
    [ApiController]
    public class UserShiftManager : ControllerBase
    {
        private readonly ILogger<UserShiftManager> _logger;

        public UserShiftManager(ILogger<UserShiftManager> logger)
        {
            _logger = logger;
        }

        //public IActionResult AddShiftData()
    }
}
