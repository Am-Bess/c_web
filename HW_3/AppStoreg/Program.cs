using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using AppStoreg.IAbstract;
using AppStoreg.Models.Context;
using AppStoreg.Mutation;
using AppStoreg.Querty;
using AppStoreg.Repository;
using AppStoreg.Service;

// dotnet ef migrations add initial1.0
// dotnet ef database update 

namespace AppStoreg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer().AddQueryType<MySimpleQuery>().AddMutationType<MiSimpleMutation>(); 


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));                      
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); 
            
            var config = new ConfigurationBuilder();                                    
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>                     
            {
                cb.Register(r => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddTransient<IServiceProduct, ServiceProduct>();
            builder.Services.AddTransient<IServiceCategory, ServiceCategory>();
            builder.Services.AddTransient<IServiceFiles, ServiceFiles>();
            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);              

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var staticFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");     
            Directory.CreateDirectory(staticFilePath);                                              

            app.UseStaticFiles(new StaticFileOptions                                                
            {
                FileProvider = new PhysicalFileProvider(staticFilePath), RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.MapGraphQL();
            app.Run();
        }
    }
}
