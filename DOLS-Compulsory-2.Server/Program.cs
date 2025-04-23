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

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();


app.UseSwagger();                         
app.UseSwaggerUI();                       

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
