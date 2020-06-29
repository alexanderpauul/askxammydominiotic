using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dominiotic.Controllers
{
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());

        [HttpPost]
        public IActionResult Create([FromBody] Orders orders)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Clients.Insert(orders);
                    unitOfWork.Save();
                    return Created("Dominiotic/Create", orders);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(orders);
        }

    }
}
