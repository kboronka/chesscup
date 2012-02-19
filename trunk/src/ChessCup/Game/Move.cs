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
using ChessCup.GUI;
using ChessCup.Game.Units;

namespace ChessCup.Game
{
	public class Move
	{
		#region members

		private string move = "";
		private int from;
		private int to;

		#endregion
		
		#region constructors

		public Move(byte unit, byte from, byte to, bool capture)
		{
			// TODO: generate correct algebraic notation
			// http://en.wikipedia.org/wiki/Algebraic_notation_(chess)
			this.from = from;
			this.to = to;
			
			if ((unit & Constants.MASK_TYPE) == Constants.KING)
			{
				// castle king side
				if (to == (from - 2)) this.move = "0-0";
				if (to == (from + 2)) this.move = "0-0-0";
			}
			
			if (String.IsNullOrEmpty(this.move))
			{
				this.move += BaseUnit.GetSymbol(unit);
				if (capture)
				{
					if ((unit & Constants.MASK_TYPE) == Constants.PAWN) this.move += Move.IndexToAddress(from)[0];
					
					this.move += "x";
				}
				
				this.move += Move.IndexToAddress(to);
			}
		}
		
		#endregion
		
		#region properties

		public int To
		{
			get { return this.to; }
			set { this.to = value; }
		}
		
		public int From
		{
			get { return this.from; }
			set { this.from = value; }
		}
		
		#endregion
		
		#region methods
		
		public override string ToString()
		{
			return this.move;
		}
		
		public static string IndexToAddress(byte location)
		{
			if (location > 64) throw new ArgumentException("location \"" + location.ToString() + "\" is invalid");
			
			return Move.File(location) + "" + Move.Rank(location);
		}
		
		public static byte AddressToIndex(string address)
		{
			if (address.Length != 2) throw new ArgumentException("Address \"" + address + "\" is invalid");
			if (address[0] < 'a' || address[0] > 'k') throw new ArgumentException("Address \"" + address + "\" is invalid");
			if (address[1] < '1' || address[1] > '8') throw new ArgumentException("Address \"" + address + "\" is invalid");
			
			int column = address[0] - 'a';
			int row = '8' - address[1];
			
			return (byte)((row * 8) + column);
		}
		
		private static char File(byte location)
		{
			return (char)('a' + (location % 8));
		}
		
		private static char Rank(byte location)
		{
			return (char)('1' + (7 - (location / 8)));
		}
		
		#endregion
		
	}
}
