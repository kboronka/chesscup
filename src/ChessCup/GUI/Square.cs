/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/30/2010
 * Time: 9:41 AM
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ChessCup.Game;
using ChessCup.Game.Units;

namespace ChessCup.GUI
{
	public partial class Square : UserControl
	{
		#region enums
		
		public enum SquareColour
		{
			White,
			Black,
			None
		}

		#endregion
		
		#region members

		private SquareColour colour = SquareColour.White;
		private byte unit;
		private bool selected = false;
		private byte id = 0xFF;
		private Image pieceImage;
		
		#endregion

		#region constructors

		public Square(byte id, SquareColour colour)
		{
			this.id = id;
			this.colour = colour;
			
			InitializeComponent();
			this.UpdateSquare();
		}
		
		#endregion
		
		#region parameters
		
		public SquareColour Colour
		{
			get { return colour; }
		}
		
		public byte Unit
		{
			get { return unit; }
			set
			{
				if (this.unit != value)
				{
					unit = value;
					this.UpdateSquare();
				}
			}
		}
		
		public bool Selected
		{
			get { return selected; }
			set
			{
				if (this.selected != value)
				{
					selected = value;
					this.UpdateSquare();
				}
			}
		}
		
		public byte ID
		{
			get { return id; }
		}
		
		#endregion
		
		#region methods

		private void UpdateSquare()
		{
			switch (this.colour)
			{
				case SquareColour.White:
					this.BackColor = Color.FromArgb(229,255,196);//Color.White;
					break;
				case SquareColour.Black:
					this.BackColor = Color.FromArgb(204,205,153);
					break;
				case SquareColour.None:
					this.BackColor = Color.Transparent;
					break;
			}
			
			if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.KING))
				pieceImage = ChessCup.GUI.Pieces.WhiteKing;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.QUEEN))
				pieceImage = ChessCup.GUI.Pieces.WhiteQueen;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.ROOK))
				pieceImage = ChessCup.GUI.Pieces.WhiteRook;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.BISHOP))
				pieceImage = ChessCup.GUI.Pieces.WhiteBishop;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.KNIGHT))
				pieceImage = ChessCup.GUI.Pieces.WhiteKnight;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.WHITE | Constants.PAWN))
				pieceImage = ChessCup.GUI.Pieces.WhitePawn;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.KING))
				pieceImage = ChessCup.GUI.Pieces.BlackKing;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.QUEEN))
				pieceImage = ChessCup.GUI.Pieces.BlackQueen;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.ROOK))
				pieceImage = ChessCup.GUI.Pieces.BlackRook;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.BISHOP))
				pieceImage = ChessCup.GUI.Pieces.BlackBishop;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.KNIGHT))
				pieceImage = ChessCup.GUI.Pieces.BlackKnight;
			else if ((this.unit & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.BLACK | Constants.PAWN))
				pieceImage = ChessCup.GUI.Pieces.BlackPawn;
			else
				pieceImage = null;

			Graphics g = this.CreateGraphics();
			this.UpdateSquare(g);
		}
		
		private void UpdateSquare(Graphics g)
		{
			g.Clear(this.BackColor);
			#if DEBUG
			if (this.id != 0xFF)
			{
				Font font = new Font("Arial", 6);
				Brush brush = new SolidBrush(Color.Black);
				g.DrawString(this.id.ToString(), font, brush, 0, 0);
			}
			#endif
			
			if (pieceImage != null)
			{
				g.DrawImage(pieceImage, 2, 2, this.Width - 4, this.Height - 4);
			}

			if (this.selected)
			{
				Pen pen = new Pen(Color.Black, 2);
				g.DrawRectangle(pen, 0, 0, this.Width, this.Height);
			}
		}
		
		#endregion
		
		#region events
		
		private void square_Paint(object sender, PaintEventArgs e)
		{
			this.UpdateSquare(e.Graphics);
		}
		
		#endregion
	}
}
