using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Sammo.Sso.Domain.Core.Bus;
using Sammo.Sso.Domain.Entities;
using Sammo.Sso.Domain.EventHandlers;
using Sammo.Sso.Domain.Events.Values;
using Sammo.Sso.Domain.Interfaces;
using Sammo.Sso.Infrastructure.Bus;
using Sammo.Sso.Infrastructure.Data.Context;
using Sammo.Sso.Infrastructure.Data.Repositories;
using Sammo.Sso.Infrastructure.Identity.Services;
using Sammo.Sso.Web.Filters;
using System.Text;
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

            //添加Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SSO API", Version = "v1" });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelErrorFilter));
                options.Filters.Add(typeof(ExceptionErrorFilter));
                options.Filters.Add(typeof(GlobalAccessLogFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddAuthentication(s =>
            {
                //添加JWT Scheme
                s.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,//是否验证Issuer
                    ValidateAudience = false,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = "SSO",//Audience
                    ValidIssuer = "SSO",//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Secret").Value))//拿到SecurityKey
                };
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
            RegisterServices(services);
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
            app.UseMiddleware<CorsMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSO API V1");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            //注入DbContext
            services.AddDbContext<SsoDbContext>();

            //注入Repository
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Domain.Entities.Application>, Repository<Domain.Entities.Application>>();

            //注入基础设施层-Identity
            services.AddScoped<IdentityService>();

            //引用包:MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            //命令总线Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //领域通知
            //services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //领域事件
            services.AddScoped<INotificationHandler<ValueChangedEvent>, ValueEventHandler>();

            services.AddMemoryCache();
        }
    }
}
