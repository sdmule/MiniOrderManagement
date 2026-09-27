using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Infrastructure.Persistence;
using MiniOrderManagement.Infrastructure.Repositories;
using MiniOrderManagement.Infrastructure.UnitOfWork;

namespace MiniOrderManagement.Infrastructure.UnitOfWork;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection")));

        services.AddScoped(
            typeof(IRepository<>),
            typeof(Repository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}