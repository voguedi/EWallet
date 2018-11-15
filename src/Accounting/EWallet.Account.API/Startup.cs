using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EWallet.Account.API
{
    public class Startup
    {
        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Ctors

        public Startup(IConfiguration configuration) => Configuration = configuration;

        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddVoguedi(s =>
            {
                s.UseJson();
                s.UseRabbitMQ("localhost", "ewallet.accounting");
                s.UseSqlServer(@"Server=DESKTOP-GQ9I89D\MSSQLSERVER16;Database=EWallet;User Id=sa;Password=123;");
            });
            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Info { Title = "EWallet.Account.API", Version = "v1" }));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "EWallet.Account.API"));
        }

        #endregion
    }
}
