using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Media;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Captures.Samples
{
	public abstract class SampleBase:
		Disposable
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public MediaType CaptureType { get; private set; }
		public DateTimeOffset DateTimeOffset { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		protected SampleBase (MediaType type, DateTimeOffset dateTimeOffset)
		{
			this.CaptureType = type;
			this.DateTimeOffset = dateTimeOffset;
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
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}

		#endregion Interface Implementation: System.IDisposable.
	}
}