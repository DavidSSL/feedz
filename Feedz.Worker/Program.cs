using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

if (hangfireConnectionString is null)
{
    throw new Exception("Missing HangfireConnection connection string");
}

// Add services to the container.
builder.Services.AddHangfire(configuration => configuration
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(hangfireConnectionString)
    )
    .AddHangfireServer()
    .AddAuthorization()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseHangfireDashboard(pathMatch: "");

app.Run();

