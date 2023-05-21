using eAgenda.Webapi.Filters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace eAgenda.Webapi.Config
{
    public static class FiltersConfig
    {
        public static void ConfigurarFiltros(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidarViewModelActionFilter());
            })

            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                options.JsonSerializerOptions.Converters.Add(new GuidToStringConverter());
            }
            );
        }
    }
}
