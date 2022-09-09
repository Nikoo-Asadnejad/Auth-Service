using AuthService.Configurations.AppSettings;
using ErrorHandlingDll.Configurations;
using GenericRepositoryDll.Configuration;
using HttpService.Configuration;
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

      ErrorHandlingDllConfigurator.InjectServices(services, configuration);
      HttpServiceConfigurator.InjectHttpService(services);
      GenericRepositoryConfigurator.InjectServices(services);

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
