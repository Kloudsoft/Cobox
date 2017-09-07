using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Media.Audio;

namespace HouseOfSynergy.PowerTools.Library.Media
{
	public sealed class MediaInfoAudio:
		MediaInfo,
		IEquatable<MediaInfo>
	{
		/// <summary>
		/// Specific to NAudio. Returns the number of extra bytes used by this waveformat. Often 0, except for compressed formats which store extra data after the WAVEFORMATEX header.
		/// </summary>
		public int ExtraSize { get; private set; }
		/// <summary>
		/// Block alignment, in bytes. The block alignment is the minimum atomic unit of data. For PCM data, the block alignment is the number of bytes used by a single sample, including data for both channels if the data is stereo. For example, the block alignment for 16-bit stereo PCM is 4 bytes (2 channels — 2 bytes per sample).
		/// </summary>
		public int BlockAlign { get; private set; }
		/// <summary>
		/// Required average data transfer rate, in bytes per second. For example, 16-bit stereo at 44.1 kHz has an average data rate of 176,400 bytes per second (2 channels — 2 bytes per sample per channel — 44,100 samples per second).
		/// </summary>
		public int AverageBytesPerSecond { get; private set; }
		/// <summary>
		/// Sample rate, in samples per second.
		/// </summary>
		public AudioSampleRate AudioSampleRate { get; private set; }
		/// <summary>
		/// Sample rate, in samples per second.
		/// </summary>
		public int SamplesPerSecond { get; private set; }
		/// <summary>
		/// The number of bits each sample uses.
		/// </summary>
		public AudioBitsPerSample AudioBitDepth { get; private set; }
		/// <summary>
		/// The number of bits each sample uses.
		/// </summary>
		public int BytesPerSample { get; private set; }
		/// <summary>
		/// The number of bytes each sample uses.
		/// </summary>
		public int BitsPerSample { get; private set; }
		/// <summary>
		/// The number of bytes each sample uses.
		/// </summary>
		public AudioBytesPerSample AudioByteDepth { get; private set; }
		/// <summary>
		/// Number of channels in the waveform-audio data. Mono data uses one channel and stereo data uses two channels.
		/// </summary>
		public AudioChannelCount AudioChannelCount { get; private set; }
		/// <summary>
		/// Number of channels in the waveform-audio data. Mono data uses one channel and stereo data uses two channels.
		/// </summary>
		public int NumberOfChannels { get; private set; }

		/// <summary>
		/// Initializes a new instance of the MediaInfoAudio class from the specified arguments.
		/// </summary>
		/// <param name="sampleRate">The number of samples per second to capture e.g. 16,000, 44,100.</param>
		/// <param name="bitDepth">The bit depth of the capture e.g. 8, 16, 24, 32.</param>
		/// <param name="channelCount">The number of channels to capture e.g. 1 for mono, 2 for stereo.</param>
		/// <param name="extraSize">Specific to NAudio. Returns the number of extra bytes used by this waveformat. Often 0, except for compressed formats which store extra data after the WAVEFORMATEX header.</param>
		public MediaInfoAudio (AudioSampleRate sampleRate, AudioBitsPerSample bitDepth, AudioChannelCount channelCount, int extraSize = 0)
			: base(type : MediaType.Audio, sampleRate : ((int) sampleRate))
		{
			if (!Enum.IsDefined(typeof(AudioSampleRate), sampleRate)) { throw (new ArgumentException("The argument [sampleRate] must be a valid member of the [AudioSamplesRate] enumeration.", "sampleRate")); }
			if (!Enum.IsDefined(typeof(AudioBitsPerSample), bitDepth)) { throw (new ArgumentException("The argument [bitDepth] must be a valid member of the [AudioBitsPerSample] enumeration.", "bitDepth")); }
			if (!Enum.IsDefined(typeof(AudioChannelCount), channelCount)) { throw (new ArgumentException("The argument [channelCount] must be a valid member of the [AudioChannelCount] enumeration.", "channelCount")); }
			if (channelCount == AudioChannelCount.AudioChannelCount_Unknown) { throw (new ArgumentException("The argument [channelCount] must not be [AudioChannelCount.AudioChannelCount_Unknown].", "channelCount")); }

			this.ExtraSize = extraSize;
			this.AudioBitDepth = bitDepth;
			this.AudioSampleRate = sampleRate;
			this.AudioChannelCount = channelCount;

			this.BitsPerSample = (int) this.AudioBitDepth;
			this.BytesPerSample = this.BitsPerSample / 8;
			this.SamplesPerSecond = (int) sampleRate;
			this.NumberOfChannels = (int) this.AudioChannelCount;

			this.BlockAlign = this.BytesPerSample * this.NumberOfChannels;
			this.AudioByteDepth = (AudioBytesPerSample) (this.BitsPerSample / 8);
			this.AverageBytesPerSecond = this.BytesPerSample * this.SamplesPerSecond * this.NumberOfChannels;
		}

		public bool Equals (MediaInfoAudio other)
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
					&& (this.AudioBitDepth == this.AudioBitDepth)
					&& (this.AudioChannelCount == this.AudioChannelCount)
					&& (this.AudioSampleRate == this.AudioSampleRate)
					;
			}

			return (result);
		}

		public override bool Equals (object obj)
		{
			return ((obj is MediaInfoAudio) && (this.Equals(obj as MediaInfoAudio)));
		}

		public override int GetHashCode ()
		{
			int hash = 0;

			unchecked // Overflow is fine, just wrap.
			{
				hash = 17;
				hash = hash * 23 + base.GetHashCode();
				hash = hash * 23 + this.AudioBitDepth.GetHashCode();
				hash = hash * 23 + this.AudioChannelCount.GetHashCode();
				hash = hash * 23 + this.AudioSampleRate.GetHashCode();
			}

			return (hash);
		}

		public override string ToString ()
		{
			return ("Media Info (Media Type: " + this.MediaType.ToString() + ")");
		}
	}
}