using MauiProj.Models;
using MauiProj.Services;
using MauiProj.Views;
using Microsoft.Extensions.Logging;

namespace MauiProj
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
            // Dependency Injection
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ProductsViewModel>();
            builder.Services.AddSingleton<ProductsPage>();
            return builder.Build();
        }
    }
}
