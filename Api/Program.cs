using Auth.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddDistributedMemoryCache();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

// Domain -  > Entity, Exceptions, Enum, Irepository, Models.
// Infrastructure -> bg Services, DbConfig, Helper Methods, Migrations, Repositories, Seeder, Wrapeprs.
// Application -> Dtos, Iservices, services, mapper. 
// Api- controller, Configurations, MiddleWare, Validations.