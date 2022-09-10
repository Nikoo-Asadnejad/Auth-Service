namespace AuthService.Configurations.AppSettings;
  public class AppSetting
  {

    public Logging Logging { get; set; }
    public Sentry Sentry { get; set; }
    public string AllowedHosts { get; set; }
    public ConnectionStrings connectionStrings { get; set; }

  }

  public class Logging
  {
    public Loglevel LogLevel { get; set; }
  }

  public class Sentry
  {
    public string Dsn { get; set; }

  }

  public class ConnectionStrings
  {
    public string SQLServer { get; set; }

  }
  public class Loglevel
  {
    public string Default { get; set; }
    public string MicrosoftAspNetCore { get; set; }
  }



