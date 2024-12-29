using MAUI.Services;
using MAUI.Views;
using MAUI.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;


namespace MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder

                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ITaskService, TaskService>();

            builder.Services.AddSingleton<TaskListViewModel>();
            builder.Services.AddSingleton<TaskListPage>();

            builder.Services.AddTransient<TaskInfoViewModel>();
            builder.Services.AddTransient<TaskInfoPage>();

            builder.Services.AddTransient<TaskCreateViewModel>();
            builder.Services.AddTransient<TaskCreatePage>();

            return builder.Build();
        }
    }
}
