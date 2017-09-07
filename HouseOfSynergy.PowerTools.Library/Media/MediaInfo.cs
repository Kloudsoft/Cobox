using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media
{
	public abstract class MediaInfo:
		IEquatable<MediaInfo>
	//ISignal<int>
	{
		public TimeSpan Interval { get; private set; }
		public int SampleRate { get; private set; }
		public MediaType MediaType { get; private set; }

		protected MediaInfo (MediaType type, int sampleRate)
		{
			if (!Enum.IsDefined(typeof(MediaType), type)) { throw (new ArgumentException("The argument [type] must be a valid member of the [MediaType] enumeration.", "type")); }

			this.MediaType = type;
			this.SampleRate = sampleRate;
			this.Interval = TimeSpan.FromSeconds(1D / this.SampleRate);
		}

		public bool Equals (MediaInfo other)
		{
			bool result = false;

			if (object.ReferenceEquals(this, other))
			{
				result = true;
			}
			else if (!object.ReferenceEquals(other, null))
			{
				result
					= (this.MediaType == other.MediaType)
					;
			}

			return (result);
		}

		public override bool Equals (object obj)
		{
			return ((obj is MediaInfo) && (this.Equals(obj as MediaInfo)));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked // Overflow is fine, just wrap.
			{
				hash = 17;
				hash = hash * 23 + this.MediaType.GetHashCode();
			}

			return (hash);
		}

		public override string ToString ()
		{
			return ("Media Info (Media Type: " + this.MediaType.ToString() + ")");
		}
	}
}