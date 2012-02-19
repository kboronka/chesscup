/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 11/21/2010
 * Time: 9:19 PM
 */

using System;
using System.Collections;
using System.Collections.Generic;

using ChessCup.Game;
using ChessCup.Game.Units;

namespace ChessCup.Game.Units
{
	public class King : BaseUnit
	{
		
		public static List<byte> LegalMoves(byte location, byte[] units, ulong attackedSquares)
		{
			List<byte> moves = new List<byte>();
			int[] possibleMoves = {FORWARD_LEFT, FORWARD_RIGHT, REVERSE_LEFT, REVERSE_RIGHT, FORWARD, REVERSE, LEFT, RIGHT};
			
			// TODO: castles
			foreach (int move in possibleMoves)
			{
				int target = location;
				while (true)
				{
					if (!BaseUnit.ValidateMove(target, move)) break;
					
					target += move;
					
					if (units[target] == Constants.EMPTY)
					{
						// vacancy
						moves.Add((byte)target);
					}
					else if ((units[target] & Constants.MASK_COLOUR) != (units[location] & Constants.MASK_COLOUR))
					{
						// kill
						moves.Add((byte)target);
						break;
					}
					else
					{
						// friendly fire
						break;
					}
					
					// move of length 1 only
					break;
				}
			}
			
			if (King.KingSideCastle(location, units, attackedSquares))
			{
				moves.Add((byte)(location + 2));
			}
			
			if (King.QueenSideCastle(location, units, attackedSquares))
			{
				moves.Add((byte)(location - 2));
			}
			
			return moves;
		}
		
		
		private static bool KingSideCastle(byte location, byte[] units, ulong attackedSquares)
		{
			int kingLocation;

			if ((units[location] & Constants.MASK_COLOUR) == Constants.WHITE)
			{
				kingLocation = 60;
			}
			else
			{
				kingLocation = 4;
			}
			
			if (location != kingLocation) return false;
			if (units[kingLocation + 1] != Constants.EMPTY) return false;
			if (units[kingLocation + 2] != Constants.EMPTY) return false;
			if ((units[kingLocation + 3] & ~Constants.MASK_COLOUR) != (Constants.ROOK | Constants.CASTLING)) return false;
			ulong shift = (0x3UL << (kingLocation + 1));
			
			if ((attackedSquares & (0x3UL << (kingLocation + 1))) != 0) return false;
			
			return true;
		}
		
		private static bool QueenSideCastle(byte location, byte[] units, ulong attackedSquares)
		{
			byte kingLocation;
			
			if ((units[location] & Constants.MASK_COLOUR) == Constants.WHITE)
			{
				kingLocation = 60;
			}
			else
			{
				kingLocation = 4;
			}
			
			if (location != kingLocation) return false;
			if (units[kingLocation - 1] != Constants.EMPTY) return false;
			if (units[kingLocation - 2] != Constants.EMPTY) return false;
			if (units[kingLocation - 3] != Constants.EMPTY) return false;
			if ((units[kingLocation - 4] & ~Constants.MASK_COLOUR) != (Constants.ROOK | Constants.CASTLING)) return false;

			if ((attackedSquares & (0x7UL << (kingLocation - 4))) != 0) return false;
			
			return true;
		}

	}
}