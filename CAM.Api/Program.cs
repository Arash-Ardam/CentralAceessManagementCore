using ApplicationDbContext.EntityConfigs;
using CAM.Api.Configurations.Auth;
using CAM.Api.Configurations.Swagger;
using CAM.Api.Mapper;
using CAM.Service;
using ReadAppDbContext.Configs;
using ReadDbContext;

namespace CAM.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
            builder.Services.AddEndpointsApiExplorer();
            builder.AddAuthConfigs();
            builder.AddSwaggerConfigs();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCAMDbContext(builder.Configuration);
            builder.Services.AddCAMReadDbContext();
            builder.Services.AddWriteDbContexts(builder.Configuration);
            builder.Services.AddReadDbContexts(builder.Configuration);


            builder.Services.AddMediatR(cnfg => cnfg.RegisterServicesFromAssembly(typeof(ServicesConfig).Assembly));
            builder.Services.AddCAMServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerConfigs();
            }

            app.UseHttpsRedirection();

            app.UseAuthConfigs();

            app.MapControllers();

            app.Run();
        }
    }
}
