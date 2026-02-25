var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
List<backend.DTOS.Record> records = new List<backend.DTOS.Record>(){
    new backend.DTOS.Record(1, "Record 1", "Description for Record 1", DateTime.Now, DateTime.Now),
    new backend.DTOS.Record(2, "Record 2", "Description for Record 2", DateTime.Now, DateTime.Now),
    new backend.DTOS.Record(3, "Record 3", "Description for Record 3", DateTime.Now, DateTime.Now)
};
app.MapGet("/records", () => records);

app.MapGet("/records/{id}", (int id) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(record);
});

app.MapPost("/records", (backend.DTOS.createdtosRecord createdRecord) => {
    var newRecord = new backend.DTOS.Record(records.Count + 1, createdRecord.Name, createdRecord.Description, createdRecord.CreatedAt, createdRecord.UpdatedAt);
    records.Add(newRecord);
    return Results.Created($"/records/{newRecord.Id}", newRecord);
});

app.MapPut("/records/{id}", (int id, backend.DTOS.updatedtosRecord updatedRecord) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    record = new backend.DTOS.Record(id, updatedRecord.Name, updatedRecord.Description, updatedRecord.CreatedAt, updatedRecord.UpdatedAt);
    records[records.FindIndex(r => r.Id == id)] = record;
    return Results.Ok(record);
});

app.MapDelete("/records/{id}", (int id) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    records.RemoveAll(r => r.Id == id);
    // records.Remove(record);

    return Results.NoContent();
});
app.MapGet("/", () => "Hello World!");

app.Run();
