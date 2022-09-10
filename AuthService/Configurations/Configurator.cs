using AuthService.Configurations.AppSettings;
using AuthService.Data;
using AuthService.Interfaces;
using AuthService.Services;
using ErrorHandlingDll.Configurations;
using GenericRepositoryDll.Configuration;
using HttpService.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


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
      });

      services.Configure<AppSetting>(configuration);

      var connection = configuration.GetConnectionString("SQLServer");
      services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connection));

      ErrorHandlingDllConfigurator.InjectServices(services, configuration);
      HttpServiceConfigurator.InjectHttpService(services);
      GenericRepositoryConfigurator.InjectServices(services);

      services.AddScoped<IAuthService, AuthService.Services.AuthService>();
      services.AddScoped<ISmsService, AuthService.Services.SmsService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IOptCodeService, OptCodeService>();

    }

    public static void ConfigPipeLines(WebApplication app)
    {

    //  ErrorHandlingDllConfigurator.ConfigureAppPipeline(app);
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
