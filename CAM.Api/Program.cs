using ApplicationDbContext.EntityConfigs;
using CAM.Api.Mapper;
using CAM.Service;
using ReadSqlDataAccess;

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
            builder.Services.AddSwaggerGen();
            builder.Services.AddCAMDbContext(builder.Configuration);
            builder.Services.Configure<ReadDbContextConfigEntry>(
                        builder.Configuration.GetSection("ReadDbContextConfigEntry"));

            builder.Services.AddMediatR(cnfg => cnfg.RegisterServicesFromAssembly(typeof(ServicesConfig).Assembly));
            builder.Services.AddCAMServices();

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

            app.Run();
        }
    }
}
