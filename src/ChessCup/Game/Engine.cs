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

using ChessCup.GUI;
using ChessCup.Game.Units;

namespace ChessCup.Game
{
	public class Engine
	{

		#region members
		
		private List<Move> moves;

		private byte[] units;
		
		private int fullMoves;
		private int halfMoveClock;

		public List<Move> availbleMoves;
		
		#endregion
		
		#region constructors
		
		public Engine()
		{
			this.NewGame();
		}
		
		#endregion
		
		#region properties

		public List<Move> Moves
		{
			get { return this.moves; }
			set { this.moves = value; }
		}
		
		public byte[] Units
		{
			get { return this.units; }
		}

		public int Turn
		{
			get
			{
				if ((this.units[0] & Constants.WHITE_TURN) == Constants.WHITE_TURN)
					return Constants.WHITE;
				
				return Constants.BLACK;
			}
		}
		
		public int Value
		{
			get
			{
				int value = 0;
				
				for (int i = 0; i < 64; i++)
				{
					
					if (this.units[i] != Constants.EMPTY)
					{
						if (((this.units[i] & Constants.MASK_COLOUR) == Constants.WHITE) == ((this.units[0] & Constants.WHITE_TURN) == Constants.WHITE_TURN))
						{
							value += BaseUnit.GetValue(this.units[i]);
						}
						else
						{
							value -= BaseUnit.GetValue(this.units[i]);
						}
					}
				}
				
				return value;
			}
		}
		
		public int WhiteValue
		{
			get
			{
				return 0;
			}
		}
		
		public int BlackValue
		{
			get
			{
				return 0;
			}
		}
		
		public string FEN
		{
			get
			{
				string fen ="";
				// TODO: sanity checks, validate valid FEN string
				// FEN: http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
				// "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
				// "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"
				// "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2"
				// "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2"
				
				// board
				int index = 0;
				for (int i = 0; i < 64; i++)
				{
					string unit = BaseUnit.UnitToFenSymbol(this.units[i]);

					if (unit == "")
					{
						index++;
					}
					else
					{
						if (index > 0)
						{
							fen += index.ToString();
							index = 0;
						}
						
						fen += unit;
					}
					
					if (i == 63) break;

					if (((i + 1) % 8) == 0)
					{
						if (index > 0)
						{
							fen += index.ToString();
							index = 0;
						}

						fen += '/';
					}
				}
				
				fen += ' ';
				
				// turn
				if ((this.units[0] & Constants.WHITE_TURN) == Constants.WHITE_TURN) fen += 'w'; else fen += 'b';
				
				fen += ' ';
				
				// castling availability
				if ((units[63] & Constants.CASTLING) == Constants.CASTLING) fen += 'K';
				if ((units[56] & Constants.CASTLING) == Constants.CASTLING) fen += 'Q';
				if ((units[7] & Constants.CASTLING) == Constants.CASTLING) fen += 'k';
				if ((units[0] & Constants.CASTLING) == Constants.CASTLING) fen += 'q';
				
				
				if (fen[fen.Length - 1] == ' ') fen += '-';
				fen += ' ';
				
				// en passant
				for (byte i = 0; i < 64; i++)
				{
					if ((units[i] & Constants.EN_PASSANT) == Constants.EN_PASSANT)
					{
						fen += Move.IndexToAddress(i);
						break;
					}
				}
				
				if (fen[fen.Length - 1] == ' ') fen += '-';
				fen += ' ';

				// reversible moves
				fen += this.halfMoveClock.ToString();
				fen += ' ';

				fen += this.fullMoves.ToString();
				
				Program.Log("fen=" + fen);
				return fen;
			}
			
			set
			{
				Program.Log("FEN=" + value);
				string[] fenStrings = value.Split(' ');

				this.units = new byte[64];
				this.moves = new List<Move>();
				
				// TODO: sanity checks, validate valid FEN string
				if (fenStrings.Length != 6)
				{
					throw new InvalidOperationException("Invalid FEN string");
				}
				
				string fenBoard = fenStrings[0];
				string fenTurn = fenStrings[1];
				string fenCastlingAvailability = fenStrings[2];
				string fenEnPassant = fenStrings[3];
				string fenHalfMoveClock = fenStrings[4];
				string fenFullMoves = fenStrings[5];
				
				Program.Log("fenBoard = " + fenBoard);
				Program.Log("fenTurn = " + fenTurn);
				Program.Log("fenCastlingAvailability = " + fenCastlingAvailability);
				Program.Log("fenEnPassant = " + fenEnPassant);
				Program.Log("fenHalfMoveClock = " + fenHalfMoveClock);
				Program.Log("fenFullMoves = " + fenFullMoves);
				
				// board
				int index = 0;
				foreach (char symbol in fenBoard)
				{
					if ((symbol >= 'a' && symbol <= 'z') || (symbol >= 'A' && symbol <= 'Z'))
					{
						this.units[index++] = BaseUnit.FenSymbolToUnit(symbol);
					}
					else if (symbol >= '0' && symbol <= '9')
					{
						index += (symbol - '0');
					}
				}
				
				// turn
				if (fenTurn[0] == 'w') this.units[0] |= Constants.WHITE_TURN;
				
				// castling availability
				foreach (char symbol in fenCastlingAvailability)
				{
					switch (symbol)
					{
						case 'k':
							units[7] |= Constants.CASTLING;
							break;
						case 'q':
							units[0] |= Constants.CASTLING;
							break;
						case 'K':
							units[63] |= Constants.CASTLING;
							break;
						case 'Q':
							units[56] |= Constants.CASTLING;
							break;
					}
				}
				
				// en passant
				if (fenEnPassant != "-")
				{
					Program.Log("EnPassant index=" + Move.AddressToIndex(fenEnPassant).ToString());
					units[Move.AddressToIndex(fenEnPassant)] |= Constants.EN_PASSANT;
				}
				
				// half move clock
				this.halfMoveClock = int.Parse(fenHalfMoveClock);
				
				// reversible moves
				this.fullMoves = int.Parse(fenFullMoves);
				
				for (int i = 0; i < 64; i++)
				{
					if (this.units[i] != Constants.EMPTY)
						Program.Log(i.ToString("00") + " 0x" + this.units[i].ToString("X2"));
				}
				
				this.availbleMoves = Engine.UpdateMoveLists(this.units);
			}
		}
		
