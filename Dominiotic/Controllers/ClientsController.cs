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
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());

        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = unitOfWork.Clients.Get();
                if (clients != null)
                    return Ok(clients);
                else
                    return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetClientsDetails(int Id)
        {
            Clients clients = unitOfWork.Clients.GetByID(Id);
            if (clients != null)
                return Ok(clients);
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Clients clients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Clients.Insert(clients);
                    unitOfWork.Save();
                    return Created("Dominiotic/Create", clients);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(clients);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult UpdateUser([FromBody] Clients clients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Clients.Update(clients);
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
                unitOfWork.Clients.Delete(Id);
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
