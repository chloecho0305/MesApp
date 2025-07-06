using Microsoft.AspNetCore.Mvc;
using VueApp1.Server.Data;
using VueApp1.Server.Models;
namespace VueApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class WorkOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WorkOrdersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.WorkOrders.ToList());
        }
        [HttpPost]
        public IActionResult Create([FromBody] WorkOrder workOrder)
        {
            try
            {
                _context.WorkOrders.Add(workOrder);
                _context.SaveChanges();
                return Ok(workOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, WorkOrder workOrder)
        {
            var existing = _context.WorkOrders.Find(id);
            if (existing == null) return NotFound();
            existing.OrderNumber = workOrder.OrderNumber;
            existing.ProductNumber = workOrder.ProductNumber;
            existing.AssignedTo = workOrder.AssignedTo;
            existing.Status = workOrder.Status;
            existing.CompletedAt = workOrder.CompletedAt;
            _context.SaveChanges();
            return Ok(existing);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workOrder = _context.WorkOrders.Find(id);
            if (workOrder == null) return NotFound();
            _context.WorkOrders.Remove(workOrder);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
