using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using AutoMapper;
using BudgetPad.Shared.Dtos;
using BudgetPad.Shared.Dtos.DtoExtensions;

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

            var budgetsToReturn = Mapper.Map<IEnumerable<BudgetDtoExtension>>(budgetsFromRepo);

            return Ok(budgetsToReturn);
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget(Guid id)
        {
            var budget = await _context.Budgets
                .Include(b => b.Bills)
                .ThenInclude(c => c.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (budget == null)
            {
                return NotFound();
            }

            var budgetToReturn = Mapper.Map<BudgetDtoExtension>(budget);

            return Ok(budgetToReturn);
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(Guid id, BudgetForCreateDto budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            if (!BudgetExists(id))
            {
                return NotFound();
            }

            var budgetToUpdate = await _context.Budgets.FindAsync(id);

            var updatedBudget = Mapper.Map(budget, budgetToUpdate);

            _context.Entry(updatedBudget).State = EntityState.Modified;

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
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("[action]/{budgetId}")]
        public async Task<IActionResult> UpdateBills(Guid budgetId, [FromBody] List<Guid> billIds)
        {
            if (!BudgetExists(budgetId))
            {
                return BadRequest();
            }

            var budgetToUpdate = await _context.Budgets
                .Include(b => b.Bills)
                .ThenInclude(c => c.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == budgetId);

            if(budgetToUpdate == null)
            {
                return NotFound();
            }

            var billsFromUser = await _context.Bills
                .Where(bill => billIds.Contains(bill.Id))
                .Include(c => c.BudgetCategory)
                .ToListAsync();

            foreach(var bill in billsFromUser) // for every bill that the user wants to add
            {
                if (!budgetToUpdate.Bills.Contains(bill)) // if it is not already added to the budget in the db
                {
                    budgetToUpdate.Bills.Add(bill); // add it
                }
            }

            // Now we can intersect the list to remove the bills that were removed
            budgetToUpdate.Bills = budgetToUpdate.Bills.Intersect(billsFromUser).ToList();

            _context.Entry(budgetToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw;
            }

            return NoContent();
        }

        private bool BudgetExists(Guid id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}
