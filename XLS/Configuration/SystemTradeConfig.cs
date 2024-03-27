using xls_Domain.Extensions;

namespace xls_Application.Configuration
{
    public static class SystemTradeConfig
    {
        public static IServiceCollection AddTradeSettings(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            var systemTradeSettings = configuration.GetSection("SystemTradeSettings");
            services.Configure<XlsTradeSettings>(systemTradeSettings);

            return services;
        }
    }
}
