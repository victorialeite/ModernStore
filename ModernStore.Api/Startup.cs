using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModernStore.Api.Security;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Services;
using ModernStore.Infra.DataContexts;
using ModernStore.Infra.Repositories;
using ModernStore.Infra.Services;
using ModernStore.Infra.Transactions;
using ModernStore.Shared;

namespace ModernStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        private const string ISSUER = "c1f6if48";
        private const string AUDIENCE = "b5cccc645927";
        private const string SECRET_KEY = "1bc9413e-a7b8-4104-9081-8ddbe0b6d6cc";

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();

            // Policy - contrato de autorização
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim("ModernStore", "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("ModernStore", "Admin"));
            });

            // Configurando Autenticação
            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                ValidateAudience = true,
                ValidAudience = AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            // Resolvendo as dependências
            services.AddScoped<ModernStoreDataContext, ModernStoreDataContext>();
            services.AddTransient<IUow, Uow>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerCommandHandler, CustomerCommandHandler>();
            services.AddTransient<OrderCommandHandler, OrderCommandHandler>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
            app.UseMvc();

            app.UseAuthentication();

            Runtime.ConnectionString = Configuration.GetConnectionString("CnnStr");
        }
    }
}
