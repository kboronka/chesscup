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
	public class Pawn : BaseUnit
	{

		public static List<byte> LegalMoves(byte location, byte[] units)
		{
			List<byte> moves = new List<byte>();
			int direction;

			if ((units[location] & Constants.MASK_COLOUR) == Constants.WHITE)
			{
				direction = FORWARD;
			}
			else
			{
				direction = REVERSE;
			}
			
			int target = location + direction;
			int column = BaseUnit.GetColumn(location);
			
			// capture to right
			if (ValidateMove(location, direction + 1))
			{
				if (units[target + 1] != Constants.EMPTY)
				{
					if ((units[target] & Constants.MASK_COLOUR) != (units[location] & Constants.MASK_COLOUR))
					{
						moves.Add((byte)(target + 1));
					}
				}
				else if ((units[target + 1 - direction] & Constants.EN_PASSANT) == Constants.EN_PASSANT)
				{
					moves.Add((byte)(target + 1));
				}
			}
			
			// capture to left
			if (ValidateMove(location, direction - 1))
			{
				if (units[target - 1] != Constants.EMPTY)
				{
					if ((units[target] & Constants.MASK_COLOUR) != (units[location] & Constants.MASK_COLOUR))
					{
						moves.Add((byte)(target - 1));
					}
				}
				else if ((units[target - 1 - direction] & Constants.EN_PASSANT) == Constants.EN_PASSANT)
				{
					moves.Add((byte)(target - 1));
				}
			}
			

			// moves
			if (ValidateMove(location, direction))
			{
				if (units[target] == Constants.EMPTY)
				{
					moves.Add((byte)target);
					byte row = BaseUnit.GetRow(location);
					if ((row == 6 && ((units[location] & Constants.MASK_COLOUR) == Constants.WHITE)) ||
					    (row == 1 && ((units[location] & Constants.MASK_COLOUR) == Constants.BLACK)))
					{
						target += direction;
						if (ValidateMove(location, direction * 2))
						{
							if (units[target] == Constants.EMPTY)
							{
								moves.Add((byte)target);
							}
						}
					}
				}
			}
			
			return moves;
		}

	}
}