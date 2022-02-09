using microservice.Core;
using microservice.Core.IServices;
using microservice.Data.Access.Services;
using microservice.Data.SQL;
using microservice.Web.API.Authentication;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"), b => b.MigrationsAssembly("microservice.Data.SQL")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUnitOfWork ,UnitOfWork>();
builder.Services.AddTransient<IUsersService, UsersService>();

builder.Host.UseSerilog((context, lc) => lc
.ReadFrom.Configuration(context.Configuration)
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSwagger();

app.UseCors();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.UseSwaggerUI();

app.Run();
