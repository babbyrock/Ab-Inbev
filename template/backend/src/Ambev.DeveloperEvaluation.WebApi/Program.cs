using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Application.Sales.CancelledSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Application.Sales.ModifySale;
using Ambev.DeveloperEvaluation.Application.Sales.SaleCancelledItem;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Serilog;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Adiciona o domínio e a porta que você quer permitir
                          .AllowAnyHeader()  // Permite qualquer header
                          .AllowAnyMethod(); // Permite qualquer método (GET, POST, PUT, DELETE, etc.)
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );
                
            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddTransient<SaleService>();

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddRebus(bus => bus
                .Transport(t => t.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"), "Rebus.OrderQueue"))
                .Routing(r => r.TypeBased()
                    .Map<SaleCreatedEvent>("Rebus.OrderQueue")
                    .Map<SaleModifiedEvent>("Rebus.OrderQueue")
                    .Map<SaleCancelledEvent>("Rebus.OrderQueue")
                    .Map<SaleCancelledItemEvent>("Rebus.OrderQueue")
                )
            );
            builder.Services.AddTransient<IHandleMessages<SaleCreatedEvent>, SaleCreatedEventHandler>();
            builder.Services.AddTransient<IHandleMessages<SaleModifiedEvent>, ModifySaleCommandHandler>();
            builder.Services.AddTransient<IHandleMessages<SaleCancelledEvent>, SaleCancelledEventHandler>();
            builder.Services.AddTransient<IHandleMessages<SaleCancelledItemEvent>, SaleCancelledItemEventHandler>();
            builder.Services.AutoRegisterHandlersFromAssemblyOf<ModifySaleCommandHandler>();

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowLocalhost4200");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
