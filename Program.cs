using curvela_backend.Data;
using curvela_backend.src.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar a política de cookies (opcional: definir as regras de consentimento, SameSite, etc.)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Se precisar de consentimento para cookies não essenciais, habilite aqui:
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IProductService, ProductService>();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Se estiver em desenvolvimento, configurar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adiciona o middleware de política de cookies
app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllers();

app.Run();
