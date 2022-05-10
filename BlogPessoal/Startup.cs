using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using BlogPessoal.src.services;
using BlogPessoal.src.services.implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace BlogPessoal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Database settings
            services.AddDbContext<BlogPessoalContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Repositories settings
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ITheme, ThemeRepository>();
            services.AddScoped<IPosting, PostingRepository>();

            // Controllers settings
            services.AddCors();
            services.AddControllers();


            // Services settings
            services.AddScoped<IAuthentication, AuthenticationServices>();

            // JWTBearer Authentication Token settings
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(b =>
                {
                    b.RequireHttpsMetadata = false;
                    b.SaveToken = true;
                    b.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlogPessoalContext context)
        {
            //Development environment
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

            // Production Environment
            // Routes
            app.UseRouting();

            app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

