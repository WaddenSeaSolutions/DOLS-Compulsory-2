using dols_compulsory_2.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();           

builder.Services.AddSingleton<NoteService>(); 

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