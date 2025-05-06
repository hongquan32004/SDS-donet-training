using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using StudentManagementgRPC.Services;
using StudentManagementgRPC.Services.Implementations;
using StudentManagementgRPC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCodeFirstGrpc();
builder.Services.AddSingleton<IStudentService, StudentService>();


// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<IStudentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
