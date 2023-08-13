using Frontend.Core.AppSettings;
using Frontend.Core.Interfaces;
using Frontend.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Frontend.Core
{
    public static class RegisterService
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppSetting, AppSetting>();
            services.AddTransient(typeof(IBaseApiService<>), typeof(BaseApiService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigSession(this IServiceCollection services, IConfiguration configuration)
        {
            var timeOut = configuration.GetValue<int>("AppSettings:SessionTimeOut");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Home/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(timeOut);

            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AspNetCore.Identity.Application";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(timeOut);
                options.SlidingExpiration = true;
            }); // test
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Session
            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(15);
            });
        }

        public static void AddHttpClientService(this IServiceCollection services, IConfiguration configuration) // ยังไม่ได้ใช้
        {
            services
                .AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri(configuration.GetSection("AppSettings:BaseApiUrl").Value));
        }
    }
}
