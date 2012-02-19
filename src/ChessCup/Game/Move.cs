/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/29/2012
 * Time: 8:04 PM
 */
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
