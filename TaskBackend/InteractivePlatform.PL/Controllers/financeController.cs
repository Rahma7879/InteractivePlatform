using InteractivePlatform.BL.Services;
using InteractivePlatform.BL.UOW;
using InteractivePlatform.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteractivePlatform.PL.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    public class financeController : ControllerBase
    {
        UnitOfWorks unit;

        public financeController(UnitOfWorks unit)
        {
            this.unit = unit;
        }
        [HttpGet]
        public ActionResult Getfinance()
        {
            List<FinanceRequest> clients = unit.FinanceRequestRepository.selectall();
            return Ok(clients);
        }

        [HttpGet("pagination")]
        public ActionResult GetfinancePagination(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 4)
        {
            var clients = unit.FinanceRequestRepository.GetPaged(page, pageSize);
            var totalClientsCount = unit.FinanceRequestRepository.Count();

            var totalPages = (int)Math.Ceiling((double)totalClientsCount / pageSize);

            return Ok(new { clients, totalPages });
        }

    }
}


