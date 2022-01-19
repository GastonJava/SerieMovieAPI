using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SerieMovieAPI.Configuration;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Data;
using SerieMovieAPI.Miscelanious;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs;
using SerieMovieAPI.Models.DTOs.User;
using SerieMovieAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace SerieMovieAPI
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

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            //TODO
            services.AddDbContext<SerieMovieDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("alkemyconexion")));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });


            // IdentityUser 

            /* services.AddDefaultIdentity<IdentityUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SerieMovieDBContext>(); */



            services.AddIdentityCore<CustomUserIdentity>(opt => opt.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SerieMovieDBContext>();

            // agregamos el unit of work para las Inyecion de contenedores
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //guardar la imagen en azure blob
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration.GetSection("Storage:ConnectionStringb").Value);
            });

            // agregamosservicio blob
            services.AddTransient<IStorageService, StorageServices>();

            //automapper dtos
            //services.AddAutoMapper(typeof(Startup));
            var mappinconf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappinconf.CreateMapper();
            services.AddSingleton(mapper);


            services.AddControllers(
                opt =>
                {
                    opt.SuppressAsyncSuffixInActionNames = false;
                }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler
            = ReferenceHandler.Preserve); /* .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler
            = ReferenceHandler.Preserve) */

            //NEWTONJSON 
            var json = JsonConvert.SerializeObject(Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

            services.AddSwaggerGen(c =>
            {
                //c.OperationFilter<AddCommonParameOperationFilter>();
                //c.SchemaFilter<SwaggerSkipPropertyFilter>();
                c.EnableAnnotations();
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SerieMovieAPI",
                        Version = "v1",
                        Description = "An API to Perform movies and series operations",
                        Contact = new OpenApiContact
                        {
                            Name = "Ponce Gaston",
                            Email = "gastonmirandajava@gmail.com",
                            Url = new Uri("http://gastonaledev.xyz")
                        }

                    });

                //c.OperationFilter<SwaggerJsonIgnore>();

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlpath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()

                  }

                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SerieMovieAPI v1"));
            }


            // Usar file server para acceder sin wwwroot en WEBAPI
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles/images/Character",
                EnableDefaultFiles = true
            });


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "StaticFiles")),
                RequestPath = "/StaticFiles/images/character",
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
