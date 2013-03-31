/* Copyright (C) 2013 Kevin Boronka
 * 
 * software is distributed under the BSD license
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

namespace ChessCup
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.board = new ChessCup.GUI.Board();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.turnStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.scoreStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.copyrightStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.fen = new System.Windows.Forms.TextBox();
			this.loadFEN = new System.Windows.Forms.Button();
			this.a = new System.Windows.Forms.Label();
			this.b = new System.Windows.Forms.Label();
			this.d = new System.Windows.Forms.Label();
			this.c = new System.Windows.Forms.Label();
			this.h = new System.Windows.Forms.Label();
			this.g = new System.Windows.Forms.Label();
			this.f = new System.Windows.Forms.Label();
			this.e = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.moveList1 = new ChessCup.GUI.MoveList();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// board
			// 
			this.board.BackColor = System.Drawing.Color.DimGray;
			this.board.Engine = null;
			this.board.Location = new System.Drawing.Point(9, 9);
			this.board.Margin = new System.Windows.Forms.Padding(0);
			this.board.Name = "board";
			this.board.Size = new System.Drawing.Size(352, 352);
			this.board.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.turnStatus,
									this.scoreStatus,
									this.copyrightStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 399);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(531, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// turnStatus
			// 
			this.turnStatus.Name = "turnStatus";
			this.turnStatus.Size = new System.Drawing.Size(61, 17);
			this.turnStatus.Text = "turnStatus";
			// 
			// scoreStatus
			// 
			this.scoreStatus.Name = "scoreStatus";
			this.scoreStatus.Size = new System.Drawing.Size(67, 17);
			this.scoreStatus.Text = "scoreStatus";
			// 
			// copyrightStatus
			// 
			this.copyrightStatus.Name = "copyrightStatus";
			this.copyrightStatus.Size = new System.Drawing.Size(388, 17);
			this.copyrightStatus.Spring = true;
			this.copyrightStatus.Text = "copyrightStatus";
			this.copyrightStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fen
			// 
			this.fen.Location = new System.Drawing.Point(9, 374);
			this.fen.Name = "fen";
			this.fen.Size = new System.Drawing.Size(449, 20);
			this.fen.TabIndex = 3;
			this.fen.Enter += new System.EventHandler(this.FenEnter);
			// 
			// loadFEN
			// 
			this.loadFEN.Location = new System.Drawing.Point(464, 374);
			this.loadFEN.Name = "loadFEN";
			this.loadFEN.Size = new System.Drawing.Size(64, 20);
			this.loadFEN.TabIndex = 4;
			this.loadFEN.Text = "Load FEN";
			this.loadFEN.UseVisualStyleBackColor = true;
			this.loadFEN.Click += new System.EventHandler(this.LoadFENClick);
			// 
			// a
			// 
			this.a.AutoSize = true;
			this.a.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.a.Location = new System.Drawing.Point(23, 361);
			this.a.Name = "a";
			this.a.Size = new System.Drawing.Size(9, 10);
			this.a.TabIndex = 5;
			this.a.Text = "a";
			// 
			// b
			// 
			this.b.AutoSize = true;
			this.b.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.b.Location = new System.Drawing.Point(68, 361);
			this.b.Name = "b";
			this.b.Size = new System.Drawing.Size(9, 10);
			this.b.TabIndex = 6;
			this.b.Text = "b";
			// 
			// d
			// 
			this.d.AutoSize = true;
			this.d.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.d.Location = new System.Drawing.Point(157, 361);
			this.d.Name = "d";
			this.d.Size = new System.Drawing.Size(9, 10);
			this.d.TabIndex = 8;
			this.d.Text = "d";
			// 
			// c
			// 
			this.c.AutoSize = true;
			this.c.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.c.Location = new System.Drawing.Point(112, 361);
			this.c.Name = "c";
			this.c.Size = new System.Drawing.Size(9, 10);
			this.c.TabIndex = 7;
			this.c.Text = "c";
			// 
			// h
			// 
			this.h.AutoSize = true;
			this.h.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.h.Location = new System.Drawing.Point(333, 361);
			this.h.Name = "h";
			this.h.Size = new System.Drawing.Size(9, 10);
			this.h.TabIndex = 12;
			this.h.Text = "h";
			// 
			// g
			// 
			this.g.AutoSize = true;
			this.g.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.g.Location = new System.Drawing.Point(289, 361);
			this.g.Name = "g";
			this.g.Size = new System.Drawing.Size(10, 10);
			this.g.TabIndex = 11;
			this.g.Text = "g";
			// 
			// f
			// 
			this.f.AutoSize = true;
			this.f.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.f.Location = new System.Drawing.Point(246, 361);
			this.f.Name = "f";
			this.f.Size = new System.Drawing.Size(7, 10);
			this.f.TabIndex = 10;
			this.f.Text = "f";
			// 
			// e
			// 
			this.e.AutoSize = true;
			this.e.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.e.Location = new System.Drawing.Point(201, 361);
			this.e.Name = "e";
			this.e.Size = new System.Drawing.Size(9, 10);
			this.e.TabIndex = 9;
			this.e.Text = "e";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 335);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(9, 10);
			this.label1.TabIndex = 13;
			this.label1.Text = "1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 293);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(9, 10);
			this.label2.TabIndex = 14;
			this.label2.Text = "2";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 203);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(9, 10);
			this.label3.TabIndex = 16;
			this.label3.Text = "4";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(0, 245);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(9, 10);
			this.label4.TabIndex = 15;
			this.label4.Text = "3";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(0, 115);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(9, 10);
			this.label5.TabIndex = 18;
			this.label5.Text = "6";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(0, 157);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(9, 10);
			this.label6.TabIndex = 17;
			this.label6.Text = "5";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(0, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(9, 10);
			this.label7.TabIndex = 20;
			this.label7.Text = "8";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(0, 66);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(9, 10);
			this.label8.TabIndex = 19;
			this.label8.Text = "7";
			// 
			// moveList1
			// 
			this.moveList1.Location = new System.Drawing.Point(381, 9);
			this.moveList1.MinimumSize = new System.Drawing.Size(115, 100);
			this.moveList1.Name = "moveList1";
			this.moveList1.Size = new System.Drawing.Size(147, 352);
			this.moveList1.TabIndex = 21;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(531, 421);
			this.Controls.Add(this.moveList1);
			this.Controls.Add(this.h);
			this.Controls.Add(this.g);
			this.Controls.Add(this.f);
			this.Controls.Add(this.e);
			this.Controls.Add(this.d);
			this.Controls.Add(this.c);
			this.Controls.Add(this.b);
			this.Controls.Add(this.a);
			this.Controls.Add(this.loadFEN);
			this.Controls.Add(this.fen);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.board);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(384, 404);
			this.Name = "MainForm";
			this.Text = "ChessCup";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private ChessCup.GUI.MoveList moveList1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label e;
		private System.Windows.Forms.Label f;
		private System.Windows.Forms.Label g;
		private System.Windows.Forms.Label h;
		private System.Windows.Forms.Label c;
		private System.Windows.Forms.Label d;
		private System.Windows.Forms.Label b;
		private System.Windows.Forms.Label a;
		private System.Windows.Forms.Button loadFEN;
		private System.Windows.Forms.TextBox fen;
		private System.Windows.Forms.ToolStripStatusLabel scoreStatus;
		private System.Windows.Forms.ToolStripStatusLabel turnStatus;
		private System.Windows.Forms.ToolStripStatusLabel copyrightStatus;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private ChessCup.GUI.Board board;
	}
}
