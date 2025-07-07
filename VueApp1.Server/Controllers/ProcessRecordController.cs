using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueApp1.Server.Data;
using VueApp1.Server.Models;
namespace VueApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class ProcessRecordController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProcessRecordController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("workorder/{workOrderId}")]
        public async Task<ActionResult<IEnumerable<ProcessRecord>>> GetByWorkOrderId(int workOrderId)
        {   
            var records = await _context.ProcessRecords
                .Where(pr => pr.WorkOrderId == workOrderId)
                .ToListAsync();
            return Ok(records);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProcessRecord>>> Create([FromBody] ProcessRecord processRecord)
        {
            try
            {
                _context.ProcessRecords.Add(processRecord);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByWorkOrderId), new { workOrderId = processRecord.WorkOrderId }, processRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessRecord>> GetById(int id)
        {
            var record = await _context.ProcessRecords.FindAsync(id);
            if (record == null) return NotFound();

            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProcessRecord updated)
        {
            if (id != updated.Id) return BadRequest("ID mismatch");

            var existing = await _context.ProcessRecords.FindAsync(id);
            if (existing == null) return NotFound();

            // 明確指定要更新哪些欄位（可以依需求擴充）
            existing.ProcessName = updated.ProcessName;
            existing.StartTime = updated.StartTime;
            existing.EndTime = updated.EndTime;
            existing.Operator = updated.Operator;
            existing.Equipment = updated.Equipment;
            existing.Status = updated.Status;

            await _context.SaveChangesAsync();
            return Ok(existing); // 或 NoContent()，看你要不要回傳內容
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.ProcessRecords.FindAsync(id);
            if (record == null) return NotFound();
            _context.ProcessRecords.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
