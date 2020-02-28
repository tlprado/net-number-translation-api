using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NumberTranslation.Domain.Services;
using Microsoft.Extensions.Configuration;
using NumberTranslation.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NumberTranslation.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddTransient<ITranslationService, TranslationService>();

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "NumberTranslation",
					Version = "v1",
					Description = "This API tranlate kwego number to arabic numbers.",
				});

			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			if (!env.IsProduction())
			{
				app.UseSwagger(c =>
				{
#if !DEBUG
                    c.PreSerializeFilters.Add((apiDoc, httpReq) =>
                    {
                        apiDoc.Servers = new List<OpenApiServer>
                            {new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/witsec"}};
                    });
#endif
				}).UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "v1"));
			}
		}
	}
}
