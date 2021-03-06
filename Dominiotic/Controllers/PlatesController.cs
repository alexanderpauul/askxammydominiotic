﻿using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace Dominiotic.Controllers
{
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


        [HttpPost]
        public IActionResult Create([FromBody] Plates plates)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Plates.Insert(plates);
                    unitOfWork.Save();
                    return Created("UnicornDemo/Create", plates);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(plates);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Plates plates)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Plates.Update(plates);
                    unitOfWork.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var plates = unitOfWork.Plates.Get();
                if (plates != null)
                    return Ok(plates);
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int Id)
        {
            Plates plates = unitOfWork.Plates.GetByID(Id);
            if (plates != null)
                return Ok(plates);
            else
            {
                return NoContent();
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromHeader] int Id)
        {
            if (Id != 0)
            {
                unitOfWork.Plates.Delete(Id);
                unitOfWork.Save();
                return Ok("Ingredients deleted");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
