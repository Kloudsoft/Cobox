using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HouseOfSynergy.PowerTools.Library.Signals.Generators;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public sealed partial class PictureBoxCanvas:
		UserControl
	{
		public const int DefaultBufferLength = 5000;
		public const int DefaultBufferWriteLength = 1000;
		public const int DefaultIntervalThreadReadMilliseconds = 100;
		public const int DefaultIntervalThreadWriteMilliseconds = 200;

		private Graph Graph = null;
		private Thread Thread = null;
		private bool Closing = false;
		private bool Terminate = true;
		private readonly TimeSpan Interval = TimeSpan.Zero;

		public PictureBoxCanvas ()
		{
			this.InitializeComponent();

			this.Thread = new Thread(new ThreadStart(this.FeedData));
			this.Graph = new Graph(PictureBoxCanvas.DefaultBufferLength);

			this.Timer.Interval = PictureBoxCanvas.DefaultIntervalThreadReadMilliseconds;
			this.Interval = TimeSpan.FromMilliseconds(PictureBoxCanvas.DefaultIntervalThreadWriteMilliseconds);
		}

		private void PictureBoxCanvas_Load (object sender, EventArgs e)
		{
			if (!this.DesignMode)
			{
				((Form) this.Parent).FormClosing += this.PictureBoxCanvas_FormClosing;

				this.Terminate = false;
				this.Thread.Start();

				this.Timer.Start();
			}
		}

		private void PictureBoxCanvas_ClientSizeChanged (object sender, EventArgs e)
		{
			if (!this.DesignMode)
			{
				if ((this.Graph != null) && (!this.Closing))
				{
					this.Graph.SetBounds(this.ClientRectangle);
				}
			}
		}

		private void PictureBoxCanvas_StyleChanged (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_VisibleChanged (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_EnabledChanged (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_Enter (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_Leave (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_KeyDown (object sender, KeyEventArgs e)
		{
		}

		private void PictureBoxCanvas_KeyUp (object sender, KeyEventArgs e)
		{
		}

		private void PictureBoxCanvas_KeyPress (object sender, KeyPressEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseEnter (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseDown (object sender, MouseEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseHover (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseMove (object sender, MouseEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseUp (object sender, MouseEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseLeave (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseClick (object sender, MouseEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseDoubleClick (object sender, MouseEventArgs e)
		{
		}

		private void PictureBoxCanvas_MouseCaptureChanged (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_Click (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_DoubleClick (object sender, EventArgs e)
		{
		}

		private void PictureBoxCanvas_Paint (object sender, PaintEventArgs e)
		{
			if (!this.DesignMode)
			{
				if (!this.Closing)
				{
					e.Graphics.CompositingMode = CompositingMode.SourceOver;
					e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
					e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
					e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

					//e.Graphics.DrawString(DateTime.Now.ToString(), this.Font, Brushes.Black, 10, 10);
					e.Graphics.DrawString(this.Graph.BufferLength.ToString(), this.Font, Brushes.Black, 10, 10);

					this.Graph.Render(e.Graphics);
				}
			}
		}

		private void PictureBoxCanvas_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (!this.DesignMode)
			{
				this.Timer.Stop();
				this.Closing = true;

				try
				{
					if (this.Parent != null)
					{
						((Form) this.Parent).FormClosing -= this.PictureBoxCanvas_FormClosing;
					}
				}
				finally
				{
				}

				this.Terminate = true;
				if (this.Thread != null)
				{
					try
					{
						this.Terminate = true;
						this.Thread.Join(TimeSpan.FromSeconds(1));
					}
					catch
					{
						try { this.Thread.Abort(); }
						finally { this.Thread = null; }
					}
				}

				if (this.Graph == null)
				{
					try { this.Graph.Dispose(); }
					finally { this.Graph = null; }
				}
			}
		}

		private void Timer_Tick (object sender, EventArgs e)
		{
			if (!this.DesignMode)
			{
				this.Invalidate();
			}
		}

		private static readonly float SampleRate = 192000;
		public static readonly Random Random = new Random();
		private readonly float [] Buffer = new float [PictureBoxCanvas.DefaultBufferWriteLength];
		private readonly SineSignalGenerator SineSignalGenerator = new SineSignalGenerator(PictureBoxCanvas.SampleRate, 220, 1, 0);
		private readonly MultiChannelSignalGenerator MultiChannelSignalGenerator = new MultiChannelSignalGenerator
		(
			PictureBoxCanvas.SampleRate,
			new ISignalGenerator<float> []
			{
				new SineSignalGenerator(PictureBoxCanvas.SampleRate, PictureBoxCanvas.Random.Next(10, 1000), 0.8, 0),
				new SineSignalGenerator(PictureBoxCanvas.SampleRate, PictureBoxCanvas.Random.Next(10, 1000), 0.8, 0),
				new SineSignalGenerator(PictureBoxCanvas.SampleRate, PictureBoxCanvas.Random.Next(10, 1000), 0.8, 0),
				new SineSignalGenerator(PictureBoxCanvas.SampleRate, PictureBoxCanvas.Random.Next(10, 1000), 0.8, 0),
				new SineSignalGenerator(PictureBoxCanvas.SampleRate, PictureBoxCanvas.Random.Next(10, 1000), 0.8, 0),
				//new SineSignalGenerator(PictureBoxCanvas.SampleRate, 21,  1.0, 0),
				//new SineSignalGenerator(PictureBoxCanvas.SampleRate, 296, 1.0, 0),
				//new SineSignalGenerator(PictureBoxCanvas.SampleRate, 655, 1.0, 0),
				//new SineSignalGenerator(PictureBoxCanvas.SampleRate, 969, 1.0, 0),
				//new SineSignalGenerator(PictureBoxCanvas.SampleRate, 926, 1.0, 0),
			}
		);

		private void FeedData ()
		{
			try
			{
				do
				{
					if (this.Closing) { break; }
					if (this.Terminate) { break; }

					//this.SineSignalGenerator.Read(this.Buffer, 0, this.Buffer.Length);
					this.MultiChannelSignalGenerator.Read(this.Buffer, 0, this.Buffer.Length);
					this.Graph.Write(this.Buffer);

					Thread.Sleep(this.Interval);
				}
				while (true);
			}
			catch (Exception e)
			{
				this.Invoke(new Action(() => MessageBox.Show(e.ToString())));
			}

			this.Terminate = true;
		}

		#region Interface: IDisposable.

		//====================================================================================================
		// Interface: IDisposable.
		//====================================================================================================

		protected sealed override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				this.components.Dispose();
			}

			if (disposing)
			{
				// Managed.

				try
				{
					if (this.Parent != null)
					{
						((Form) this.Parent).FormClosing -= this.PictureBoxCanvas_FormClosing;
					}
				}
				finally
				{
				}

				this.Terminate = true;
				if (this.Thread != null)
				{
					try
					{
						this.Terminate = true;
						this.Thread.Join(TimeSpan.FromSeconds(1));
					}
					catch
					{
						try { this.Thread.Abort(); }
						finally { this.Thread = null; }
					}
				}

				if (this.Graph == null)
				{
					try { this.Graph.Dispose(); }
					finally { this.Graph = null; }
				}
			}

			base.Dispose(disposing);
		}

		#endregion Interface: IDisposable.
	}
}