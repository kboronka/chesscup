/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/29/2010
 * Time: 8:27 PM
 */
using System;
using System.Windows.Forms;

using SkylaLib.Tools;

namespace ChessCup
{
	internal sealed class Program
	{
		private static Logger log;
		
		[STAThread]
		private static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				ExceptionHandler.Display(ex);
			}
		}
		
		public static void Log(string message)
		{
			#if DEBUG
			if (Program.log == null) Program.log = new Logger();
			Program.log.WriteLine(message);
			#endif
		}
		
		public static void ShowLog()
		{
			#if DEBUG
			if (Program.log == null) Program.log = new Logger();
			Program.log.Show();
			#endif			
		}
	}
}