using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dominiotic.Controllers
{
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class ReceivablesController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


        [HttpGet("{id}")]
        public IActionResult GetReceivablesDetails(int Id)
        {
            Receivables receivables = unitOfWork.Receivables.GetByID(Id);
            if (receivables != null)
                return Ok(receivables);
            else
            {
                return NoContent();
            }
        }
    }
}
