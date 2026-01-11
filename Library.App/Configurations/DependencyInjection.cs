using Library.Domain.Intefaces.Repositories;
using Library.Domain.Intefaces.Services;
using Library.Domain.Services;
using Library.Infra.Repositories;

namespace Library.App.Configurations
{
    public static class DependencyInjection
    {
        public static void AddConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBookReposistory, BookRepository>();

            services.AddScoped<IBookService, BookService>();
        }
    }
}
