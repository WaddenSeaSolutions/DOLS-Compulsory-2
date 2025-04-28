using DOLS.UserService.DAL;
using DOLS.UserService.Service;
using DOLS_Compulsory_2.Server.DAL;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MySqlConnection>(_ =>
    new MySqlConnection(DbUtils.ProperlyFormattedConnectionString));

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UserDAL>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:32116")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
