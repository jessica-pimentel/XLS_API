using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Extensions.Mail;

namespace xls_Domain.Configuration
{
    public static class MailConfig
    {
        public static IServiceCollection AddMailServiceConfiguration(this IServiceCollection services,
                                                                                   IConfiguration configuration)
        {
            var mailServiceSettings = configuration.GetSection("MailServiceSettings");
            services.Configure<MailServiceSettings>(mailServiceSettings);

            return services;
        }
    }
}
