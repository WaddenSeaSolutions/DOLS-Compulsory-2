using dols_compulsory_2.Server.Services;
using DOLS_Compulsory_2.Server.DAL;
using DOLS_Compulsory_2.Server.Services;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();           

builder.Services.AddSingleton<FeatureFlaggingService>();


builder.Services.AddScoped<MySqlConnection>(_ =>
    new MySqlConnection(DbUtils.ProperlyFormattedConnectionString));

builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<NoteDAL>();
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

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseSwagger();                         
app.UseSwaggerUI();                       

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