		#endregion
		
		#region methods

		public void NewGame()
		{
			this.moves = new List<Move>();
			this.FEN = Constants.STARTING_POSITION;
		}
		
		public void LoadGame()
		{
			// TODO: serialize engine
		}
		
		public void SaveGame()
		{
			// TODO: de-serialize engine
		}
		
		public void MovePiece(byte from, byte to)
		{
			bool capture = (units[to] != Constants.EMPTY) || (((units[from] & Constants.MASK_TYPE) == Constants.PAWN) && ((Math.Abs(from - to) % 2) != 0));
			this.moves.Add(new Move(this.units[from], from, to, capture));

			this.units = Engine.MovePiece(this.units, from, to);
			this.availbleMoves = Engine.UpdateMoveLists(this.units);

			this.halfMoveClock++;
			if ((this.units[to] & Constants.MASK_TYPE) == Constants.PAWN) this.halfMoveClock = 0;
			if (capture) this.halfMoveClock = 0;

			if ((this.units[0] & Constants.WHITE_TURN) != Constants.WHITE_TURN) this.fullMoves++;
			
			foreach (Move move in this.availbleMoves)
			{
				Program.Log("Move\t" + move.ToString() + "\t(" +  move.From.ToString() + " " + move.To.ToString() + ")");
			}
		}
		
		public static byte[] MovePiece(byte[] units, byte from, byte to)
		{
			byte[] results = new byte[64];
			
			for (byte i = 0; i < 64; i++)
			{
				results[i] = units[i];
			}
			
			
			if ((results[from] & Constants.MASK_TYPE) == Constants.KING)
			{
				// castle king side
				if (to == (from + 2))
				{
					results[from + 1] = results[from + 3];
					results[from + 3] = Constants.EMPTY;
				}

				// castle queen side
				if (to == (from - 2))
				{
					results[from - 1] = results[from - 4];
					results[from - 4] = Constants.EMPTY;
				}
				
				// remove CASTLING status
				if ((results[from] & Constants.MASK_COLOUR) == Constants.WHITE)
				{
					if ((results[56] & Constants.CASTLING) == Constants.CASTLING) results[56] ^= Constants.CASTLING;
					if ((results[63] & Constants.CASTLING) == Constants.CASTLING) results[63] ^= Constants.CASTLING;
				}
				else
				{
					if ((results[0] & Constants.CASTLING) == Constants.CASTLING) results[0] ^= Constants.CASTLING;
					if ((results[7] & Constants.CASTLING) == Constants.CASTLING) results[7] ^= Constants.CASTLING;					
				}
			}
			
			if ((results[from] & Constants.MASK_TYPE) == Constants.ROOK)
			{
				// remove CASTLING status
				if ((results[from] & Constants.CASTLING) == Constants.CASTLING) results[from] ^= Constants.CASTLING;
			}
			
			// capture en passant pawn
			if ((results[from] & Constants.MASK_TYPE) == Constants.PAWN)
			{
				if (results[to] == Constants.EN_PASSANT)
				{
					int enPassant;
					
					if ((results[from] & Constants.MASK_COLOUR) == Constants.WHITE)
					{
						enPassant = to - Constants.FORWARD;
					}
					else
					{
						enPassant = to - Constants.REVERSE;
					}
					
					results[enPassant] = Constants.EMPTY;
				}
			}
			
			// reset en passant status
			for (int i = 0; i < 64; i++)
			{
				if ((results[i] & Constants.EN_PASSANT) == Constants.EN_PASSANT)
				{
					results[i] ^= Constants.EN_PASSANT;
				}
			}
			
			// move unit (and/or capture)
			results[to] = results[from];
			results[from] = Constants.EMPTY;

			// set en passant status
			if (((results[to] & Constants.MASK_TYPE) == Constants.PAWN) && (Math.Abs(from - to) == 16))
			{
				if (from >= 48)
				{
					results[from - 8] |= Constants.EN_PASSANT;
				}
				else
				{
					results[from + 8] |= Constants.EN_PASSANT;
				}
			}
						
			// other player's turn
			Engine.UpdateTurnFlag(ref results);
			
			return results;
		}
		
