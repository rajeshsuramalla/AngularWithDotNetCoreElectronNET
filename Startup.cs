using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngularWithDotnetCoreElectronNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.EnvironmentName == "Development")
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            ElectronBootstrap();
        }

        public async void ElectronBootstrap()
        {
            BrowserWindowOptions options = new BrowserWindowOptions
            {
                Show = false
            };

            BrowserWindow mainWindow = await Electron.WindowManager.CreateWindowAsync();
            mainWindow.OnReadyToShow += () =>
            {
                mainWindow.Show();
            };
            mainWindow.SetTitle("App Name here");

            MenuItem[] menu = new MenuItem[]
            {
                new MenuItem
                {
                    Label = "File",
                    Submenu=new MenuItem[]
                    {
                        new MenuItem
                        {
                            Label ="Exit",
                            Click =()=>{Electron.App.Exit();}
                        }
                    }
                },
                new MenuItem
                {
                    Label = "Info",
                    Click = async ()=>
                    {
                        await Electron.Dialog.ShowMessageBoxAsync("Welcome to App");
                    }
                }
            };

            Electron.Menu.SetApplicationMenu(menu);
        }
    }
}
