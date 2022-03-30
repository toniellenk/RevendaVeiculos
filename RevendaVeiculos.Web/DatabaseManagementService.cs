using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data;

namespace RevendaVeiculos.Web
{
    public static class DatabaseManagementService
    {

        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<RevendaVeiculosContext>().Database.Migrate();
            }
        }
    }
}
