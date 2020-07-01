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
    [Route("api/v1/Dominiotic/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private DominioticContext dbContext = new DominioticContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DominioticContext());


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

        [HttpPut]
        public IActionResult Update([FromBody] Clients clients)
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

        [HttpPut]
        public IActionResult Unsubscribe([FromBody] Clients clients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Disabling all properties only active field can be modified to unsubscribe
                    dbContext.Entry(clients).Property(x => x.FullName).IsModified = false;
                    dbContext.Entry(clients).Property(x => x.Phone).IsModified = false;
                    dbContext.Entry(clients).Property(x => x.Email).IsModified = false;
                    dbContext.Entry(clients).Property(x => x.Registered).IsModified = false;
                    dbContext.Entry(clients).Property(x => x.Identifier).IsModified = false;

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
        public IActionResult Delete([FromHeader] int Id)
        {
            if (Id != 0)
            {
                // Check client does not have order created.
                Orders orders = unitOfWork.Orders.Get(x => x.ClientId == Id).FirstOrDefault();
                if (orders == null)
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
            else
            {
                return NoContent();
            }
        }

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
            {
                return Ok(clients);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Order([FromBody] ClientOrder clientOrder)
        {
            try
            {
                int _ClientId = clientOrder.ClientId;
                int _PlateId = clientOrder.PlateId;

                Plates plates = unitOfWork.Plates.GetByID(_PlateId);
                List<PlatesItems> platesItems = unitOfWork.PlatesItems.Get(x => x.PlateId == _PlateId).ToList();
                Clients client = unitOfWork.Clients.GetByID(_ClientId);

                if (plates != null && client != null)
                {
                    Orders order = new Orders
                    {
                        ClientId = client.ClientId,
                        Amount = plates.Price,
                        Active = true
                    };

                    // Create order.
                    unitOfWork.Orders.Insert(order);

                    // Looking for items in the order.
                    foreach (var i in platesItems)
                    {
                        OrdersItems ordersItems = new OrdersItems
                        {
                            OrderId = order.OrderId,
                            IngredientId = i.IngredientId
                        };

                        // Add ingredients to order items
                        unitOfWork.OrdersItems.Insert(ordersItems);
                    }

                    // Save the day!
                    unitOfWork.Save();

                    return Created("Dominiotic/Create", order);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBillToPay(int Id)
        {
            // Get the list of comsumed plates to pay.
            List<Receivables> receivables = unitOfWork.Receivables.Get(x => x.ClientId == Id).ToList();
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
