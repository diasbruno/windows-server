using System.IO;
using System.Configuration;
using System.Data;
using WatsonWebserver;
using WatsonWebserver.Core;
using System.Windows;

namespace WindowsServer
{
    public class Program
    {
	// All WPF applications should execute on a single-threaded apartment (STA) thread
	[STAThread]
	public static void Main()
	{
	    App app = new App();
	    app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
	    app.Run();
	}
    }

    public class App : Application
    {

	static async Task DefaultRoute(HttpContextBase ctx)
	{
	    try
	    {

		Application.Current.Dispatcher.Invoke((Action)delegate{
			Window window = new Window();
			window.Show();
		    });

		await ctx.Response.Send("ok");
	    }
	    catch (Exception ex)
	    {
		Console.WriteLine(ex.Message);
	    }
	}

	protected override void OnStartup(StartupEventArgs e)
	{
	    base.OnStartup(e);


	    WebserverSettings _Settings = new WebserverSettings
	    {
		Hostname = "localhost",
		Port = 8000
	    };

	    Webserver server = new Webserver(_Settings, DefaultRoute);

	    server.Start();
	}
    }
}
