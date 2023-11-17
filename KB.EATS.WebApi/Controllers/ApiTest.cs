using KB.EATS.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KB.EATS.WebApi.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class ApiTest : ControllerBase
    {
        [HttpGet("get")]
        public ResponseModel TestGet()
        {
            return new ResponseModel() { Data = null, Message = "Test1", Result = true };
        }

        [HttpGet("getbyid/{id}")]
        public ResponseModel TestGet(int id)
        {
            return new ResponseModel() { Data = id, Message = "Test1", Result = true };
        }
    }
}
