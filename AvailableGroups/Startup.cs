using AutoMapper;
using AvailableGroups.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;

namespace AvailableGroups
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

            services.AddTransient<IApiService, ApiService>();
          //  services.AddHttpContextAccessor();
            services.AddHttpClient();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("AuthRoar", ConfigureAuthRoarOidcOptions());
            services.AddControllersWithViews();
            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(Startup));
        }

        private Action<OpenIdConnectOptions> ConfigureAuthRoarOidcOptions()
        {
            return options =>
            {
                options.Authority = $"https://{Configuration["AuthRoar:Domain"]}";
                options.ClientId = Configuration["AuthRoar:ClientId"];
                options.ClientSecret = Configuration["AuthRoar:ClientSecret"];

                options.ResponseType = OpenIdConnectResponseType.Code;
                options.UsePkce = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");

                // Automatically store the resulting access and refresh token in the authentication session
                options.SaveTokens = true;

                // Set the callback path, it is mandatory as backend API setting
                options.CallbackPath = new PathString("/callback");

                // The rest settings are optional, need check with API document for details
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimsIssuer = "AuthRoar";
                options.SignedOutRedirectUri = new PathString("/");
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ----- Not inculding HTTPS for this development purpose project
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
                      
            app.UseAuthentication();
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
