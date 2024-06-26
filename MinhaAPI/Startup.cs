using CarrinhoCompraAPI.Domain.Services;
using CarrinhoCompraAPI.Infrastructure.Data.Contexts;
using CarrinhoCompraAPI.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarrinhoCompraAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SqlServerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
                            builder.MigrationsAssembly("CarrinhoCompraAPI")));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddScoped<ProdutoRepository>();
            services.AddScoped<ProdutoService>();

            services.AddScoped<CarrinhoRepository>();
            services.AddScoped<CarrinhoService>();

            services.AddScoped<CompraRepository>();
            services.AddScoped<CompraService>();

            services.AddScoped<CompraProdutoRepository>();
            services.AddScoped<CompraProdutoService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinhaAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinhaAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
