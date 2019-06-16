using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using BudgetPad.Server.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using BudgetPad.Server.CoreServices.Expense;
using AutoMapper;
using BudgetPad.Shared.Dtos;

namespace BudgetPad.Server.Controllers.UnplannedExpenses
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnplannedExpensesController : ControllerBase
    {
        private readonly IUnplannedExpenseRepository _unplannedExpenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<UnplannedExpensesController> _logger;
        private readonly IPaymentService _paymentService;

        public UnplannedExpensesController(IUnplannedExpenseRepository unplannedExpenseRepository, ICategoryRepository categoryRepository,
            ILogger<UnplannedExpensesController> logger, IPaymentService paymentService)
        {
            _unplannedExpenseRepository = unplannedExpenseRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _paymentService = paymentService;
        }

        // GET: api/UnplannedExpenses
        [HttpGet]
        public async Task<IActionResult> GetUnplannedExpenses(bool includeNavigationProps = false)
        {
            var expensesFromRepo = await _unplannedExpenseRepository.GetAllDirectNav(includeNavigationProps).ToListAsync();

            var expensesToReturn = Mapper.Map<IEnumerable<UnplannedExpenseDto>>(expensesFromRepo);

            return Ok(expensesToReturn);
        }

        // GET: api/UnplannedExpenses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnplannedExpense(Guid id)
        {
            if(!await _unplannedExpenseRepository.EntryExists(id))
            {
                return NotFound();
            }

            var expenseFromRepo = await _unplannedExpenseRepository.GetById(id, true);

            var expenseToReturn = Mapper.Map<UnplannedExpenseDto>(expenseFromRepo);
            
            return Ok(expenseToReturn);
        }

        // PUT: api/UnplannedExpenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnplannedExpense(Guid id, UnplannedExpenseForCreateDto unplannedExpense)
        {
            if (id != unplannedExpense.Id)
            {
                return BadRequest();
            }

            if(!await _unplannedExpenseRepository.EntryExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expenseFromRepo = await _unplannedExpenseRepository.GetById(id, true);

            var categoryFromRepo = _categoryRepository.GetCategoryByName(unplannedExpense.CategoryName);

            if(categoryFromRepo == null)
            {
                return BadRequest();
            }

            var expenseToUpdate = Mapper.Map(unplannedExpense, expenseFromRepo);
            expenseToUpdate.BudgetCategory = categoryFromRepo;

            await _unplannedExpenseRepository.Create(expenseToUpdate);
            await _paymentService.AddUnplannedExpensePaymentAsync(expenseToUpdate);

            return NoContent();
        }

        // POST: api/UnplannedExpenses
        [HttpPost]
        public async Task<IActionResult> PostUnplannedExpense(UnplannedExpenseForCreateDto unplannedExpense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryFromRepo = _categoryRepository.GetCategoryByName(unplannedExpense.CategoryName);
            if(categoryFromRepo == null)
            {
                return BadRequest();
            }

            var expenseToCreate = Mapper.Map<UnplannedExpense>(unplannedExpense);
            expenseToCreate.BudgetCategory = categoryFromRepo;

            await _unplannedExpenseRepository.Create(expenseToCreate);

            return CreatedAtAction("GetUnplannedExpense", new { id = unplannedExpense.Id }, unplannedExpense);
        }

        // DELETE: api/UnplannedExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnplannedExpense(Guid id)
        {
            if(!await _unplannedExpenseRepository.EntryExists(id))
            {
                return NotFound();
            }

            var unplannedExpense = await _unplannedExpenseRepository.GetById(id, true);

            await _unplannedExpenseRepository.Delete(id);

            return NoContent();
        }
    }
}
