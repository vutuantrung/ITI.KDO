﻿using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentication;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ITI.KDO.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.Configure<TokenProviderOptions>(o =>
            {
                o.Audience = Configuration["JwtBearer:Audience"];
                o.Issuer = Configuration["JwtBearer:Issuer"];
                o.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            // Add framework services.
            services.AddMvc();
            services.AddOptions();
            services.AddSingleton(_ => new UserGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new EventGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new PresentGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new ParticipantGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new ParticipationGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new CategoryPresentGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new ContactGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new FacebookContactGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton(_ => new ItemQuantityGateway(Configuration["ConnectionStrings:KDODB"]));
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<UserServices>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<PresentServices>();
            services.AddSingleton<QuantityServices>();
            services.AddSingleton<ContactServices>();
            services.AddSingleton<ParticipantServices>();
            services.AddSingleton<ParticipationServices>();
            services.AddSingleton<NotificationServices>();
            services.AddSingleton<EventServices>();
            services.AddSingleton<CategoryPresentServices>();
            services.AddSingleton<FacebookServices>();
            services.AddSingleton<FacebookClient>();
            services.AddSingleton<FileServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string secretKey = Configuration["JwtBearer:SigningKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AuthenticationScheme = JwtBearerAuthentication.AuthenticationScheme,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtBearer:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtBearer:Audience"],

                    NameClaimType = ClaimTypes.Email,
                    AuthenticationType = JwtBearerAuthentication.AuthenticationType
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthentication.AuthenticationScheme
            });
            
            //Google Login OAuth
            ExternalAuthenticationEvents googleAuthenticationEvents = new ExternalAuthenticationEvents(
                new GoogleExternalAuthenticationManager(app.ApplicationServices.GetRequiredService<UserServices>()));

            app.UseGoogleAuthentication(c =>
            {
                c.SignInScheme = CookieAuthentication.AuthenticationScheme;
                c.ClientId = Configuration["Authentication:Google:ClientId"];
                c.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                c.Events = new OAuthEvents
                {
                    OnCreatingTicket = googleAuthenticationEvents.OnCreatingTicket
                };
                c.AccessType = "offline";
            });

            //Facebook Login OAuth
            ExternalAuthenticationEvents facebookAuthenticationEvents = new ExternalAuthenticationEvents(
                new FacebookExternalAuthenticationManager(
                    app.ApplicationServices.GetRequiredService<UserServices>(), 
                    app.ApplicationServices.GetRequiredService<FacebookServices>())
                    );

            app.UseFacebookAuthentication(c =>
            {
                c.SignInScheme = CookieAuthentication.AuthenticationScheme;
                c.ClientId = Configuration["Authentication:Facebook:AppId"];
                c.ClientSecret = Configuration["Authentication:Facebook:AppSecret"];
                c.Events = new OAuthEvents
                {
                    OnCreatingTicket = facebookAuthenticationEvents.OnCreatingTicket
                };
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "Home/{*anything}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseStaticFiles();
        }
    }
}
