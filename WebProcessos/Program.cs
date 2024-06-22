using Microsoft.EntityFrameworkCore;
using WebProcessos.Data;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DataBase");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkSqlServer().
    AddDbContext<BancoContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEtapaRepositorio, EtapaRepositorio>();
builder.Services.AddScoped<IServicoRepositorio, ServicoRepositorio>();
builder.Services.AddScoped<IOrdemServicoRepositorio, OrdemServicoRepositorio>();
builder.Services.AddScoped<IOrdemServico_EtapaRepositorio, OrdemServico_EtapaRepositorio>();

builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSession(x => {
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
#if DEBUG
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
#endif

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");
});


app.Run();
