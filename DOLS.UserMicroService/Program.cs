
using DOLS.UserService.DAL;
using DOLS.UserService.Service;
using DOLS_Compulsory_2.Server.DAL;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MySqlConnection>(_ =>
    new MySqlConnection(DbUtils.ProperlyFormattedConnectionString));

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UserDAL>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();