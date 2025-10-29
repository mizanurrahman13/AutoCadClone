using Microsoft.Extensions.Logging;
using AutoCADClone.Application.Features.DrawLine;

namespace AutoCADClone.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DrawLineHandler).Assembly));
            builder.Services.AddSingleton<CanvasService>();

            return builder.Build();
        }
    }
}
