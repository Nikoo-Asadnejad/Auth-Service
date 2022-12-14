using AuthService.Configurations.AppSettings;
using AuthService.Data;
using AuthService.DataAccess.Repository;
using AuthService.DataAccess.Repository.Interfaces;
using AuthService.DataAccess.Repository.Repositories;
using AuthService.Interfaces;
using AuthService.Services;
using AuthService.Utils;
using ErrorHandlingDll.Configurations;
using GenericRepositoryDll.Configuration;
using HttpService.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SmsService.Configurations
{
  public static class Configurator
  {


    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {

      services.AddControllers();
      services.AddEndpointsApiExplorer();

      services.AddSwaggerGen(c =>
      {
        var filePath = Path.Combine(AppContext.BaseDirectory, "AuthService.xml");
        c.IncludeXmlComments(filePath);
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
         {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          } });

      });

      services.Configure<AppSetting>(configuration);

      services.AddAuthentication();
 

      var connection = configuration.GetConnectionString("SQLServer");
      services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connection));
      services.AddScoped<DbContext, AuthDbContext>();

      ErrorHandlingDllConfigurator.InjectServices(services, configuration);
      HttpServiceConfigurator.InjectHttpService(services);
      GenericRepositoryConfigurator.InjectServices(services);

      services.AddScoped<IAuthService, AuthService.Services.AuthService>();
      services.AddScoped<ISmsService, AuthService.Services.SmsService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IOptCodeService, OptCodeService>();
      services.AddScoped<IUserTokenService, UserTokenService>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddSingleton<IJwtTokenTools, JwtTokenTools>();

      services.AddAuthentication(options =>
       {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
       })
        .AddJwtBearer(o =>
       {
         o.TokenValidationParameters = new TokenValidationParameters
         {
           ValidIssuer = configuration["Jwt:Issuer"],
           ValidAudience = configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey
             (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = false,
           AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
           ValidateIssuerSigningKey = true
         };
       });

    }

    public static void ConfigPipeLines(WebApplication app)
    {

      //  ErrorHandlingDllConfigurator.ConfigureAppPipeline(app);
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
      app.MapControllers();



      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth-Service API's");
        });
      }


    }


  }

}
