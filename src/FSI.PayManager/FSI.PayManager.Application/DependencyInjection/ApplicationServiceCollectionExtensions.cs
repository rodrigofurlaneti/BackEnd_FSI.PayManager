using Microsoft.Extensions.DependencyInjection;
using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using FSI.PayManager.Application.Services;

namespace FSI.PayManager.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICrudAppService<UserDto>, UserAppService>();
            services.AddScoped<ICrudAppService<WalletDto>, WalletAppService>();
            services.AddScoped<ICrudAppService<CategoryDto>, CategoryAppService>();
            services.AddScoped<ICrudAppService<RecurringTransactionDto>, RecurringTransactionAppService>();
            services.AddScoped<ICrudAppService<FinancialTransactionDto>, FinancialTransactionAppService>();
            services.AddScoped<ICrudAppService<ReminderDto>, ReminderAppService>();
            services.AddScoped<IAuthAppService, AuthAppService>();
            return services;
        }
    }
}
