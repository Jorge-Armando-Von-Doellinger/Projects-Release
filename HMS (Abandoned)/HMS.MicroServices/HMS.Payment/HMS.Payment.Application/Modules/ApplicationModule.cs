using HMS.Payments.Application.Interfaces.UseCases;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Parsers;
using HMS.Payments.Application.Processor;
using HMS.Payments.Application.Services;
using HMS.Payments.Application.UseCases;
using HMS.Payments.Core.Interfaces.Internal_Services;
using HMS.Payments.Core.Interfaces.Processor;
using HMS.Payments.Messaging.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Payments.Application.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddServices()
            .AddMappers()
            .AddProcessor()
            .AddParsers();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IPaymentEmployeeUseCase, PaymentEmployeeUseCase>();
        services.AddScoped<IPaymentUseCase, PaymentUseCase>();
        services.AddScoped<IRefundUseCase, RefundUseCase>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddSingleton<JsonSchemaService>();

        //services.AddSingleton<ISchemasService, SchemasService>();
        //services.AddSingleton<ISchemasModelService, SchemasModelService>();
        //services.AddScoped<HandlerService>();
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<EmployeePaymentMapper>();
        services.AddScoped<PaymentMapper>();
        services.AddScoped<RefundMapper>();
        return services;
    }

    private static IServiceCollection AddProcessor(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IMessageProcessor, PaymentProcessor>(MessageProcessorSelectorEnum.Payment
            .ToString());
        services.AddKeyedSingleton<IMessageProcessor, PaymentEmployeeProcessor>(MessageProcessorSelectorEnum
            .PaymentEmployee.ToString());
        services.AddKeyedSingleton<IMessageProcessor, RefundProcessor>(MessageProcessorSelectorEnum.Refund.ToString());
        return services;
    }

    private static IServiceCollection AddParsers(this IServiceCollection services)
    {
        services.AddScoped<MessageParser>();
        return services;
    }
}