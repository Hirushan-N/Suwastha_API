using Microsoft.AspNetCore.Mvc;
using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Controllers.ChannelingOfficeControllers
{
    [ApiController]
    [Route("api/v1/Admin/ChannelingOffice")]
    public class ChannelingOfficeController_Admin : Controller
    {
        [HttpPost]
        public ResponseResult Create([FromBody]ChannelingOffice channelingOffice)
        {
            channelingOffice.setChannelingOfficeID();
            ChannelingOfficeService channelingOfficeService = new ChannelingOfficeService();
            return channelingOfficeService.Save(channelingOffice);
        }

        [HttpPut]
        public ResponseResult Update([FromBody]ChannelingOffice channelingOffice)
        {
            ChannelingOfficeService channelingOfficeService = new ChannelingOfficeService();
            return channelingOfficeService.Save(channelingOffice);
        }

        [HttpPut]
        [Route("Delete")]
        public ResponseResult Delete([FromBody]ChannelingOffice channelingOffice)
        {
            ChannelingOfficeService channelingOfficeService = new ChannelingOfficeService();
            return channelingOfficeService.Delete(channelingOffice);
        }
    }
}
