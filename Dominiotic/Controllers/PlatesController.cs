using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dominiotic.Controllers
{
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


        [HttpGet("{id}")]
        public IActionResult GetReceivablesDetails(int Id)
        {
            Plates plates = unitOfWork.Plates.GetByID(Id);
            if (plates != null)
                return Ok(plates);
            else
            {
                return NoContent();
            }
        }
    }
}
