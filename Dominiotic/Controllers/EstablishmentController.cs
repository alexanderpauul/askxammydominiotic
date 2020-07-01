using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dominiotic.Entities.DTOs;
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

        [HttpPut]
        public IActionResult Update([FromBody] Establishment establishment)
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

        [HttpGet("{id}")]
        public IActionResult Details(int Id)
        {
            Establishment establishment = unitOfWork.Establishments.GetByID(Id);
            if (establishment != null)
            {
                return Ok(establishment);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Payment([FromBody] ClientCharge clientCharge)
        {
            // Payment received from client
            try
            {
                if (ModelState.IsValid)
                {

                    Receivables receivables = new Receivables
                    {
                        ClientId = clientCharge.ClientId,
                        Amount = clientCharge.Amount,
                        DueDate = DateTime.Now
                    };


                    unitOfWork.Receivables.Insert(receivables);
                    unitOfWork.Save();
                    return Created("Dominiotic/Create", clientCharge);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }

            return BadRequest(clientCharge);
        }

        [HttpGet("{id}")]
        public IActionResult GetReceivables(int Id)
        {
            List<Receivables> receivables = unitOfWork.Receivables
                                                      .Get(x => x.ClientId == Id)
                                                      .Where(x => x.DueDate != null).ToList();
            if (receivables != null)
            {
                return Ok(receivables);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
