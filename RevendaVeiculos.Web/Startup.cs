using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;
using RevendaVeiculos.Message.Models;
using RevendaVeiculos.Message.Producers;
using RevendaVeiculos.Service.Services.Marcas;
using RevendaVeiculos.Service.Services.Proprietarios;
using RevendaVeiculos.Service.Services.Veiculos;
using RevendaVeiculos.Web.LocalizationResources;
using RevendaVeiculos.Web.Maps;
using System.Globalization;

namespace RevendaVeiculos.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }


        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            var cultures = new CultureInfo[]
            {
                new CultureInfo("pt-br"),
            };

            services.AddRazorPages()
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(
                    ops =>
                    {
                        ops.UseAllCultureProviders = false;
                        ops.ResourcesPath = "LocalizationResources";
                        ops.RequestLocalizationOptions = o =>
                        {
                            o.SupportedCultures = cultures;
                            o.SupportedUICultures = cultures;
                            o.DefaultRequestCulture = new RequestCulture("pt-br");
                        };
                    });

            services.AddDbContext<RevendaVeiculosContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RevendaVeiculosContext")));


            if (Environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }
            else
            {
                services.AddControllersWithViews();
            }

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitConfig"));
            services.AddScoped<INotificacaoEmailProducer, NotificacaoEmailProducer>();

            services.AddScoped<IMarcasService, MarcasService>();
            services.AddScoped<IProprietariosService, ProprietariosService>();
            services.AddScoped<IVeiculosService, VeiculosService>();

            services.AddAutoMapper(c => c.AddProfile<MapProfile>(), typeof(Startup));
            services.AddControllersWithViews();

        }

        public void Configure(IApplicationBuilder app)
        {
            DatabaseManagementService.MigrationInitialisation(app);

            app.UseRequestLocalization();

            if (!Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
