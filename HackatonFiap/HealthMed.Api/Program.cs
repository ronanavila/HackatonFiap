using Auth.Services;
using HealthMed.Application.Contracts;
using HealthMed.Application.Services;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Contratcs;
using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Repository.LoginRepository;
using HealthMed.Infrastructure.Repository.MedicRepository;
using HealthMed.Infrastructure.Repository.PatientRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
      options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(x =>
    {
      x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "HeathMed Api",
    Description = "HeathMed Api",


  });

  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = @"Enter 'Bearer' [space] your token",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var secret = builder.Configuration.GetValue<string>("Secret") ?? string.Empty;

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", x =>
{
  x.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false
  };
});

var dbConnectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection") ?? string.Empty;

builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IMedicService, MedicService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IMedicRepository, MedicRepository>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();

var app = builder.Build();

LoadConfiguration(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void LoadConfiguration(WebApplication app)
{
  Settings.ConnectionString = app.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection") ?? string.Empty;
  Settings.Secret = app.Configuration.GetValue<string>("Secret") ?? string.Empty;
}