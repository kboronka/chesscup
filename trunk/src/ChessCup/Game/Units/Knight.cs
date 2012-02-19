/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 11/20/2010
 * Time: 5:51 PM
 */
using System;
using System.Collections;
using System.Collections.Generic;

using ChessCup;
using ChessCup.GUI;
namespace ChessCup.Game.Units
{
	public class Knight : BaseUnit
	{

		public static List<byte> LegalMoves(byte location, byte[] units)
		{
			List<byte> moves = new List<byte>();
			int[] possibleMoves = {KNIGHT_UP_RIGHT, KNIGHT_UP_LEFT, KNIGHT_DOWN_RIGHT, KNIGHT_DOWN_LEFT, KNIGHT_LEFT_UP, KNIGHT_LEFT_DOWN, KNIGHT_RIGHT_UP, KNIGHT_RIGHT_DOWN};
			
			foreach (int move in possibleMoves)
			{
				if (!BaseUnit.ValidateMove(location, move)) continue;
				
				int target = location + move;

				if (units[target] == Constants.EMPTY)
				{
					moves.Add((byte)target);
					continue;
				}
				else if ((units[target] & Constants.MASK_COLOUR) != (units[location] & Constants.MASK_COLOUR))
				{
					moves.Add((byte)target);
					continue;
				}
			}
			
			return moves;
		}

	}
}