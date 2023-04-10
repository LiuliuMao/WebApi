using Common;
using Common.Helper;
using Common.Cache;
using IRepository;
using IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Auth;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Service.Permissions;
using Model.Enums;
using Microsoft.AspNetCore.Authorization;

namespace WebApi
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
            services.AddControllers();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                //c.OperationFilter<AddResponseHeadersFilter>();
                //c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme() { 
                //Description="请在输入时添加Bearer和一个空格",
                //Name= "Authorization",
                //In=ParameterLocation.Header,
                //Type=SecuritySchemeType.ApiKey
                //});
            });

            #endregion

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            #region JWT
            services.Configure<AuthSetting>(Configuration.GetSection("tokenConfig"));

            var token = Configuration.GetSection("tokenConfig").Get<AuthSetting>();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("TwoFactor", policy => policy.Requirements.Add(new TwoFactorRequirement()));

            //    options.AddPolicy("Supervisor", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Supervisor)));

            //    options.AddPolicy("Manager", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Manager)));

            //    options.AddPolicy("Director", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Director)));

            //    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new CoreRequirement(LevelEnum.Beginner)).Build();
            //});
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //Token Validation Parameters
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                    GetBytes(token.Secret)),
                    //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。
                    ValidIssuer = token.Issuer,
                    //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddScoped<IAuthServer, AuthServer>();
            #endregion

            //AddAssembly(services, "IService");
            //AddAssembly(services, "Service");
            //注册DbContext
            services.AddDbContext<SqlServerDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddScoped<IDBContextFactory, RepositoryFactory>();//泛型工厂
            services.AddScoped<ISqlServerDBContext, SqlServerDBContext>();//db
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddSingleton<IRedisCache, RedisCache>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>  
        /// 自动注册服务——获取程序集中的实现类对应的多个接口
        /// </summary>
        /// <param name="services">服务集合</param>  
        /// <param name="assemblyName">程序集名称</param>
        public void AddAssembly(IServiceCollection services, string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType).ToList();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    if (interfaceType.Length == 1)
                    {
                        services.AddTransient(interfaceType[0], item);
                    }
                    if (interfaceType.Length > 1)
                    {
                        services.AddTransient(interfaceType[1], item);
                    }
                }
            }
        }

    }
}
