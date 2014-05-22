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
using System.Collections;
using System.Collections.Generic;

using ChessCup.Game;

namespace ChessCup.Game.Units
{
	public abstract class BaseUnit
	{
		#region constants
		
		protected const int FORWARD = -8;
		protected const int REVERSE = 8;
		protected const int LEFT = -1;
		protected const int RIGHT = 1;

		protected const int FORWARD_LEFT = FORWARD + LEFT;
		protected const int FORWARD_RIGHT = FORWARD + RIGHT;
		protected const int REVERSE_LEFT = REVERSE + LEFT;
		protected const int REVERSE_RIGHT = REVERSE + RIGHT;
		
		protected const int KNIGHT_UP_RIGHT = FORWARD + FORWARD + RIGHT;
		protected const int KNIGHT_UP_LEFT = FORWARD + FORWARD + LEFT;
		protected const int KNIGHT_DOWN_RIGHT = REVERSE + REVERSE + RIGHT;
		protected const int KNIGHT_DOWN_LEFT = REVERSE + REVERSE + LEFT;
		protected const int KNIGHT_LEFT_UP = LEFT + LEFT + FORWARD;
		protected const int KNIGHT_LEFT_DOWN = LEFT + LEFT + REVERSE;
		protected const int KNIGHT_RIGHT_UP = RIGHT + RIGHT + FORWARD;
		protected const int KNIGHT_RIGHT_DOWN = RIGHT + RIGHT + REVERSE;
		
		protected const int EDGE_LEFT = 0;
		protected const int EDGE_RIGHT = 7;
		protected const int EDGE_BOTTOM = 7;
		protected const int EDGE_TOP = 0;

		#endregion

		#region methods
		
		public static byte FenSymbolToUnit(char symbol)
		{
			switch (symbol)
			{
				case 'r':
					return Constants.ROOK	| Constants.BLACK;
				case 'n':
					return Constants.KNIGHT	| Constants.BLACK;
				case 'b':
					return Constants.BISHOP	| Constants.BLACK;
				case 'q':
					return Constants.QUEEN	| Constants.BLACK;
				case 'k':
					return Constants.KING	| Constants.BLACK;
				case 'p':
					return Constants.PAWN	| Constants.BLACK;
				case 'R':
					return Constants.ROOK	| Constants.WHITE;
				case 'N':
					return Constants.KNIGHT	| Constants.WHITE;
				case 'B':
					return Constants.BISHOP	| Constants.WHITE;
				case 'Q':
					return Constants.QUEEN	| Constants.WHITE;
				case 'K':
					return Constants.KING	| Constants.WHITE;
				case 'P':
					return Constants.PAWN	| Constants.WHITE;
				default:
					return Constants.EMPTY;
			}
		}
		
		public static string UnitToFenSymbol(byte unit)
		{
			unit &= (Constants.MASK_TYPE | Constants.MASK_COLOUR);
			
			switch (unit)
			{
				case Constants.ROOK | Constants.BLACK:
					return "r";					
				case Constants.KNIGHT | Constants.BLACK:
					return "n";
				case Constants.BISHOP | Constants.BLACK:
					return "b";
				case Constants.QUEEN | Constants.BLACK:
					return "q";
				case Constants.KING | Constants.BLACK:
					return "k";
				case Constants.PAWN | Constants.BLACK:
					return "p";
				case Constants.ROOK | Constants.WHITE:
					return "R";
				case Constants.KNIGHT | Constants.WHITE:
					return "N";
				case Constants.BISHOP | Constants.WHITE:
					return "B";
				case Constants.QUEEN | Constants.WHITE:
					return "Q";
				case Constants.KING | Constants.WHITE:
					return "K";
				case Constants.PAWN | Constants.WHITE:
					return "P";
				default:
					return "";
			}
		}		

