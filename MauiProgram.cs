using Microsoft.Extensions.Logging;

namespace Receptkonyv_MAUI
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
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<RecipeDetailPage>();
            builder.Services.AddTransient<RecipeDetailPageViewModel>();
            builder.Services.AddTransient<EditRecipePage>();
            builder.Services.AddTransient<EditRecipePageViewModel>();
            builder.Services.AddTransient<FilterPage>();
            builder.Services.AddTransient<FilterPageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
