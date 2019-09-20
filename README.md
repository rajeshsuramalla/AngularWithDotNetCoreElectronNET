# AngularWithDotNetCoreElectronNET
Angular SPA with Dotnet Core and ElectronNet API
# AngularWithDotNetCoreElectronNET
Angular SPA with Dotnet Core and ElectronNet API

1. Open VS 2017 with Administrator mode
2. Create Asp.Net core Web Application with Angular template
3. Install the ElectronNET.CLI using following command "dotnet tool install ElectronNET.CLI -g"
4. Goto project folder and open cmd 
5. Execute the following command "electronize init", it will create electron-manifest.json file in project folder
6. Right click on dependencies, goto Nuget package manager and install ElectronNET.API
7. Add below method in Startup.cs
8. Call that method from Configure() in Startup.cs

	

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
9. Add UseElectron(args) in Program.cs

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseElectron(args);
        }
	
10. Got cmd and execte the following command "electronize start", it will open the desktop application. First time it will take time.


11. Production build for windows:electronize build /target win

Prerequisites
1. Dotnet core SDK 2.2
2. Node js
3. Angular
4. ElectronNET.CLI
