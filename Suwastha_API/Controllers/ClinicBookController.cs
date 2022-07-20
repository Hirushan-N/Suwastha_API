using Microsoft.AspNetCore.Mvc;
using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClinicBookController : Controller
    {
        [HttpGet]
        public ResponseResult Read()
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Read();
        }
    }
}
