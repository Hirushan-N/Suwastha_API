using Microsoft.AspNetCore.Mvc;
using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Controllers
{
    [ApiController]
    [Route("api/v1/Clinic/ClinicBook")]
    public class ClinicBooksController_CNC : Controller
    {

        [HttpPost]
        public ResponseResult Create([FromBody]ClinicBook clinicBook)
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Save(clinicBook);
        }

        [HttpGet]
        public ResponseResult Read()
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Read();
        }

        [HttpPut]
        public ResponseResult Update([FromBody] ClinicBook clinicBook)
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Save(clinicBook);
        }

        [HttpPut]
        [Route("Delete")]
        public ResponseResult Delete([FromBody]ClinicBook clinicBook)
        {
            ClinicBookService clinicBookService = new ClinicBookService();
            return clinicBookService.Delete(clinicBook);
        }

    }
}
