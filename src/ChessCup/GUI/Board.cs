/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/30/2010
 * Time: 9:39 AM
 */
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
				return;
			}
			
			if (this.previousSelectedSquare.Unit != Constants.EMPTY)
			{
				// is current move legal?
				if (this.engine.availbleMoves.Find(delegate(Move m) { return (m.From == previousSelectedSquare.ID && m.To == selectedPiece.ID); }) != null)
				{
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