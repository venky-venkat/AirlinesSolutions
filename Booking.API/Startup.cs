using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Booking.DL.DBContext;
using Microsoft.EntityFrameworkCore;
using Booking.BL.Business;
using Booking.BL.IBusiness;
using Booking.DL.IRepository;
using Booking.DL.Repository;
using ValidateToken.Filter;
namespace Booking.API
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
            services.AddMvc()
                 .AddMvcOptions(options =>
                 {
                     options.Filters.Add(new AuthorizationFilter());
                 });
            services.AddTransient<IBookingBL,BookingBusiness>();
            services.AddTransient<IBookingDL,BookingRepository>();
            
            services.AddDbContext<BookingDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"),
              b => b.MigrationsAssembly("Booking.DL")));

            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Booking Service API",
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Booking Service"));
          
        }
    }
}
