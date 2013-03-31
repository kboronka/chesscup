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

using System;
using ChessCup.Game.Units;

namespace ChessCup.Game
{
	/// <summary>
	/// Description of Constants.
	/// </summary>
	public class Constants
	{
		public const string STARTING_POSITION = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
		
		// board square value
		
		// status
		public const byte EMPTY			= 0x00;
		public const byte WHITE_TURN	= 0x08;

		public const byte WHITE			= 0x10;
		public const byte BLACK			= 0x20;
		public const byte EN_PASSANT	= 0x40;
		public const byte CASTLING		= 0x80;
		
		// value
		public const byte KING			= 0x01;
		public const byte QUEEN			= 0x02;
		public const byte ROOK			= 0x03;
		public const byte BISHOP		= 0x04;
		public const byte KNIGHT		= 0x05;
		public const byte PAWN			= 0x06;

		
		public const byte MASK_COLOUR	= 0x30;
		public const byte MASK_TYPE		= 0x07;
		
		#region Enum
		
		public enum UnitType
		{
			None		= 0xFF,
			WhiteKing	= 0x01,
			WhiteQueen	= 0x02,
			WhiteRook	= 0x03,
			WhiteBishop	= 0x04,
			WhiteKnight	= 0x05,
			WhitePawn	= 0x06,
			BlackKing	= 0x11,
			BlackQueen	= 0x12,
			BlackRook	= 0x13,
			BlackBishop	= 0x14,
			BlackKnight	= 0x15,
			BlackPawn	= 0x16
		}
		
		#endregion
		
		public const int FORWARD = -8;
		public const int REVERSE = 8;
		public const int LEFT = -1;
		public const int RIGHT = 1;

		public const int FORWARD_LEFT = FORWARD + LEFT;
		public const int FORWARD_RIGHT = FORWARD + RIGHT;
		public const int REVERSE_LEFT = REVERSE + LEFT;
		public const int REVERSE_RIGHT = REVERSE + RIGHT;
		
		public const int KNIGHT_UP_RIGHT = FORWARD + FORWARD + RIGHT;
		public const int KNIGHT_UP_LEFT = FORWARD + FORWARD + LEFT;
		public const int KNIGHT_DOWN_RIGHT = REVERSE + REVERSE + RIGHT;
		public const int KNIGHT_DOWN_LEFT = REVERSE + REVERSE + LEFT;
		public const int KNIGHT_LEFT_UP = LEFT + LEFT + FORWARD;
		public const int KNIGHT_LEFT_DOWN = LEFT + LEFT + REVERSE;
		public const int KNIGHT_RIGHT_UP = RIGHT + RIGHT + FORWARD;
		public const int KNIGHT_RIGHT_DOWN = RIGHT + RIGHT + REVERSE;
		
		public const int EDGE_LEFT = 0;
		public const int EDGE_RIGHT = 7;
		public const int EDGE_BOTTOM = 7;
		public const int EDGE_TOP = 0;
		
		public Constants()
		{

		}
	}
}
