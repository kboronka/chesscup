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
