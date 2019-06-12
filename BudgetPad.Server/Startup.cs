using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using BudgetPad.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using BudgetPad.Shared.Services.BudgetFunds;
using BudgetPad.Server.CoreServices.Expense;
using BudgetPad.Server.CoreServices.Mappers;

namespace BudgetPad.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddDbContext<BudgetPadContext>(options =>
                options.UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BudgetPadDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddSingleton<IMapperService, MapperService>();
            services.AddSingleton<ICalculateBudgetFundsService, CalculateBudgetFundsService>();
            services.AddSingleton<IFundsInCategoryService, FundsInCategoryService>();
            services.AddScoped<IExpenseLoggerService, ExpenseLoggerService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapperService mapperService)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            mapperService.InitializeMapper();

            app.UseBlazor<Client.Startup>();
        }
    }
}
