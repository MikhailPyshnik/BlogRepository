using AutoMapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BlogApi.Autentification;
using DataBase.Context;
using DataBase.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Models.Blog;
using Services.BlogService;
using Services.CommentService;
using Microsoft.AspNetCore.Http;
using BlogApi.ErrorMiddleWare;
using BlogApi.Api.Simple_API_for_Authentication;
using System.Threading.Tasks;

namespace BlogApi
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
            services.Configure<ApplicationSetting>(options =>
            {
                options.ConnectionString = Configuration.GetSection("BlogPostingDBSettings:ConnectionString").Value;
                options.DatabaseName = Configuration.GetSection("BlogPostingDBSettings:DatabaseName").Value;
            });

            services.AddScoped<IRepository<Blog>, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<Api.Simple_API_for_Authentication.IUserRepository, Api.Simple_API_for_Authentication.UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddSingleton<ApplicationContext>();

            //*****

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userName = context.Principal.Identity.Name;
                        var user = userService.GetById(userName);
                        //var userId = int.Parse(context.Principal.Identity.Name);
                        //var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //****

            //services.AddIdentityMongoDbProvider<ApplicationUser, AutRole>(identityOptions =>
            //{
            //    identityOptions.Password.RequiredLength = 6;
            //    identityOptions.Password.RequireLowercase = true;
            //    identityOptions.Password.RequireUppercase = true;
            //    identityOptions.Password.RequireNonAlphanumeric = true;
            //    identityOptions.Password.RequireDigit = true;
            //}, options =>
            //{
            //    options.ConnectionString = "mongodb://localhost:27017/BlogPostingDB";
            //});


            // Add Jwt Authentication
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            //services.AddAuthentication(options =>
            //{
            //    //Set default Authentication Schema as Bearer
            //    options.DefaultAuthenticateScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(cfg =>
            //{
            //    cfg.RequireHttpsMetadata = false;
            //    cfg.SaveToken = true;
            //    cfg.TokenValidationParameters =
            //           new TokenValidationParameters
            //           {
            //               ValidIssuer = Configuration["JwtIssuer"],
            //               ValidAudience = Configuration["JwtIssuer"],
            //               IssuerSigningKey =
            //            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
            //               ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //           };
            //});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddAutoMapper(typeof(BlogServiceMapping), typeof(CommentServiceMapping), typeof(AutoMapperProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage(); ÷òîáû ðàáîòàë MiddleWare
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(x => x
            //   .AllowAnyOrigin()
            //   .AllowAnyMethod()
            //   .AllowAnyHeader());


            app.UseAuthentication();    // àóòåíòèôèêàöèÿ
            app.UseAuthorization();     // àâòîðèçàöèÿ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}