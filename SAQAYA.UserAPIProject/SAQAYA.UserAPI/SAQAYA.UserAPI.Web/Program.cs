using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repositories;
using SAQAYA.UserAPI.Models;
using SAQAYA.UserAPI.Repositories;
using SAQAYA.UserAPI.Services;
using SAQAYA.UserAPI.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Services
//Add Service to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped(typeof(UserService));
foreach (TypeInfo T in
          typeof(UserService).Assembly.DefinedTypes
          .Where(i => i.Name.EndsWith("Service")))
{
    builder.Services.AddScoped(T);
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IValidator<UserModel>, UserValidator>();
builder.Services.AddScoped(typeof(MinimalApiEndpoints));
builder.Services.AddScoped(typeof(UnitOfWork));
builder.Services.AddScoped(typeof(Generic<>));
builder.Services.AddScoped(typeof(EntitiesContext));
builder.Services.AddDbContext<EntitiesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserAPIConnectionString")));

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    //Use Swagger in application.
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1"));
}

// Sample Endpoint 
app.MapGet("/", () => "Hello! This is a Minimal User API application, please use Swagger or Postman to access Minimal Api End Points").ExcludeFromDescription();

#region MinimalApiEndpoints
MinimalApiEndpoints.RegisterUser(app);
MinimalApiEndpoints.GetUser(app);
MinimalApiEndpoints.UpdateUser(app);
MinimalApiEndpoints.DeleteUser(app);
#endregion

app.Run();
