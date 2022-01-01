using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (HybridSupport.IsElectronActive)
{
    CreateMenu();
    CreateElectronWindow();
}
app.Run();

async void CreateElectronWindow()
{
    var options = new BrowserWindowOptions { Width = 1024, Height = 1024 }; 
    var window = await Electron.WindowManager.CreateWindowAsync(options);
    window.OnClosed += () => Electron.App.Quit();
}

void CreateMenu()
{
    var fileMenu = new MenuItem[]
    {
        new MenuItem { Label = "Home", 
                                Click = () => Electron.WindowManager.BrowserWindows.First().LoadURL($"http://localhost:{BridgeSettings.WebPort}/") },
        new MenuItem { Label = "Privacy", 
                                Click = () => Electron.WindowManager.BrowserWindows.First().LoadURL($"http://localhost:{BridgeSettings.WebPort}/Privacy") },
        new MenuItem { Type = MenuType.separator },
        new MenuItem { Role = MenuRole.quit }
    };

    var viewMenu = new MenuItem[]
    {
        new MenuItem { Role = MenuRole.reload },
        new MenuItem { Role = MenuRole.forcereload },
        new MenuItem { Role = MenuRole.toggledevtools },
        new MenuItem { Type = MenuType.separator },
        new MenuItem { Role = MenuRole.resetzoom },
        new MenuItem { Role = MenuRole.zoomin },
        new MenuItem { Role = MenuRole.zoomout },
        new MenuItem { Type = MenuType.separator },
        new MenuItem { Role = MenuRole.togglefullscreen }
    };

    var menu = new MenuItem[] 
    {
        new MenuItem { Label = "File", Type = MenuType.submenu, Submenu = fileMenu },
        new MenuItem { Label = "View", Type = MenuType.submenu, Submenu = viewMenu }
    };

    Electron.Menu.SetApplicationMenu(menu);
}