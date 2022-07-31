using Microsoft.AspNetCore.Mvc;
using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Controllers
{
    [ApiController]
    [Route("api/v1/OPD/ClinicBook")]
    public class ClinicBooksController_OPD : Controller
    {

        [HttpPost]
        public ResponseResult Create([FromBody]ClinicBook clinicBook)
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Save(clinicBook);
        }

    }
}
