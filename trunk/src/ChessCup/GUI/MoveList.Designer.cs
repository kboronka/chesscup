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

namespace ChessCup.GUI
{
	partial class MoveList
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.list = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.previous = new System.Windows.Forms.Button();
			this.first = new System.Windows.Forms.Button();
			this.last = new System.Windows.Forms.Button();
			this.next = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.list.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3});
			this.list.FullRowSelect = true;
			this.list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.list.HideSelection = false;
			this.list.Location = new System.Drawing.Point(0, 0);
			this.list.MultiSelect = false;
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(120, 352);
			this.list.TabIndex = 0;
			this.list.UseCompatibleStateImageBehavior = false;
			this.list.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 23;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 42;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Width = 42;
			// 
			// previous
			// 
			this.previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.previous.Enabled = false;
			this.previous.Location = new System.Drawing.Point(31, 325);
			this.previous.Name = "previous";
			this.previous.Size = new System.Drawing.Size(23, 23);
			this.previous.TabIndex = 5;
			this.previous.Text = "<";
			this.previous.UseVisualStyleBackColor = true;
			this.previous.Visible = false;
			// 
			// first
			// 
			this.first.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.first.Enabled = false;
			this.first.Location = new System.Drawing.Point(2, 325);
			this.first.Name = "first";
			this.first.Size = new System.Drawing.Size(23, 23);
			this.first.TabIndex = 4;
			this.first.Text = "|<";
			this.first.UseVisualStyleBackColor = true;
			this.first.Visible = false;
			// 
			// last
			// 
			this.last.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.last.Enabled = false;
			this.last.Location = new System.Drawing.Point(93, 325);
			this.last.Name = "last";
			this.last.Size = new System.Drawing.Size(23, 23);
			this.last.TabIndex = 7;
			this.last.Text = ">|";
			this.last.UseVisualStyleBackColor = true;
			this.last.Visible = false;
			// 
			// next
			// 
			this.next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.next.Enabled = false;
			this.next.Location = new System.Drawing.Point(64, 325);
			this.next.Name = "next";
			this.next.Size = new System.Drawing.Size(23, 23);
			this.next.TabIndex = 6;
			this.next.Text = ">";
			this.next.UseVisualStyleBackColor = true;
			this.next.Visible = false;
			// 
			// MoveList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.last);
			this.Controls.Add(this.next);
			this.Controls.Add(this.previous);
			this.Controls.Add(this.first);
			this.Controls.Add(this.list);
			this.MinimumSize = new System.Drawing.Size(115, 100);
			this.Name = "MoveList";
			this.Size = new System.Drawing.Size(120, 352);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button next;
		private System.Windows.Forms.Button last;
		private System.Windows.Forms.Button first;
		private System.Windows.Forms.Button previous;
		private System.Windows.Forms.ListView list;
	}
}
