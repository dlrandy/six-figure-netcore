using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;


LogManager.Setup()
    .LoadConfigurationFromFile(
    string.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));
//LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(
//    "nlog.config");

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
//<-----
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
// disdable ApiController default model state validation
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
//----->

//builder.Services.AddControllers();
builder.Services.AddControllers(config =>{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
  .AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter()
  .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

/**
 * WebApplication implements IHost,IApllicationBuilder,and IEndpointRouteBuilder
 * 
 * middleware is used to modifying the application's pipeline---handling request and response
 */
var app = builder.Build();

// Configure the HTTP request pipeline.

//<-----

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else {
//    app.UseHsts();
//}
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
{
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
app.UseAuthentication();
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

