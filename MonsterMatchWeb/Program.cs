using SimpleInjector.Lifestyles;
using SimpleInjector;
using Data;
using BusinessLogin.Web.Contracts;
using BusinessLogic;

Container container = new();
container.Options.DefaultLifestyle = Lifestyle.CreateHybrid(Lifestyle.Scoped, Lifestyle.Transient);
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSimpleInjector(container, options => {
    options.AddAspNetCore()
    .AddControllerActivation()
    .AddViewComponentActivation()
    .AddPageModelActivation()
    .AddTagHelperActivation();

    options.AddLogging();
});

builder.Services.AddSession();

container.Register<IAssetsService, AssetsService>();
container.Register<ISecurityService, SecurityService>();
container.Register(() => {
    var context = new ContextBizagiMatch(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    return context;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Services.UseSimpleInjector(container);
container.Verify();

container.GetInstance<ContextBizagiMatch>().Database.EnsureCreated();

app.Run();
