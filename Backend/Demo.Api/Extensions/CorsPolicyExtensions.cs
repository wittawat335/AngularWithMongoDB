using Demo.Domain.Utilities;

namespace Demo.Api.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            //var url = configuration.GetSection(Constants.AppSettings.Client_URL).Value;
            //var CorsPolicy = configuration.GetSection(Constants.AppSettings.CorsPolicy).Value;
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy(CorsPolicy, builder =>
            //    {
            //        builder.WithOrigins(url).AllowAnyHeader().AllowAnyMethod();
            //    });
            //});

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }
    }
}
