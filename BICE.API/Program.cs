using BICE.SRV;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(typeof(Materiel_SRV), new Materiel_SRV());
builder.Services.AddSingleton(typeof(Vehicule_SRV), new Vehicule_SRV());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();


// app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();