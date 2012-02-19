/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/29/2010
 * Time: 8:27 PM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using SkylaLib.Tools;

using ChessCup.Game;
using ChessCup.Game.Units;
using ChessCup.GUI;

namespace ChessCup
{
	public partial class MainForm : Form
	{
		
		#region members
		
		private Engine engine;
		
		#endregion
		
		#region constructors

		public MainForm()
		{
			InitializeComponent();

			this.engine = new Engine();
			this.board.Engine = this.engine;
			this.board.MoveDone += new EventHandler(board_MoveDone);
			
			this.Text = "ChessCup " + AssemblyInfo.Version;
			this.copyrightStatus.Text = AssemblyInfo.Copyright;
			this.UpdateStatusBar();

			Program.ShowLog();
		}
		
		#endregion
		
		#region properties
		
		#endregion
		
		#region methods
		
		private void UpdateMoveList()
		{
			this.moveList.BeginUpdate();
			this.moveList.Items.Clear();
			
			foreach (Move move in this.engine.Moves)
			{
				this.moveList.Items.Add(move);
			}
			
			this.moveList.EndUpdate();
		}

		private void UpdateStatusBar()
		{
			if (this.engine.Turn == Constants.WHITE)
			{
				this.turnStatus.Text = "White's Turn";
			}
			else
			{
				this.turnStatus.Text = "Black's Turn";
			}
			
			int value = this.engine.Value;
			if (value > 0)
			{
				this.scoreStatus.Text = "± " + value.ToString();
			}
			else
			{
				this.scoreStatus.Text = "± (" + Math.Abs(value).ToString() + ")";
			}
			
			this.fen.Text = this.engine.FEN;
		}
		
		#endregion
		
		#region events

		private void board_MoveDone(object sender, EventArgs e)
		{
			this.UpdateStatusBar();
			this.UpdateMoveList();
		}
		
		private void LoadFENClick(object sender, EventArgs e)
		{
			try
			{
				this.engine.FEN = this.fen.Text;
				this.board.UpdateBoardArray();
				this.UpdateMoveList();
			}
			catch (Exception ex)
			{
				ExceptionHandler.Display(ex);
			}
		}
		
		private void FenEnter(object sender, EventArgs e)
		{
			this.fen.SelectAll();
		}
		
		#endregion
	}
}
