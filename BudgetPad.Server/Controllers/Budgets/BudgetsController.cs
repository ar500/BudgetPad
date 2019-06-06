using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using AutoMapper;
using BudgetPad.Shared.Dtos;

namespace BudgetPad.Server.Controllers.Budgets
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetPadContext _context;

        public BudgetsController(BudgetPadContext context)
        {
            _context = context;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<IActionResult> GetBudgets(bool includeBills = false)
        {
            List<Budget> budgetsFromRepo;

            if (includeBills)
            {
                budgetsFromRepo = await _context.Budgets
                    .Include(b => b.Bills)
                    .ThenInclude(c => c.BudgetCategory)
                    .ToListAsync();
            }
            else
            {
                budgetsFromRepo = await _context.Budgets
                    .ToListAsync();
            }

            var budgetsToReturn = Mapper.Map<IEnumerable<BudgetDto>>(budgetsFromRepo);

            return Ok(budgetsFromRepo);
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget(Guid id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            var budgetToReturn = Mapper.Map<BudgetDto>(budget);

            return Ok(budgetToReturn);
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(Guid id, Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Budgets
        [HttpPost]
        public async Task<IActionResult> PostBudget(BudgetForCreateDto budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billsToAttach = await _context.Bills
                .Include(b => b.BudgetCategory)
                .Where(e => budget.ReturnedBillNames.Contains(e.ShortName))
                .ToListAsync();

            var budgetToCreate = Mapper.Map<Budget>(budget);
            budgetToCreate.Bills = billsToAttach;

            _context.Budgets.Add(budgetToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudget", new { id = budget.Id }, budget);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Budget>> DeleteBudget(Guid id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return budget;
        }

        private bool BudgetExists(Guid id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}
