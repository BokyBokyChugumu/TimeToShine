using Microsoft.AspNetCore.Mvc;
using WebApplication1; 
using WebApplication1.Models;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DeviceManager>(provider =>
{
    var manager = new DeviceManager();

    
    manager.AddDevice(new Smartwatch { Id = "SW-API-1", Name = "API Smartwatch 1", IsTurnedOn = false, BatteryPercentage = 80 });
    manager.AddDevice(new PersonalComputer { Id = "PC-API-1", Name = "API PC 1", IsTurnedOn = true, OperatingSystem = "Windows" });
    manager.AddDevice(new EmbeddedDevice { Id = "ED-API-1", Name = "API Embedded 1", IsTurnedOn = false, IPAddress = "192.168.1.100", NetworkName = "MD Ltd. Network" });
    manager.AddDevice(new EmbeddedDevice { Id = "ED-API-2", Name = "API Embedded 2", IsTurnedOn = true, IPAddress = "10.0.0.5", NetworkName = "Home Network" });
    manager.AddDevice(new Smartwatch { Id = "SW-API-2", Name = "API Smartwatch 2", IsTurnedOn = true, BatteryPercentage = 30 });
    manager.AddDevice(new PersonalComputer { Id = "PC-API-2", Name = "API PC 2", IsTurnedOn = false, OperatingSystem = null });
    return manager;
});

var app = builder.Build();




app.MapGet("/devices", (DeviceManager manager) =>
{
    var devicesShortInfo = manager.GetAllDevicesShortInfo();
    return Results.Ok(devicesShortInfo);
})
.Produces<List<DeviceShortInfoResponse>>(StatusCodes.Status200OK);


app.MapGet("/devices/{id}", (string id, DeviceManager manager) =>
{
    var device = manager.GetDeviceById(id);
    if (device == null)
    {
        return Results.NotFound(new { message = $"Device with ID '{id}' not found." });
    }
    return Results.Ok(device);
})
.Produces<Device>(StatusCodes.Status200OK);


app.MapPost("/devices", ([FromBody] DeviceCreateRequest newDeviceRequest, DeviceManager manager) =>
{
    try
    {
        var device = newDeviceRequest.ToDevice();
        manager.AddDevice(device);
        return Results.Created($"/devices/{device.Id}", new { message = "Device created successfully", deviceId = device.Id }); // 201 Created
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
})
.Accepts<DeviceCreateRequest>("application/json")
.Produces<object>(StatusCodes.Status201Created);


app.MapPut("/devices/{id}", (string id, [FromBody] DeviceUpdateRequest updatedDeviceRequest, DeviceManager manager) =>
{
    try
    {
        manager.UpdateDevice(id, updatedDeviceRequest.ToDevice(id));
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { message = ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
})
.Accepts<DeviceUpdateRequest>("application/json")
.Produces(StatusCodes.Status204NoContent);


app.MapDelete("/devices/{id}", (string id, DeviceManager manager) =>
    {
        try
        {
            manager.RemoveDevice(id);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { message = ex.Message });
        }
    })
    .Produces(StatusCodes.Status204NoContent);



app.Run();