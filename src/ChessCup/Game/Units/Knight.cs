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