		public static byte GetValue(byte unit)
		{
			// max value = 9q + 2r + 2k + 2b = (81 + 10 + 6 + 6) = 103

			if ((unit & Constants.MASK_TYPE) == Constants.KING) return 152;
			if ((unit & Constants.MASK_TYPE) == Constants.QUEEN) return 9;
			if ((unit & Constants.MASK_TYPE) == Constants.ROOK) return 5;
			if ((unit & Constants.MASK_TYPE) == Constants.BISHOP) return 3;
			if ((unit & Constants.MASK_TYPE) == Constants.KNIGHT) return 3;
			if ((unit & Constants.MASK_TYPE) == Constants.PAWN) return 1;
			
			return 0;
		}
		
		public static string GetSymbol(byte unit)
		{
			if ((unit & Constants.MASK_TYPE) == Constants.KING) return "K";
			if ((unit & Constants.MASK_TYPE) == Constants.QUEEN) return "Q";
			if ((unit & Constants.MASK_TYPE) == Constants.ROOK) return "R";
			if ((unit & Constants.MASK_TYPE) == Constants.BISHOP) return "B";
			if ((unit & Constants.MASK_TYPE) == Constants.KNIGHT) return "N";
			if ((unit & Constants.MASK_TYPE) == Constants.PAWN) return "";
			
			return "";
		}

		protected static int GetColour(Constants.UnitType type)
		{
			if ((int) type > Constants.BLACK)
			{
				return Constants.BLACK;
			}
			else
			{
				return Constants.WHITE;
			}
		}
		
		protected static bool ValidateMove(int target, int move)
		{
			if ((target + move) < 0 || (target + move) > 63) return false;
			
			int row = BaseUnit.GetRow((byte)target);
			int column = BaseUnit.GetColumn((byte)target);

			// standard moves
			if ((move == FORWARD) || (move == FORWARD_LEFT) || (move == FORWARD_RIGHT))
			{
				if (row == 0) return false;
			}

			if ((move == REVERSE) || (move == REVERSE_LEFT) || (move == REVERSE_RIGHT))
			{
				if (row == 7) return false;
			}

			if ((move == LEFT) || (move == FORWARD_LEFT) || (move == REVERSE_LEFT))
			{
				if (column == 0) return false;
			}

			if ((move == RIGHT) || (move == FORWARD_RIGHT) || (move == REVERSE_RIGHT))
			{
				if (column == 7) return false;
			}
			
			
			// knight moves
			if ((move == KNIGHT_UP_RIGHT) || (move == KNIGHT_UP_LEFT))
			{
				if (row <= 1) return false;
			}
			
			if ((move == KNIGHT_DOWN_RIGHT) || (move == KNIGHT_DOWN_LEFT))
			{
				if (row >= 6) return false;
			}

			if ((move == KNIGHT_UP_RIGHT) || (move == KNIGHT_DOWN_RIGHT))
			{
				if (column == 7) return false;
			}
			
			if ((move == KNIGHT_UP_LEFT) || (move == KNIGHT_DOWN_LEFT))
			{
				if (column == 0) return false;
			}
			
			if ((move == KNIGHT_RIGHT_UP) || (move == KNIGHT_LEFT_UP))
			{
				if (row == 0) return false;
			}

			if ((move == KNIGHT_RIGHT_DOWN) || (move == KNIGHT_LEFT_DOWN))
			{
				if (row == 7) return false;
			}
			
			if ((move == KNIGHT_RIGHT_UP) || (move == KNIGHT_RIGHT_DOWN))
			{
				if (column >= 6) return false;
			}
			
			if ((move == KNIGHT_LEFT_UP) || (move == KNIGHT_LEFT_DOWN))
			{
				if (column <= 1) return false;
			}

			// TODO: move exposes king to attack?
			
			return true;
		}
		
		protected static byte GetRow(byte target)
		{
			return (byte)(target / 8);
		}
		
		protected static byte GetColumn(byte target)
		{
			return (byte)(target % 8);
		}
		
		#endregion
	}
}
