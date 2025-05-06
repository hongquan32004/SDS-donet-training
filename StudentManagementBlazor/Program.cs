using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using StudentManagementBlazor.Components;
using StudentManagementBlazor.GrpcClient;
using StudentManagementgRPC.Services.Interfaces;
using AntDesign;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IStudentService>(provider =>
{
    var chanel = GrpcChannel.ForAddress(" http://localhost:5232");
    return chanel.CreateGrpcService<IStudentService>();
});
builder.Services.AddSingleton<StudentGrpcClient>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntDesign();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
