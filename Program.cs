using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//<-----
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
//----->

builder.Services.AddControllers();

/**
 * WebApplication implements IHost,IApllicationBuilder,and IEndpointRouteBuilder
 * 
 * middleware is used to modifying the application's pipeline---handling request and response
 */
var app = builder.Build();

// Configure the HTTP request pipeline.

//<-----
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else {
    app.UseHsts();
}

//---->

app.UseHttpsRedirection();

//<-----
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions() {
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
//----->
app.UseAuthorization();

//<-----
// requestdelegate
// custom middleware use,map(mapwhen),run
//app.Use(async (context, next) => {
//    Console.WriteLine($"Logic before executing the next delegate in the use method");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the use method");
//});

//// any middlware component that add after the map method, even we don't use the Run middleware inside the branch 
//app.Map("/usingmapbranch",builder => {
//    builder.Use(async (context, next) => {
//        Console.WriteLine("Map branch logic in the Use method before the next delegate");
//        await next.Invoke();
//        Console.WriteLine("Map branch logic in the Use method after the next delegate");
//    });
//    builder.Run(async context => {
//        Console.WriteLine($"Map branch response to the client in the run method");
//        await context.Response.WriteAsync("Hello fro");
//    });
//});
//app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), (IApplicationBuilder obj) => {
//    obj.Run(async (context) => await context.Response.WriteAsync("Hello from the MapWhen branch."));

//});
//app.Run(async context => {
//    Console.WriteLine($"Writing the response to the client in the run method");
//    await context.Response.WriteAsync("Hello from the middleware component.");
//});

// ----->

app.MapControllers();

app.Run();

