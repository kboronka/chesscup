/* Copyright (C) 2013 Kevin Boronka
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