/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 11/21/2010
 * Time: 9:19 PM
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace ChessCup.Game.Units
{
	public class Queen : BaseUnit
	{

		public static List<byte> LegalMoves(byte location, byte[] units)
		{
			List<byte> moves = new List<byte>();
			int[] possibleMoves = {FORWARD_LEFT, FORWARD_RIGHT, REVERSE_LEFT, REVERSE_RIGHT, FORWARD, REVERSE, LEFT, RIGHT};

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
				}
			}
			
			return moves;
		}

	}
}