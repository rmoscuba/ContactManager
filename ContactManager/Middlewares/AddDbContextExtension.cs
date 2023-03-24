using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Middlewares
{
    public static class AddDbContextExtension
    {
        public static object Configuration { get; private set; }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Contexts.ContactsContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("ContactsConn"))
            );

            return services;
        }
    }
}
