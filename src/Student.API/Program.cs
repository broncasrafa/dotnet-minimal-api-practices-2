using Student.API.DependencyInjection;
using Student.API.Endpoints;
using Student.API.Endpoints.Course;
using Student.Application.DependencyInjection;
using Student.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllerAndJsonConfig();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddCorsConfig();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Minimal Api Practices 2");
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    //c.InjectStylesheet("/css/swagger-ui/swagger-dark.css");
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseStaticFiles();
//app.UseExceptionHandler();
app.UseCors("AllowAllPolicy");

#region [ Endpoints ]
app.MapCourseEndpoints();
app.MapStudentEndpoints();
app.MapEnrollmentEndpoints();
#endregion



app.Run();

