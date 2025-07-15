using Microsoft.Extensions.Logging;

namespace TrivialEuropeo
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
                    fonts.AddFont("Winter Bubble.otf", "WinterBubble");
                    fonts.AddFont("Acadian.ttf", "Acadian");
                    fonts.AddFont("A.C.M.E. Secret Agent.ttf", "Acme");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}