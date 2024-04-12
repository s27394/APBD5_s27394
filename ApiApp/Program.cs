using ApiApp;
using Microsoft.AspNetCore.Routing.Tree;

var builder = WebApplication.CreateBuilder(args);

// AddAnimal services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IMockDb, MockDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// mappings here
app.MapGet("/animals", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAll());
});

app.MapGet("/animals/{id}", (IMockDb mockDb, int id) =>
{
    var tmpAnimal = mockDb.GetAnimalById(id);
    return tmpAnimal is null ? Results.NotFound() : Results.Ok(tmpAnimal);
});

app.MapPost("/animals", (IMockDb mockDb, Animal animal) =>
{
    mockDb.AddAnimal(animal);
    return Results.Created();
});

app.MapPut("/animals/{id}", (IMockDb mockDb, int id, Animal animal) =>
{
    var existingAnimal = mockDb.GetAnimalById(id);
    if (existingAnimal is null)
    {
        return Results.NotFound();
    }

    mockDb.EditAnimalById(id, animal);
    return Results.NoContent();
});

app.MapDelete("/animals/{id}", (IMockDb mockDb, int id) =>
{
    var existingAnimal = mockDb.GetAnimalById(id);
    if (existingAnimal is null)
    {
        return Results.NotFound();
    }

    mockDb.RemoveAnimalById(id);
    return Results.NoContent();
});

app.MapGet("/animals/{id}/visits", (IMockDb mockDb, int id) =>
{
    var visitsForAnimal = mockDb.GetVisitsByAnimalId(id);
    return Results.Ok(visitsForAnimal);
});

app.MapPost("/animals/{id}/visits", (IMockDb mockDb, int Id, Visit visit) =>
{

    var animal = mockDb.GetAnimalById(visit.AnimalId);

    if (animal is null)
    {
        return Results.BadRequest("Animal with such Id does not exist. Cannot add visit to it.");
    }

    if (!animal.Id.Equals(Id))
    {
        return Results.BadRequest("Id in request header does not match the one in visit.");
    }

    mockDb.AddVisit(visit);
    return Results.Created();
});


app.MapControllers();
app.Run();