using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebDemo;
using WebDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<WebDemoDatabase>(optionsAction =>
{
    optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Appcon"), options =>
    {
        options.MinBatchSize(2);
    });
});

builder.Services.AddScoped<ChangeInformationService, ChangeInformationService>();
builder.Services.AddScoped<JwtTokenService, JwtTokenService>();
builder.Services.AddScoped<CartService, CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = (context) =>
        {
            var token = context.HttpContext.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            return Task.CompletedTask;
        }
    };

    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenService.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
                            TokenValidationParameters validationParameters) =>
        {
            return notBefore.HasValue ? notBefore.Value <= DateTime.UtcNow : false ||
            !expires.HasValue || expires.Value >= DateTime.UtcNow;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
