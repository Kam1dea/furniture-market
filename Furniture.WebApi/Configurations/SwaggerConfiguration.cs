using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

public static class SwaggerConfiguration
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // 1. Основная информация о API
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Furniture API",
                Version = "v1",
                Description = "API для заказа мебели: просмотр, создание, отзывы",
            });

            // 2. Подключение XML-комментариев (для отображения /// <summary>)
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            // 3. Настройка авторизации через JWT (Bearer Token)
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer eyJhbGciOiJIUzI1Ni...'",
                Name = "Authorization",           // Название заголовка
                In = ParameterLocation.Header,    // Где передаётся — в заголовке HTTP
                Type = SecuritySchemeType.ApiKey, // Тип: API-ключ (в данном случае — JWT)
                Scheme = "Bearer",                // Схема аутентификации
                BearerFormat = "JWT"              // Для ясности в UI
            });

            // 4. Указание, что большинство endpoint'ов требуют авторизации
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>() // Пусто — значит, нет дополнительных scopes
                }
            });
        });
    }
}