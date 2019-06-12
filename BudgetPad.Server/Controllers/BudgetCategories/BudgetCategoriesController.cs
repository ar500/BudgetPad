using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using BudgetPad.Shared.Dtos;

namespace BudgetPad.Server.Controllers.BudgetCategories
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCategoriesController : ControllerBase
    {
        private readonly BudgetPadContext _context;

        public BudgetCategoriesController(BudgetPadContext context)
        {
            _context = context;
        }

        // GET: api/BudgetCategories
        [HttpGet("[action]")]
        public async Task<IEnumerable<BudgetCategoryDto>> GetCategories()
        {
            var categoriesFromRepo = await _context.Categories.ToListAsync();

            return Mapper.Map<IEnumerable<BudgetCategoryDto>>(categoriesFromRepo);
        }

        // GET: api/BudgetCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetCategory>> GetBudgetCategory(Guid id)
        {
            var budgetCategory = await _context.Categories.FindAsync(id);

            if (budgetCategory == null)
            {
                return NotFound();
            }

            return budgetCategory;
        }

        // PUT: api/BudgetCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudgetCategory(Guid id, BudgetCategoryDto budgetCategory)
        {
            if (id != budgetCategory.Id)
            {
                return BadRequest();
            }

            if (!BudgetCategoryExists(id))
            {
                return NotFound();
            }

            var categoryFromRepo = _context.Categories.Find(id);
            
            var categoryToUpdate = Mapper.Map(budgetCategory, categoryFromRepo);

            _context.Entry(categoryToUpdate).State = EntityState.Modified;

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

        // POST: api/BudgetCategories
        [HttpPost]
        public async Task<ActionResult<BudgetCategory>> PostBudgetCategory(BudgetCategory budgetCategory)
        {
            _context.Categories.Add(budgetCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudgetCategory", new { id = budgetCategory.Id }, budgetCategory);
        }

        // DELETE: api/BudgetCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BudgetCategory>> DeleteBudgetCategory(Guid id)
        {
            var budgetCategory = await _context.Categories.FindAsync(id);
            if (budgetCategory == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(budgetCategory);
            await _context.SaveChangesAsync();

            return budgetCategory;
        }

        private bool BudgetCategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
