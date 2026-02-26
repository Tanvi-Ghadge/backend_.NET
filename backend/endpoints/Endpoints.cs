using System;

namespace backend.endpoints;

public static class Endpoints
{
    private static readonly List<backend.DTOS.Record> records = new List<backend.DTOS.Record>(){
    new backend.DTOS.Record(1, "Record 1", "Description for Record 1", DateTime.Now, DateTime.Now),
    new backend.DTOS.Record(2, "Record 2", "Description for Record 2", DateTime.Now, DateTime.Now),
    new backend.DTOS.Record(3, "Record 3", "Description for Record 3", DateTime.Now, DateTime.Now)
};

public static WebApplication MapEndpoints(this WebApplication app)
{   

    var group = app.MapGroup("/records").WithParameterValidation();

    group.MapGet("/", () => records);

group.MapGet("/{id}", (int id) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(record);
});

group.MapPost("/", (backend.DTOS.createdtosRecord createdRecord) => {
    
    var newRecord = new backend.DTOS.Record(records.Count + 1, createdRecord.Name, createdRecord.Description, createdRecord.CreatedAt, createdRecord.UpdatedAt);
    records.Add(newRecord);
    return Results.Created($"/records/{newRecord.Id}", newRecord);
})
;

group.MapPut("/{id}", (int id, backend.DTOS.updatedtosRecord updatedRecord) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    record = new backend.DTOS.Record(id, updatedRecord.Name, updatedRecord.Description, updatedRecord.CreatedAt, updatedRecord.UpdatedAt);
    records[records.FindIndex(r => r.Id == id)] = record;
    return Results.Ok(record);
});

group.MapDelete("/{id}", (int id) => {
    var record = records.Find(r => r.Id == id);
    if (record is null)
    {
        return Results.NotFound();
    }
    records.RemoveAll(r => r.Id == id);
    // records.Remove(record);

    return Results.NoContent();
});
    return app;
}
}
