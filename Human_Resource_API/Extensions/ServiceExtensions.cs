namespace Human_Resource_API.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
            option.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.WithHeaders("accept", "content-type");
                builder.WithMethods("POST", "GET", "PUT", "DELETE", "PATCH");

                });
            });
        }


    }
}
