/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 4/8/2010
 * Time: 10:50 PM
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using ChessCup.Game;
using ChessCup.Game.Units;

// TODO: captured units should be sorted by colour
namespace ChessCup.GUI
{
	public partial class PrisonersOfWar : UserControl
	{
		#region members

		private ChessCup.GUI.Square[] squares = new ChessCup.GUI.Square[16];
		private List<Constants.UnitType> deadMen = new List<Constants.UnitType>();
		private int numberDead = 0;
		
		#endregion
		
		#region constructors
		
		public PrisonersOfWar()
		{
			InitializeComponent();
			
			int i = 0;
			
			for (int row = 0; row < tableLayoutPanel1.RowCount; row++) {
				for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++) {
					squares[i] = new ChessCup.GUI.Square(0xFF, Square.SquareColour.Black);
					this.tableLayoutPanel1.Controls.Add(squares[i], col, row);
					squares[i].Dock = DockStyle.Fill;
					i++;
				}
			}
		}
		
		#endregion
		
		#region properties
		
		public List<Constants.UnitType> DeadMen {
			get { return deadMen; }
			set { deadMen = value; }
		}
		
		
		#endregion
		
		#region methods
		
		public void AddDeadMan(byte Unit)
		{
			squares[numberDead++].Unit = Unit;
		}
		
		#endregion
		
		#region events
		
		#endregion
	}
}
