using BudgetPad.Shared;
using BudgetPad.Shared.Dtos;
using AutoMapper;
using BudgetPad.Shared.Dtos.DtoExtensions;
using BudgetPad.Shared.Services.BudgetFunds;

namespace BudgetPad.Server.CoreServices.Mappers
{
    public class MapperService : IMapperService
    {
        private readonly IFundsInCategoryService _fundsInCategoryService;

        public MapperService(IFundsInCategoryService fundsInCategoryService)
        {
            _fundsInCategoryService = fundsInCategoryService;
        }

        public void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                MapCategory(cfg);
                MapBill(cfg);
                MapUnplannedExpense(cfg);
                MapBudget(cfg);
                MapPayment(cfg);
                MapExpenses(cfg);
            });
        }

        private IMapperConfigurationExpression MapCategory(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BudgetCategoryDto, BudgetCategory>();

            cfg.CreateMap<BudgetCategoryDto, BudgetCategory>().ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapBill(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BillDto, Bill>();
            cfg.CreateMap<BillDto, Bill>().ReverseMap();

            cfg.CreateMap<BillForCreateDto, Bill>();
            cfg.CreateMap<BillForCreateDto, Bill>().ReverseMap();

            cfg.CreateMap<BillDtoExtension, Bill>();
            cfg.CreateMap<BillDtoExtension, Bill>().ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapUnplannedExpense(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UnplannedExpenseDto, UnplannedExpense>();
            cfg.CreateMap<UnplannedExpenseDto, UnplannedExpense>().ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapBudget(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BudgetDto, Budget>();
            cfg.CreateMap<BudgetDto, Budget>()
                .ReverseMap();

            cfg.CreateMap<BudgetForCreateDto, Budget>();
            cfg.CreateMap<BudgetForCreateDto, Budget>().ReverseMap();

            cfg.CreateMap<BudgetDtoExtension, Budget>();


            cfg.CreateMap<BudgetDtoExtension, Budget>().ReverseMap()
                .BeforeMap((s, d) => d.CategoryGroups = _fundsInCategoryService.GroupExpensesByCat(s.Bills));

            return cfg;
        }

        private IMapperConfigurationExpression MapPayment(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PaymentDto, Payment>();
            cfg.CreateMap<PaymentDto, Payment>().ReverseMap();

            cfg.CreateMap<PaymentForCreateDto, Payment>();
            cfg.CreateMap<PaymentForCreateDto, Payment>().ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapExpenses(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Bill, ExpenseLogEntry>();
            cfg.CreateMap<Bill, ExpenseLogEntry>().ReverseMap();

            cfg.CreateMap<UnplannedExpense, ExpenseLogEntry>();
            cfg.CreateMap<UnplannedExpense, ExpenseLogEntry>().ReverseMap();

            return cfg;
        }
    }
}
