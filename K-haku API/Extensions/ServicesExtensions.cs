using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace K_haku_API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kuhaku",
                    Description = "It is a website that takes data from different pages of movies and series, and uses them through an API created with web scraping by me that takes all this information.",
                    Contact = new OpenApiContact
                    {
                        Email = "abreumartinezjefferson@gmail.com",
                        Name = "Jefferson Abreu",
                        Url = new Uri("https://github.com/AbreuHD")
                    }
                });

                options.DescribeAllParametersInCamelCase();

            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection service)
        {
            service.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

        }
    }
}
