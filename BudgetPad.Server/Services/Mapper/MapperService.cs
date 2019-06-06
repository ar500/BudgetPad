using BudgetPad.Shared;
using BudgetPad.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetPad.Server.Services.Mapper
{
    public class MapperService : IMapperService
    {
        public void InitializeMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                MapCategory(cfg);
                MapBill(cfg);
                MapExpenseBase(cfg);
                MapBudget(cfg);
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
            cfg.CreateMap<BillDto, Bill>()
                .IncludeBase<ExpenseBaseDto, ExpenseBase>();

            cfg.CreateMap<BillDto, Bill>()
                .IncludeBase<ExpenseBaseDto, ExpenseBase>().ReverseMap();

            cfg.CreateMap<BillForCreateDto, Bill>()
                .IncludeBase<ExpenseBaseForCreateDto, ExpenseBase>();

            cfg.CreateMap<BillForCreateDto, Bill>()
                .IncludeBase<ExpenseBaseForCreateDto, ExpenseBase>()
                .ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapExpenseBase(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ExpenseBaseForCreateDto, ExpenseBase>();

            cfg.CreateMap<ExpenseBaseForCreateDto, ExpenseBase>().ReverseMap();

            cfg.CreateMap<ExpenseBaseDto, ExpenseBase>();
            cfg.CreateMap<ExpenseBaseDto, ExpenseBase>().ReverseMap();

            return cfg;
        }

        private IMapperConfigurationExpression MapBudget(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BudgetDto, Budget>();
            cfg.CreateMap<BudgetDto, Budget>()
                .ReverseMap();

            cfg.CreateMap<BudgetForCreateDto, Budget>();
            cfg.CreateMap<BudgetForCreateDto, Budget>().ReverseMap();

            return cfg;
        }
    }
}
