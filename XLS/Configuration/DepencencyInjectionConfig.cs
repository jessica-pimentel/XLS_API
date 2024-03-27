namespace xls_Application.Configuration
{
    public static class DepencencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                                  IConfiguration configuration)
        {
            services.AddSingleton(configuration);


            return services;
        }
    }
}
