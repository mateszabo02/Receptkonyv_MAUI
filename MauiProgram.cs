using Microsoft.Extensions.Logging;
using Receptkonyv_MAUI.Repositories;

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
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<RecipeDetailPage>();
            builder.Services.AddTransient<RecipeDetailPageViewModel>();
            builder.Services.AddTransient<EditRecipePage>();
            builder.Services.AddTransient<EditRecipePageViewModel>();
            builder.Services.AddTransient<FilterPage>();
            builder.Services.AddTransient<FilterPageViewModel>();
            builder.Services.AddSingleton<IRecipeRepository, RecipeRepository>();
            return builder.Build();
        }
    }
}
