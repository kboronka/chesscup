/* Copyright (C) 2013 Kevin Boronka
 * 
 * software is distributed under the BSD license
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */

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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ChessCup;
using ChessCup.Game;
using ChessCup.Game.Units;

// TODO: add column and row lables
namespace ChessCup.GUI
{
	public delegate EventHandler MoveCompleteEventHandler();
	
	public partial class Board : UserControl
	{
		#region members
		
		public EventHandler MoveDone;
		private ChessCup.GUI.Square[] squares;
		private ChessCup.GUI.Square previousSelectedSquare;
		private Engine engine;
		
		#endregion
		
		#region constructors

		public Board()
		{
			InitializeComponent();
			squares = new Square[tableLayoutPanel1.RowCount * tableLayoutPanel1.ColumnCount];
			byte i = 0;
			
			Square.SquareColour rowStartColour = Square.SquareColour.White;
			Square.SquareColour lastColour;
			
			for (byte row = 0; row < tableLayoutPanel1.RowCount; row++)
			{
				if (rowStartColour == ChessCup.GUI.Square.SquareColour.White)
				{
					rowStartColour = ChessCup.GUI.Square.SquareColour.Black;
				}
				else
				{
					rowStartColour = ChessCup.GUI.Square.SquareColour.White;
				}
				
				lastColour = rowStartColour;

				for (byte col = 0; col < tableLayoutPanel1.ColumnCount; col++)
				{
					if (lastColour == ChessCup.GUI.Square.SquareColour.White)
					{
						lastColour = ChessCup.GUI.Square.SquareColour.Black;
					}
					else
					{
						lastColour = ChessCup.GUI.Square.SquareColour.White;
					}
					
					squares[i] = new Square(i, lastColour);
					this.tableLayoutPanel1.Controls.Add(squares[i], col, row);
					squares[i].Dock = DockStyle.Fill;
					squares[i].Click += new EventHandler(SquareClick);

					i++;
				}
			}
		}
		
		#endregion
		
		#region properties

		#endregion
		
		#region methods
		
		public void UpdateBoardArray()
		{
			for (int i = 0; i < 64; i++)
			{
				this.squares[i].Unit = this.engine.Units[i];
			}
		}
		
		public Engine Engine
		{
			get { return this.engine; }
			set
			{
				if (this.engine != value)
				{
					this.engine = value;
					this.UpdateBoardArray();
				}
			}
		}

		#endregion
		
		#region events
		
		private void SquareClick(object sender, EventArgs e)
		{
			ChessCup.GUI.Square selectedPiece = (ChessCup.GUI.Square)sender;
			
			// don't pickup air
			if (selectedPiece.Unit == Constants.EMPTY && previousSelectedSquare == null)
			{
				return;
			}
			
			// don't move other players peices
			if (selectedPiece.Unit != Constants.EMPTY)
			{
				if (((selectedPiece.Unit & Constants.MASK_COLOUR) != this.engine.Turn) && (previousSelectedSquare == null))
				{
					return;
				}
			}

			if (selectedPiece == this.previousSelectedSquare)
			{
				selectedPiece.Selected = false;
				this.previousSelectedSquare = null;
				return;
			}

			if (previousSelectedSquare == null)
			{
				previousSelectedSquare = selectedPiece;
				selectedPiece.Selected = true;
				
				Program.Log("Selected: " + selectedPiece.ID);
				
				foreach (Move move in this.engine.availbleMoves.FindAll(delegate(Move m) { return (m.From == selectedPiece.ID); }))
				{
					Program.Log(move.ToString());
				}
					
				return;
			}
			
			if (this.previousSelectedSquare.Unit != Constants.EMPTY)
			{
				// is current move legal?
				if (this.engine.availbleMoves.Find(delegate(Move m) { return (m.From == previousSelectedSquare.ID && m.To == selectedPiece.ID); }) != null)
				{
					/*
					if (selectedPiece.Unit != Constants.EMPTY)
					{
						if ((selectedPiece.Unit & Constants.BLACK) > 0)
						{
							this.prisonersOfWarWhite.AddDeadMan(selectedPiece.Unit);
						}
						else
						{
							this.prisonersOfWarBlack.AddDeadMan(selectedPiece.Unit);
						}
					}
					*/

					this.engine.MovePiece(this.previousSelectedSquare.ID, selectedPiece.ID);
					this.UpdateBoardArray();
					MoveDone.Invoke(sender, e);
				}
			}
			
			this.previousSelectedSquare.Selected = false;
			this.previousSelectedSquare = null;
		}

		#endregion
		
	}
}