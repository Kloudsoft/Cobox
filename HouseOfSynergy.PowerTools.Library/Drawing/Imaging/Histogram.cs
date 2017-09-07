using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Drawing.Imaging
{
	public sealed class Histogram:
		ICloneable<Histogram>,
		ICopyable<Histogram>,
		IPersistBinary<Histogram>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private int _MaxA = 0;
		private int _MaxR = 0;
		private int _MaxG = 0;
		private int _MaxB = 0;
		private int _MaxT = 0;

		private int [] _A = null;
		private int [] _R = null;
		private int [] _G = null;
		private int [] _B = null;

		private int _Width = 0;
		private int _Height = 0;
		private int _Stride = 0;
		private Rectangle _Bounds = Rectangle.Empty;

		#endregion Members.

		#region Constructors, Initializers, Destructor & Finalizers.

		//====================================================================================================
		// Constructors, Initializers, Destructor & Finalizers.
		//====================================================================================================

		public Histogram ()
		{
			this._A = new int [256];
			this._R = new int [256];
			this._G = new int [256];
			this._B = new int [256];

			this.Initialize();
		}

		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Initialize ()
		{
			this._MaxA = 0;
			this._MaxR = 0;
			this._MaxG = 0;
			this._MaxB = 0;
			this._MaxT = 0;

			for (int i = 0; i < this._A.Length; i++)
				this._A [i] = 0;
			for (int i = 0; i < this._R.Length; i++)
				this._R [i] = 0;
			for (int i = 0; i < this._G.Length; i++)
				this._G [i] = 0;
			for (int i = 0; i < this._B.Length; i++)
				this._B [i] = 0;
		}

		#endregion Constructors, Initializers, Destructor & Finalizers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public int [] A { get { return (this._A); } }
		public int [] R { get { return (this._R); } }
		public int [] G { get { return (this._G); } }
		public int [] B { get { return (this._B); } }

		public int MaxA { get { return (this._MaxA); } }
		public int MaxR { get { return (this._MaxR); } }
		public int MaxG { get { return (this._MaxG); } }
		public int MaxB { get { return (this._MaxB); } }

		public int MaxT { get { return (this._MaxT); } }

		/// <summary>
		/// The image width for the histogram source.
		/// </summary>
		public int Width { get { return (this._Width); } }

		/// <summary>
		/// The image height for the histogram source.
		/// </summary>
		public int Height { get { return (this._Height); } }

		/// <summary>
		/// The image stride for the histogram source.
		/// </summary>
		public int Stride { get { return (this._Stride); } }

		/// <summary>
		/// The image size for the histogram source.
		/// </summary>
		public Size Size { get { return (new Size(this._Width, this._Height)); } }

		/// <summary>
		/// The bounds of the histogram source image area to compute.
		/// </summary>
		public Rectangle Bounds { get { return (this._Bounds); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ComputeHistogram (BitmapData data)
		{
			int stride = 0;
			int height = 0;
			int sections = 0;
			int sectionLength = 0;
			byte [] buffer = null;

			if (data == null)
				throw (new ArgumentNullException("data"));

			this.Initialize();

			this._Width = data.Width;
			this._Height = data.Height;
			this._Stride = data.Stride;
			this._Bounds = new Rectangle(0, 0, data.Width, data.Height);

			sections = 4;
			height = data.Height;
			stride = System.Math.Abs(data.Stride);
			sectionLength = (data.Stride * data.Height) / sections;

			buffer = new byte [data.Stride * data.Height];
			Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < stride; x += 4)
				{
					int location = y * stride;

					this._B [buffer [location + x + 0]]++;
					this._G [buffer [location + x + 1]]++;
					this._R [buffer [location + x + 2]]++;
					this._A [buffer [location + x + 3]]++;
				}
			}

			//System.Threading.Tasks.Parallel.For
			//(
			//	0,
			//	sections,
			//	new System.Threading.Tasks.ParallelOptions() { MaxDegreeOfParallelism = System.Environment.ProcessorCount },
			//	y =>
			//	{
			//		int location = y * sectionLength;

			//		for (int x = 0; x < sectionLength; x += 4)
			//		{
			//			System.Threading.Interlocked.Increment(ref this.B [buffer [location + x + 0]]);
			//			System.Threading.Interlocked.Increment(ref this.G [buffer [location + x + 1]]);
			//			System.Threading.Interlocked.Increment(ref this.R [buffer [location + x + 2]]);
			//			System.Threading.Interlocked.Increment(ref this.A [buffer [location + x + 3]]);
			//		}
			//	}
			//);

			this._MaxA = this._A.Max();
			this._MaxR = this._R.Max();
			this._MaxG = this._G.Max();
			this._MaxB = this._B.Max();

			this._MaxT = new int [] { this._MaxA, this._MaxR, this._MaxG, this._MaxB }.Max();
		}

		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ComputeHistogram (BitmapData data, Rectangle rectangle)
		{
			int stride = 0;

			if (data == null)
				throw (new ArgumentNullException("data"));
			if ((rectangle.Left < 0) || (rectangle.Top < 0) || (rectangle.Right > data.Width) || (rectangle.Bottom > data.Height))
				throw (new ArgumentException("The argument [rectangle] does not fall within image bounds.", "rectangle"));

			this.Initialize();

			this._Width = data.Width;
			this._Height = data.Height;
			this._Stride = data.Stride;
			this._Bounds = rectangle;

			stride = System.Math.Abs(data.Stride);

			unsafe
			{
				byte* pointer = (byte*) 0;

				for (int y = rectangle.Top; y < rectangle.Bottom; y++)
				{
					pointer = ((byte*) data.Scan0) + (stride * y);

					for (int x = rectangle.Left; x < rectangle.Right; x += 4)
					{
						this._B [pointer [x + 0]]++;
						this._G [pointer [x + 1]]++;
						this._R [pointer [x + 2]]++;
						this._A [pointer [x + 3]]++;
					}
				}
			}

			this._MaxA = this._A.Max();
			this._MaxR = this._R.Max();
			this._MaxG = this._G.Max();
			this._MaxB = this._B.Max();

			this._MaxT = new int [] { this._MaxA, this._MaxR, this._MaxG, this._MaxB }.Max();
		}

		#endregion Methods.

		#region Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.ICloneable<T>.

		//====================================================================================================
		// Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.ICloneable<T>.
		//====================================================================================================

		Histogram ICloneable<Histogram>.Clone () { return (this.Clone()); }
		Histogram ICopyable<Histogram>.CopyFrom (Histogram source) { return (this.CopyFrom(source)); }
		Histogram ICopyable<Histogram>.CopyTo (Histogram destination) { return (this.CopyTo(destination)); }

		public Histogram Clone ()
		{
			return (new Histogram().CopyFrom(this));
		}

		public Histogram CopyFrom (Histogram source)
		{
			this.Initialize();

			source.A.CopyTo(this._A, 0);
			source.R.CopyTo(this._R, 0);
			source.G.CopyTo(this._G, 0);
			source.B.CopyTo(this._B, 0);

			this._MaxA = source.MaxA;
			this._MaxR = source.MaxR;
			this._MaxG = source.MaxG;
			this._MaxB = source.MaxB;

			this._MaxT = source.MaxT;

			return (this);
		}

		public Histogram CopyTo (Histogram destination)
		{
			return (destination.CopyFrom(this));
		}

		#endregion Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.ICloneable<T>.

		#region Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistBinary<T>.

		//====================================================================================================
		// Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistBinary<T>.
		//====================================================================================================

		public const int LengthFixed
				= 4				// Length.
				+ 4				// this.Width.
				+ 4				// this.Height.
				+ 4				// this.Stride.
				+ 4				// this.Bounds.X.
				+ 4				// this.Bounds.Y.
				+ 4				// this.Bounds.Width.
				+ 4				// this.Bounds.Height.
				+ 4				// this.A.Length.
				+ (256 * 4)		// this.A []: int [256] to byte [256 * 4].
				+ 4				// this.R.Length.
				+ (256 * 4)		// this.R []: int [256] to byte [256 * 4].
				+ 4				// this.G.Length.
				+ (256 * 4)		// this.G []: int [256] to byte [256 * 4].
				+ 4				// this.B.Length.
				+ (256 * 4)		// this.B []: int [256] to byte [256 * 4].
				; // = 4140.

		int IPersistBinary<Histogram>.Length
		{
			get { return (Histogram.LengthFixed); }
		}

		BinaryWriter IPersistBinary<Histogram>.ToBinary (BinaryWriter writer)
		{
			return (this.ToBinary(writer));
		}

		Histogram IPersistBinary<Histogram>.FromBinary (BinaryReader reader)
		{
			return (this.FromBinary(reader));
		}

		public int Length
		{
			get
			{
				return (Histogram.LengthFixed);
			}
		}

		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public BinaryWriter ToBinary (BinaryWriter writer)
		{
			writer.Write(LengthFixed);

			writer.Write(this._Width);
			writer.Write(this._Height);
			writer.Write(this._Stride);
			writer.Write(this._Bounds.X);
			writer.Write(this._Bounds.Y);
			writer.Write(this._Bounds.Width);
			writer.Write(this._Bounds.Height);

			writer.Write(this._A.Length);
			for (int i = 0; i < this._A.Length; i++)
				writer.Write(this._A [i]);

			writer.Write(this._R.Length);
			for (int i = 0; i < this._R.Length; i++)
				writer.Write(this._R [i]);

			writer.Write(this._G.Length);
			for (int i = 0; i < this._G.Length; i++)
				writer.Write(this._G [i]);

			writer.Write(this._B.Length);
			for (int i = 0; i < this._B.Length; i++)
				writer.Write(this._B [i]);

			return (writer);
		}

		//[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Histogram FromBinary (BinaryReader reader)
		{
			if (reader == null)
				throw (new ArgumentNullException("reader"));

			this.Initialize();

			if (reader.BaseStream.Length > 0)
			{
				//try
				{
					if (reader.BaseStream.Length <= LengthFixed)
						throw (new ArgumentException("The argument [reader.BaseStream] appears to be of invalid length.", "reader"));

					int len = reader.ReadInt32();

					if (len > 0)
					{
						if (len != LengthFixed)
							throw (new ArgumentException("The argument [reader.BaseStream] appears to contain invalid data.", "reader"));

						this._Width = reader.ReadInt32();
						this._Height = reader.ReadInt32();
						this._Stride = reader.ReadInt32();

						this._Bounds = new Rectangle
						(
							reader.ReadInt32(),
							reader.ReadInt32(),
							reader.ReadInt32(),
							reader.ReadInt32()
						);

						this._A = new int [reader.ReadInt32()];
						for (int i = 0; i < this._A.Length; i++)
							this._A [i] = reader.ReadInt32();
						this._R = new int [reader.ReadInt32()];
						for (int i = 0; i < this._R.Length; i++)
							this._R [i] = reader.ReadInt32();
						this._G = new int [reader.ReadInt32()];
						for (int i = 0; i < this._G.Length; i++)
							this._G [i] = reader.ReadInt32();
						this._B = new int [reader.ReadInt32()];
						for (int i = 0; i < this._B.Length; i++)
							this._B [i] = reader.ReadInt32();

						this._MaxA = this._A.Max();
						this._MaxR = this._R.Max();
						this._MaxG = this._G.Max();
						this._MaxB = this._B.Max();

						this._MaxT = new int [] { this._MaxA, this._MaxR, this._MaxG, this._MaxB }.Max();
					}
				}
				//catch
				{
					//this.Initialize();

					//throw;
				}
			}

			return (this);
		}

		#endregion Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistBinary<T>.
	}
}