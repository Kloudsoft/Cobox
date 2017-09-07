namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	partial class PictureBoxCanvas
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			this.Timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// Timer
			// 
			this.Timer.Interval = 1000;
			this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// PictureBoxCanvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "PictureBoxCanvas";
			this.Size = new System.Drawing.Size(100, 100);
			this.Load += new System.EventHandler(this.PictureBoxCanvas_Load);
			this.ClientSizeChanged += new System.EventHandler(this.PictureBoxCanvas_ClientSizeChanged);
			this.EnabledChanged += new System.EventHandler(this.PictureBoxCanvas_EnabledChanged);
			this.VisibleChanged += new System.EventHandler(this.PictureBoxCanvas_VisibleChanged);
			this.Click += new System.EventHandler(this.PictureBoxCanvas_Click);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxCanvas_Paint);
			this.DoubleClick += new System.EventHandler(this.PictureBoxCanvas_DoubleClick);
			this.Enter += new System.EventHandler(this.PictureBoxCanvas_Enter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PictureBoxCanvas_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PictureBoxCanvas_KeyPress);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PictureBoxCanvas_KeyUp);
			this.Leave += new System.EventHandler(this.PictureBoxCanvas_Leave);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxCanvas_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxCanvas_MouseDoubleClick);
			this.MouseCaptureChanged += new System.EventHandler(this.PictureBoxCanvas_MouseCaptureChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxCanvas_MouseDown);
			this.MouseEnter += new System.EventHandler(this.PictureBoxCanvas_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.PictureBoxCanvas_MouseLeave);
			this.MouseHover += new System.EventHandler(this.PictureBoxCanvas_MouseHover);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxCanvas_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxCanvas_MouseUp);
			this.StyleChanged += new System.EventHandler(this.PictureBoxCanvas_StyleChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer Timer;
	}
}
