using Blazored.Modal;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace BudgetPad.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            services.AddBlazoredModal();
            
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
