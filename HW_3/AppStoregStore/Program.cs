using Autofac;
using Autofac.Extensions.DependencyInjection;
using AppStoregStore.IAbstract;
using AppStoregStore.Models.Context;
using AppStoregStore.Mutation;
using AppStoregStore.Querty;
using AppStoregStore.Repository;
using AppStoregStore.Service;
using AppStoregStore.WebClient.Client;
using AppStoregStore.WebClient.IAbstractClient;

// dotnet ef migrations add initial2.0
// dotnet ef database update 

namespace AppStoregStore
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
                cb.Register(r => new StoregeContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Host.ConfigureContainer<ContainerBuilder>(x =>
            {
                x.RegisterType<ServiceStorage>().As<IServiceStorage>();
                x.RegisterType<StoregClient>().As<IStoregClient>();
            });

            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapGraphQL();
            app.Run();
        }
    }
}
