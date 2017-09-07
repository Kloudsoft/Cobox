using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Media;

namespace HouseOfSynergy.PowerTools.Library.Captures.Samples
{
	public sealed class SampleAudio:
		SampleBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public byte [] Data { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public SampleAudio (DateTimeOffset dateTimeOffset, byte [] data)
			: base(MediaType.Audio, dateTimeOffset)
		{
			if (data == null) { throw (new ArgumentNullException("data")); }

			this.Data = data;
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		#endregion Methods.

		#region Interface Implementation: System.IDisposable.

		//====================================================================================================
		// Interface Implementation: System.IDisposable.
		//====================================================================================================

		private bool Disposed { get; set; }

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					this.Data = null;
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.
	}
}