/*
 * User: Kevin Boronka (kboronka@gmail.com)
 * Date: 1/30/2010
 * Time: 9:41 AM
 */
namespace ChessCup.GUI
{
	partial class Square
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
			this.SuspendLayout();
			// 
			// Square
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Tan;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "Square";
			this.Size = new System.Drawing.Size(108, 93);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.square_Paint);
			this.ResumeLayout(false);
		}
	}
}
