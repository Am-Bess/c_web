
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AppStore.IAbstract;
using AppStore.Models.Context;
using AppStore.Mutation;
using AppStore.Querty;
using AppStore.Repository;
using AppStore.Service;
using AppStore.WebClient.Client;
using AppStore.WebClient.IAbstractClient;

// dotnet ef migrations add initial1.0
// dotnet ef database update 
// docker build -t appstore .
// docker run -p 8020:8080 appstore


namespace AppStore
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
