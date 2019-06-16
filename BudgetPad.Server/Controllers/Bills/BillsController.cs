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
using Microsoft.Extensions.Logging;
using BudgetPad.Server.CoreServices.Expense;
using BudgetPad.Shared.Dtos.DtoExtensions;
using BudgetPad.Server.DataAccess.Repositories;

namespace BudgetPad.Server.Controllers.Bills
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<BillsController> _logger;
        private readonly IPaymentService _paymentService;

        public BillsController(IBillRepository billRepository, ICategoryRepository categoryRepository, ILogger<BillsController> logger, IPaymentService paymentService)
        {
            _billRepository = billRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _paymentService = paymentService;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<IActionResult> GetBills(bool includeNavigationProps = false, bool includeTotalPaid = false)
        {
            var billsFromRepo = await _billRepository.GetAllDirectNav(includeNavigationProps).ToListAsync();
           
            if (includeTotalPaid)
            {
                var BillsForUser = Mapper.Map<IEnumerable<BillDtoExtension>>(billsFromRepo);
                return Ok(BillsForUser);
            }
            else
            {
                var BillsForUser = Mapper.Map<IEnumerable<BillDto>>(billsFromRepo);
                return Ok(BillsForUser);
            }
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(Guid id)
        {
            var bill = await _billRepository.GetById(id, true);

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

            if (!await _billRepository.EntryExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billFromRepo = await _billRepository.GetById(id, true);

            var categoryFromRepo = _categoryRepository.GetCategoryByName(bill.CategoryName);

            if (categoryFromRepo == null)
            {
                return BadRequest();
            }
            
            var billToUpdate = Mapper.Map(bill, billFromRepo);
            billToUpdate.BudgetCategory = categoryFromRepo;

            await _billRepository.Update(id, billToUpdate);
            
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

            var categoryFromRepo = _categoryRepository.GetCategoryByName(bill.CategoryName);

            if (categoryFromRepo == null)
            {
                return BadRequest();
            }

            var billToAdd = Mapper.Map<Bill>(bill);
            billToAdd.BudgetCategory = categoryFromRepo;

            await _billRepository.Create(billToAdd);

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            if(!await _billRepository.EntryExists(id))
            {
                return NotFound();
            }

            await _billRepository.Delete(id);

            return NoContent();
        }

        // GET: api/Bills/GetPayments/billId
        [HttpGet("[action]/{billId}")]
        public async Task<IActionResult> GetPayments(Guid billId)
        {
            if(billId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _billRepository.EntryExists(billId))
            {
                return NotFound();
            }

            var paymentsFromRepo = _billRepository.GetPayments(billId);

            var paymentsToReturn = Mapper.Map<IList<PaymentDto>>(paymentsFromRepo);

            return Ok(paymentsToReturn);
        }

        // POST: api/Bills/AddPayment/billId
        [HttpPost("[action]/{billId}")]
        public async Task<IActionResult> AddPayment(Guid billId, PaymentForCreateDto payment)
        {
            if (billId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _billRepository.EntryExists(billId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billFromRepo = await _billRepository.GetById(billId, true);

            var paymentToAdd = Mapper.Map<Payment>(payment);

            await _paymentService.AddBillPaymentAsync(billFromRepo, paymentToAdd);

            await _billRepository.Update(billId, billFromRepo);

            return CreatedAtAction("GetPayments", new { billId = billFromRepo.Id }, billFromRepo);
        }

        // PUT: api/Bills/UpdatePayment/paymentId
        [HttpPut("[action]/{paymentId}")]
        public async Task<IActionResult> UpdatePayment(Guid paymentId, PaymentForCreateDto payment)
        {
            if (paymentId == Guid.Empty)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentToUpdate = await _paymentService.GetPaymentById(paymentId);

            if (paymentToUpdate == null)
            {
                return NotFound();
            }

            Mapper.Map(payment, paymentToUpdate);

            await _paymentService.UpdatePaymentAsync(paymentId, paymentToUpdate);

            return NoContent();
        }

        // DELETE: api/Bills/DeletePayment/paymentId
        [HttpDelete("[action]/{paymentId}")]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            if(paymentId == null)
            {
                return NoContent();
            }

            await _paymentService.DeletePaymentAsync(paymentId);

            return NoContent();
        }
    }
}
