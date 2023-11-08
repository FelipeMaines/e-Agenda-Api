using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Infra.Orm;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication1.Config.AutoMapperConfig;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Config;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class Program
    {
        public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var velue = reader.GetString();
                return TimeSpan.Parse(velue);
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ConfigurarSerilog(builder.Logging);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarInjecaoDependecia(builder.Configuration);
            builder.Services.ConfigurarSwagger();

            builder.Services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen( c =>
            {
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}