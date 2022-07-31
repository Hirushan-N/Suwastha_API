using Microsoft.AspNetCore.Mvc;
using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Controllers.ChannelingOfficeControllers
{
    [ApiController]
    [Route("api/v1/ChannelingOffice")]
    public class ChannelingOfficeController_COF : Controller
    {
        [HttpPost]
        [Route("Login")]
        public ResponseResult Login([FromBody]LoginRequest loginRequest)
        {
            ChannelingOfficeService channelingOfficeService = new ChannelingOfficeService();
            return channelingOfficeService.Login(loginRequest);
        }

    }
}
