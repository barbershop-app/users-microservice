using microservice.Core;
using microservice.Core.IServices;
using microservice.Data.Access.Services;
using microservice.Data.SQL;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Replace the default .NET logger with Serilog
builder.Host.UseSerilog((context, lc) => lc
.ReadFrom.Configuration(context.Configuration)
);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
}));


builder.Services.AddHttpClient("localhost").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
});

builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"), b => b.MigrationsAssembly("microservice.Data.SQL")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IUnitOfWork ,UnitOfWork>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();


var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseSwagger();

app.UseCors();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.RoutePrefix = String.Empty;
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "users-microservice");
    options.InjectStylesheet("/swagger/custom.css");
});

app.Run();
