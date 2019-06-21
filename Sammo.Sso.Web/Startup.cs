using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Sammo.Sso.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace Sammo.Sso.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<SsoDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = "Sso";
                options.ClientSecret = "Secret";
                options.Authority = "https://oidc.faasx.com/";
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                //是否将Tokens保存到AuthenticationProperties中
                options.SaveTokens = true;
                //是否从UserInfoEndpoint获取Claims
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters.NameClaimType = "name";

                /***********************************相关事件***********************************/
                // 未授权时，重定向到OIDC服务器时触发
                //options.Events.OnRedirectToIdentityProvider = context => Task.CompletedTask;

                // 获取到授权码时触发
                //options.Events.OnAuthorizationCodeReceived = context => Task.CompletedTask;
                // 接收到OIDC服务器返回的认证信息（包含Code, ID Token等）时触发
                //options.Events.OnMessageReceived = context => Task.CompletedTask;
                // 接收到TokenEndpoint返回的信息时触发
                //options.Events.OnTokenResponseReceived = context => Task.CompletedTask;
                // 验证Token时触发
                //options.Events.OnTokenValidated = context => Task.CompletedTask;
                // 接收到UserInfoEndpoint返回的信息时触发
                //options.Events.OnUserInformationReceived = context => Task.CompletedTask;
                // 出现异常时触发
                //options.Events.OnAuthenticationFailed = context => Task.CompletedTask;

                // 退出时，重定向到OIDC服务器时触发
                //options.Events.OnRedirectToIdentityProviderForSignOut = context => Task.CompletedTask;
                // OIDC服务器退出后，服务端回调时触发
                //options.Events.OnRemoteSignOut = context => Task.CompletedTask;
                // OIDC服务器退出后，客户端重定向时触发
                //options.Events.OnSignedOutCallbackRedirect = context => Task.CompletedTask;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
