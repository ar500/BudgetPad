using System.Linq;

namespace BudgetPad.Shared.Dtos.DtoExtensions
{
    public class BillDtoExtension : BillDto
    {
        public decimal TotalPaid
        {
            get
            {
                if(this.Payments.Count() == 0)
                {
                    return 0;
                }
                
                return this.Payments.Select(p => p.AmountPaid).Sum(); }
        }
    }
}
