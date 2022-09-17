using SmsService.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Configurator.InjectServices(builder.Services,builder.Configuration);
var app = builder.Build();
Configurator.ConfigPipeLines(app);
app.Run();
