﻿#region License
// Copyright (c) 2012 Kevin Boronka. All rights reserved.
//
// http://www.opensource.org/licenses/bsd-license.php
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY 
// OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
#endregion

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