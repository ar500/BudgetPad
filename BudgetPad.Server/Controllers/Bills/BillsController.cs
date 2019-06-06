using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using BudgetPad.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace BudgetPad.Server.Controllers.Bills
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly BudgetPadContext _context;
        private readonly ILogger<BillsController> _logger;

        public BillsController(BudgetPadContext context, ILogger<BillsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<IActionResult> GetBills(bool includeCategories = false)
        {
            List<Bill> BillsFromRepo;

            if (includeCategories)
            {
                BillsFromRepo = await _context.Bills
                    .Include(b => b.BudgetCategory)
                    .ToListAsync();
            }
            else
            {
                BillsFromRepo = await _context.Bills
                    .ToListAsync();
            }

            var BillsForUser = Mapper.Map<IEnumerable<BillDto>>(BillsFromRepo);

            return Ok(BillsForUser);
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(Guid id)
        {
            var bill = await _context.Bills
                .Include(c => c.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bill == null)
            {
                return NotFound();
            }

            var billForUser = Mapper.Map<BillForCreateDto>(bill);

            return Ok(billForUser);
        }

        // PUT: api/Bills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(Guid id, BillForCreateDto bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            if (!BillExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billFromRepo = await _context.Bills
                .Include(c => c.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id);

            var categoryFromRepo = await _context.Categories
                .FirstOrDefaultAsync(c => c.ShortName == bill.CategoryName);

            if (categoryFromRepo == null)
            {
                return BadRequest();
            }
            
            var billToUpdate = Mapper.Map(bill, billFromRepo);
            billToUpdate.BudgetCategory = categoryFromRepo;

            _context.Entry(billToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Bills
        [HttpPost]
        public async Task<IActionResult> PostBill(BillForCreateDto bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryFromRepo = await _context.Categories
                .FirstOrDefaultAsync(b => b.ShortName == bill.CategoryName);

            if (categoryFromRepo == null)
            {
                return BadRequest();
            }

            var billToAdd = Mapper.Map<Bill>(bill);
            billToAdd.BudgetCategory = categoryFromRepo;
            
            _context.Bills.Add(billToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError("A DB update exception was caught.");
                _logger.LogError($"The exception was: {ex.Message}");
                _logger.LogError($"The exception was: {ex.InnerException}");
            }

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            var billToReturn = Mapper.Map<BillDto>(bill);

            return Ok(billToReturn);
        }

        private bool BillExists(Guid id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
