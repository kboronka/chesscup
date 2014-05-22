/* Copyright (C) 2014 Kevin Boronka
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
