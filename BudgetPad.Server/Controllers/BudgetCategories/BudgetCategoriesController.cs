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
using BudgetPad.Server.DataAccess.Repositories;

namespace BudgetPad.Server.Controllers.BudgetCategories
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public BudgetCategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/BudgetCategories
        [HttpGet("[action]")]
        public IEnumerable<BudgetCategoryDto> GetCategories()
        {
            var categoriesFromRepo = _repository.GetAllDirectNav(false);

            return Mapper.Map<IEnumerable<BudgetCategoryDto>>(categoriesFromRepo);
        }

        // GET: api/BudgetCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetCategoryDto>> GetBudgetCategory(Guid id)
        {
            var budgetCategory = await _repository.GetById(id);

            if (budgetCategory == null)
            {
                return NotFound();
            }

            var categoryToReturn = Mapper.Map<BudgetCategoryDto>(budgetCategory);

            return categoryToReturn;
        }

        // PUT: api/BudgetCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudgetCategory(Guid id, BudgetCategoryDto budgetCategory)
        {
            if (id != budgetCategory.Id)
            {
                return BadRequest();
            }

            if (!await _repository.EntryExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryToUpdate = await _repository.GetById(id);

            Mapper.Map(budgetCategory, categoryToUpdate);

            await _repository.Update(id, categoryToUpdate);

            return NoContent();
        }

        // POST: api/BudgetCategories
        [HttpPost]
        public async Task<IActionResult> PostBudgetCategory(BudgetCategoryDto budgetCategory)
        {
            var categoryToCreate = Mapper.Map<BudgetCategory>(budgetCategory);

            await _repository.Create(categoryToCreate);

            return CreatedAtAction("GetBudgetCategory", new { id = budgetCategory.Id }, budgetCategory);
        }

        // DELETE: api/BudgetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetCategory(Guid id)
        {
            await _repository.Delete(id);

            return NoContent();
        }
    }
}
