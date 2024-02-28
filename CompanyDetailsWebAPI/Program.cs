using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Repository;
using CompanyDetailsWebAPI.Repository.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICommonDropDownRepository, CommonDropDownRepository>();
builder.Services.AddScoped<IEmployeeRegRepository, EmployeeRegRepository>();
builder.Services.AddScoped<IUserRegRepository, UserRegRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<OrganizationRepository>();


//builder.Services.AddScoped<ILoginRepository, LoginRepository>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


