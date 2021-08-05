using Airline.BL.Business;
using Airline.BL.IBusiness;
using Airline.DL.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Airline.DL.IRepository;
using Airline.DL.Repositories;
using ValidateToken.Filter;
namespace Airline.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            //services.AddMvc()
            //      .AddMvcOptions(options =>
            //      {
            //          options.Filters.Add(new AuthorizationFilter());
            //      });
            services.AddCors();
            services.AddTransient<IAirlineBL,AirlinesBL>();
            services.AddTransient<IAirlineRepository,AirlinesRepository>();
            services.AddTransient<IFlightBL, FlightBusinessLogic>();
            services.AddTransient<IFlight, FlightRepository>();

            services.AddDbContext<AirlinesDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"),
              b => b.MigrationsAssembly("Airline.DL")));

           

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Airlines Service API",
                    Version = "v2",
                    Description = "Flight Application",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Airline Service"));
        }
    }
}
