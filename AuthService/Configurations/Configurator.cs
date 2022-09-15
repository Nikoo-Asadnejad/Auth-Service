using AuthService.Configurations.AppSettings;
using AuthService.Data;
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

    public static void ConfigureApp(IServiceCollection services, IConfiguration configuration , WebApplication app)
    {
      InjectServices(services, configuration);
      ConfigPipeLines(app);
    }
    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {

      services.AddControllers();
      services.AddEndpointsApiExplorer();

      services.AddSwaggerGen(c =>
      {
        var filePath = Path.Combine(AppContext.BaseDirectory, "AuthService.xml");
        c.IncludeXmlComments(filePath);
      });

      services.Configure<AppSetting>(configuration);

      services.AddAuthentication();

      var connection = configuration.GetConnectionString("SQLServer");
      services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connection));

      ErrorHandlingDllConfigurator.InjectServices(services, configuration);
      HttpServiceConfigurator.InjectHttpService(services);
      GenericRepositoryConfigurator.InjectServices(services);

      services.AddScoped<IAuthService, AuthService.Services.AuthService>();
      services.AddScoped<ISmsService, AuthService.Services.SmsService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IOptCodeService, OptCodeService>();
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
      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
      app.MapControllers();
      


      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth-Service API's");
        });
      }

      app.Run();
    }


  }

}
