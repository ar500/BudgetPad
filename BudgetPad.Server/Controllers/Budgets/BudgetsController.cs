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
using BudgetPad.Server.DataAccess.Repositories;

namespace BudgetPad.Server.Controllers.Budgets
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetPadContext _context;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBillRepository _billRepository;

        public BudgetsController(BudgetPadContext context, IBudgetRepository budgetRepository, IBillRepository billRepository)
        {
            _context = context;
            _budgetRepository = budgetRepository;
            _billRepository = billRepository;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<IActionResult> GetBudgets(bool includeBills = false)
        {
            IEnumerable<Budget> budgetsFromRepo;

            if (includeBills)
            {
                budgetsFromRepo = _budgetRepository.GetBudgets();
            }
            else
            {
                budgetsFromRepo = _budgetRepository.GetAllDirectNav(false);
            }

            var budgetsToReturn = Mapper.Map<IEnumerable<BudgetDtoExtension>>(budgetsFromRepo);

            return Ok(budgetsToReturn);
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget(Guid id)
        {
            var budget = await _budgetRepository.GetBudget(id);

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

            if (!await _budgetRepository.EntryExists(id))
            {
                return NotFound();
            }

            var budgetToUpdate = await _budgetRepository.GetById(id);

            var updatedBudget = Mapper.Map(budget, budgetToUpdate);

            await _budgetRepository.Update(id, updatedBudget);

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

            var billsToAttach = _billRepository.GetBillsFromNameList(budget.ReturnedBillNames);

            var budgetToCreate = Mapper.Map<Budget>(budget);
            budgetToCreate.Bills = billsToAttach;

            await _budgetRepository.Create(budgetToCreate);

            return CreatedAtAction("GetBudget", new { id = budget.Id }, budget);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            if(!await _budgetRepository.EntryExists(id))
            {
                return NoContent();
            }

            await _budgetRepository.Delete(id);

            return NoContent();
        }

        [HttpPost("[action]/{budgetId}")]
        public async Task<IActionResult> UpdateBills(Guid budgetId, [FromBody] List<Guid> billIds)
        {
            if (!await _budgetRepository.EntryExists(budgetId))
            {
                return BadRequest();
            }

            var budgetToUpdate = await _budgetRepository.GetBudget(budgetId);

            if(budgetToUpdate == null)
            {
                return NotFound();
            }

            var billsFromUser = _billRepository.GetBillsFromIdList(billIds);

            foreach(var bill in billsFromUser) // for every bill that the user wants to add
            {
                if (!budgetToUpdate.Bills.Contains(bill)) // if it is not already added to the budget in the db
                {
                    budgetToUpdate.Bills.Add(bill); // add it
                }
            }

            // Now we can intersect the list to remove the bills that were removed
            budgetToUpdate.Bills = budgetToUpdate.Bills.Intersect(billsFromUser).ToList();

            await _budgetRepository.Update(budgetToUpdate.Id, budgetToUpdate);

            return NoContent();
        }
    }
}
