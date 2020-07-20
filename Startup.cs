using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// using notificationsWebApi.Data;
using Newtonsoft.Json;
using SGCont.Data;
using SGCont.Models;
using SGCont.Utils;
using Swashbuckle.AspNetCore.Swagger;

[assembly : HostingStartup (typeof (SGCont.Startup))]
namespace SGCont {
    public class Startup : IHostingStartup {
        public void Configure (IWebHostBuilder builder) {

            builder.ConfigureServices (ConfigureServices);
            builder.Configure (Configure);
            builder.Configure (Configure);
        }

        public void ConfigureServices (WebHostBuilderContext context, IServiceCollection services) {
            services.AddDbContext<SGContDbContext> (options =>
                options.UseNpgsql (context.Configuration.GetConnectionString ("DefaultConnection"), b => b.MigrationsAssembly ("SGCont")));

            services.AddSignalR ();

            services.AddIdentity<Usuario, IdentityRole> ()
                .AddEntityFrameworkStores<SGContDbContext> ()
                .AddDefaultTokenProviders ();

            services.AddTransient<SGContDbContext> ();
            services.AddAuthentication (options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://microapp.cu",
                ValidAudience = "https://microapp.cu",
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes ("Admin123*1234567890"))
                };
            });

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v1",
                        Title = "SGCont API",
                        Description = "Sistema de Gestión de Contratos",
                        TermsOfService = new Uri ("https://example.com/terms"),
                        Contact = new OpenApiContact {
                            Name = "Alejandro Román Franco",
                                Email = "alejoroman0605@gmail.com",
                                Url = new Uri ("https://alejo.com/")
                        }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });

            services.AddTransient<SGContDbContext> ();
            services.AddSingleton<MenuLoader> ();
            services.AddSingleton<LicenciaService> ();

            services.AddSpaStaticFiles (config => {
                config.RootPath = context.Configuration.GetValue<string> ("ClientApp");
            });
            services.AddCors ();
            var mvcBuilder = services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2).AddJsonOptions (options => {
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            var lista = new string[] {
                "SGCont"
            };
            var asseblies = AppDomain.CurrentDomain.GetAssemblies ().Where (c => lista.Contains (c.FullName));
            foreach (var assembly in asseblies) {
                mvcBuilder.AddApplicationPart (assembly);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app) {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment> ();
            var config = app.ApplicationServices.GetRequiredService<IConfiguration> ();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory> ();

            loggerFactory.AddFile ("Logs/Log-{Date}.txt");

            app.UseAuthentication ();
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseSwagger (c => c.RouteTemplate = "docs/{documentName}/docs.json");
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/docs/v1/docs.json", "SGCont API v1");
                c.RoutePrefix = "docs";
            });
            // app.UseSignalR (routes => {
            //     routes.MapHub<NotificationsHub> ("/notihub");
            // });
            app.UseCors (build => build.AllowAnyOrigin ().AllowAnyHeader ().AllowAnyMethod ());
            app.UseMvc (builder => {
                builder.MapRoute (
                    name: "default",
                    template: "admin/{controller=Home}/{action=Index}/{id?}");

            });
            app.UseSpaStaticFiles ();
            app.Map ("/home", conf => {
                conf.UseSpa (builder => {
                    builder.Options.SourcePath = config.GetValue<string> ("ClientApp");

                });
            });
            // app.UseSpa(builder =>
            // {
            //     builder.Options.SourcePath = "../contratacion-vue/dist";

            // });
        }
    }
}