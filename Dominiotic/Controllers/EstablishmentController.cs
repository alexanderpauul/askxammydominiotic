using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dominiotic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


        [HttpGet("{id}")]
        public IActionResult GetEstablishmentDetails(int Id)
        {
            Establishment establishment = unitOfWork.Establishments.GetByID(Id);
            if (establishment != null)
                return Ok(establishment);
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Establishment establishment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Establishments.Insert(establishment);
                    unitOfWork.Save();
                    return Created("Dominiotic/Create", establishment);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(establishment);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult UpdateUser([FromBody] Establishment establishment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Establishments.Update(establishment);
                    unitOfWork.Save();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromHeader] int Id)
        {

            if (Id != 0)
            {
                unitOfWork.Establishments.Delete(Id);
                unitOfWork.Save();
                return Ok("Client Deleted");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
