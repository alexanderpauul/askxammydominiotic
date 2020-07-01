using Dominiotic.Entities.Models;
using Dominiotic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace Dominiotic.Controllers
{
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


        [HttpPost]
        public IActionResult Create([FromBody] Ingredients ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Ingredients.Insert(ingredients);
                    unitOfWork.Save();
                    return Created("UnicornDemo/Create", ingredients);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(ingredients);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Ingredients ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Ingredients.Update(ingredients);
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
                var ingredients = unitOfWork.Ingredients.Get();
                if (ingredients != null)
                    return Ok(ingredients);
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
            Ingredients ingredients = unitOfWork.Ingredients.GetByID(Id);
            if (ingredients != null)
                return Ok(ingredients);
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
                unitOfWork.Ingredients.Delete(Id);
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
