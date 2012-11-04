#region License
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ChessCup;
using ChessCup.Game;

namespace ChessCup.GUI
{
	public partial class MoveList : UserControl
	{
		private List<Move> moves;
		private List<string> feds;
		
		public MoveList()
		{
			InitializeComponent();
			this.Clear();
		}
		
		public void Clear()
		{
			this.moves = new List<Move>();
			this.feds = new List<string>();
			this.UpdateList();
		}
		
		public void AddMove(Move move, string fed)
		{
			this.moves.Add(move);
			this.feds.Add(fed);
			this.UpdateList();
		}
		
		public void UpdateList()
		{
			//listview1.Items.Add(new ListViewItem(new string[] { "Name", "Address" } );
			this.list.BeginUpdate();
			this.list.Items.Clear();
			
			for (int index = 0; index < this.moves.Count; index += 2)
			{
				int moveNumber = (index / 2) + 1;
				string white = this.moves[index].ToString();
				string black = "";
				if ((index + 1) < this.moves.Count) black = this.moves[index + 1].ToString();

				ListViewItem row = new ListViewItem(new string[] {moveNumber.ToString() + ".", white, black});
				this.list.Items.Add(row);
			}
			
			this.list.EndUpdate();
		}
	}
}
