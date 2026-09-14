using NLog;
using NLog.Web;

// логгер поднимаем до создания хоста, чтобы поймать даже ошибки старта
var logger = LogManager.Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

try
{
    logger.Info("Запуск приложения");

    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Приложение остановлено из-за необработанной ошибки");
    throw;
}
finally
{
    // без этого NLog может не успеть дописать файл и держит ресурсы
    LogManager.Shutdown();
}