		private static void UpdateTurnFlag(ref byte[] units)
		{
			if ((units[0] & Constants.WHITE_TURN) == Constants.WHITE_TURN)
			{
				units[0] ^= Constants.WHITE_TURN;
			}
			else
			{
				units[0] |= Constants.WHITE_TURN;
			}
		}
		
		private static List<Move> UpdateMoveLists(byte[] units)
		{
			if ((units[0] & Constants.WHITE_TURN) == Constants.WHITE_TURN)
			{
				ulong attackedSquares = FindSquaresUnderAttack(units, Constants.BLACK);
				return MoveList(Engine.FindAvailbleMoves(units, Constants.WHITE, attackedSquares, 1), units);
			}
			else
			{
				ulong attackedSquares = FindSquaresUnderAttack(units, Constants.WHITE);
				return MoveList(Engine.FindAvailbleMoves(units, Constants.BLACK, attackedSquares, 1), units);
			}
		}
		
		private static byte[,] FindAvailbleMoves(byte[] units, byte turn, ulong attackedSquares, byte depth)
		{
			byte[,] moves = new byte[2,240];
			byte index = 0;
			
			for (byte i = 0; i < 64; i++)
			{
				byte unitColour = (byte)(units[i] & Constants.MASK_COLOUR);
				if (unitColour != turn) continue;

				List<byte> destinations;
				byte unitType = (byte)(units[i] & Constants.MASK_TYPE);
				
				switch (unitType)
				{
					case Constants.KING:
						destinations = King.LegalMoves(i, units, attackedSquares);
						break;
					case Constants.QUEEN:
						destinations = Queen.LegalMoves(i, units);
						break;
					case Constants.ROOK:
						destinations = Rook.LegalMoves(i, units);
						break;
					case Constants.BISHOP:
						destinations = Bishop.LegalMoves(i, units);
						break;
					case Constants.KNIGHT:
						destinations = Knight.LegalMoves(i, units);
						break;
					case Constants.PAWN:
						destinations = Pawn.LegalMoves(i, units);
						break;
					default:
						continue;
				}
				
				foreach (byte to in destinations)
				{
					// filter moves that allow king captures
					byte[] tempUnits = MovePiece(units, i, to);
					
					byte enemyturn = Constants.BLACK;
					if (turn != Constants.WHITE)
						enemyturn = Constants.WHITE;
					
					ulong attacked = 0;
					
					if (depth != 0)
					{
						attacked = Engine.FindSquaresUnderAttack(tempUnits, enemyturn);
					}
					
					byte kingLocation = Engine.FindKing(tempUnits, turn);
					
					if ((attacked & (1UL << kingLocation)) == 0)
					{
						moves[0, index] = i;
						moves[1, index++] = to;
					}
				}
			}
			
			return moves;
		}
		
		private static ulong FindSquaresUnderAttack(byte [] units, byte turn)
		{
			ulong result = 0;
			byte[,] moves = Engine.FindAvailbleMoves(units, turn, 0, 0);
			
			for (byte i = 0; i < 240; i++)
			{
				if (moves[0,i] == moves[1,i]) break;
				result |= 1UL << moves[1,i];
			}
			
			return result;
		}

		private static byte FindKing(byte [] units, byte turn)
		{
			for (byte i = 0; i < 64; i++)
			{
				if ((units[i] & (Constants.MASK_TYPE | Constants.MASK_COLOUR)) == (Constants.KING | turn))
					return i;
			}
			
			return 64;
		}
		
		private static List<Move> MoveList(byte[,] moves, byte[] units)
		{
			List<Move> moveList = new List<Move>();
			
			for (byte i = 0; i < 240; i++)
			{
				byte from = moves[0,i];
				byte to = moves[1,i];
				if (from == to) break;
				
				moveList.Add(new Move(units[from], from, to, (units[to] != Constants.EMPTY)));
			}
			
			return moveList;
		}
		
		#endregion
		
		#region events
		
		#endregion

	}
}
