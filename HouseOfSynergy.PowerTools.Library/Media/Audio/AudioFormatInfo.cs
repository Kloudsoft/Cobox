using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Audio
{
	/// <summary>
	/// Represents information about an audio format.
	/// </summary>
	public class AudioFormatInfo
	{
		/// <summary>
		/// Gets and sets the block alignment in bytes.
		/// <remarks>
		/// Software for playback and recording of audio handles audio data in blocks. The sizes of these blocks are multiples of the value of the BlockAlign property. Block alignment value is the number of bytes in an atomic unit (that is, a block) of audio for a particular format. For Pulse Code Modulation (PCM) formats, the formula for calculating block alignment is as follows:
		/// Block Alignment = Bytes per Sample x Number of Channels.
		/// For example, the block alignment value for 16-bit PCM format mono audio is 2 (2 bytes per sample x 1 channel). For 16-bit PCM format stereo audio, the block alignment value is 4.
		/// Data written and read from a device must always start at the beginning of a block. For example, it is illegal to start playback of PCM data in the middle of a sample (meaning on a boundary that is not block-aligned).
		/// </remarks>
		/// </summary>
		public int BlockAlign { get; private set; }

		/// <summary>
		/// Gets the channel count of the audio.
		/// </summary>
		public AudioChannelCount AudioChannels { get; private set; }

		/// <summary>
		/// Gets the channel count of the audio.
		/// </summary>
		public int ChannelCount { get; private set; }

		/// <summary>
		/// Gets the bits per sample of the audio.
		/// </summary>
		public int BitsPerSample { get; private set; }

		/// <summary>
		/// Gets the bits per sample of the audio.
		/// </summary>
		public AudioBitsPerSample AudioBitsPerSample { get; private set; }

		/// <summary>
		/// Gets the bytes per sample of the audio.
		/// </summary>
		public int BytesPerSample { get; private set; }

		/// <summary>
		/// Gets the bytes per sample of the audio.
		/// </summary>
		public AudioBytesPerSample AudioBytesPerSample { get; private set; }

		/// <summary>
		/// The format-specific data of the audio format
		/// </summary>
		private byte [] FormatSpecificData { get; set; }

		/// <summary>
		///Gets the samples per second of the audio format.
		/// </summary>
		public int SamplesPerSecond { get; private set; }

		/// <summary>
		/// Gets the average bytes per second of the audio.
		/// </summary>
		public int AverageBytesPerSecond { get; private set; }

		/// <summary>
		/// Gets the encoding format of the audio.
		/// </summary>
		public AudioEncodingFormat EncodingFormat { get; private set; }

		/// <summary>
		/// Initializes a new instance of the SpeechAudioFormatInfo class and initializes private fields.
		/// </summary>
		private AudioFormatInfo ()
		{
			this.FormatSpecificData = new byte [] { };
		}

		/// <summary>
		/// Initializes a new instance of the SpeechAudioFormatInfo class and specifies the samples per second, bits per sample, and the number of channels.
		/// </summary>
		/// <param name="samplesPerSecond">The value for the samples per second.</param>
		/// <param name="bitsPerSample">The value for the bits per sample.</param>
		/// <param name="channels">A member of the AudioChannel enumeration (indicating Mono or Stereo).</param>
		public AudioFormatInfo (int samplesPerSecond, AudioBitsPerSample bitsPerSample, AudioChannelCount channels)
			: this()
		{
			this.EncodingFormat = AudioEncodingFormat.Pcm;

			this.AudioChannels = channels;
			this.ChannelCount = (int) channels;
			this.SamplesPerSecond = samplesPerSecond;
			this.BitsPerSample = (int) bitsPerSample;

			this.AudioBitsPerSample = bitsPerSample;
			this.BytesPerSample = this.BitsPerSample / 8;
			this.BlockAlign = this.BytesPerSample * this.ChannelCount;
			this.AudioBytesPerSample = (AudioBytesPerSample) this.BytesPerSample;
			this.AverageBytesPerSecond = this.SamplesPerSecond * (this.BitsPerSample / 2);
		}

		/// <summary>
		/// Initializes a new instance of the SpeechAudioFormatInfo class and specifies the encoding format, samples per second, bits per sample, number of channels, average bytes per second, block alignment value, and an array containing format-specific data.
		/// </summary>
		/// <param name="encodingFormat"></param>
		/// <param name="samplesPerSecond"></param>
		/// <param name="bitsPerSample"></param>
		/// <param name="channelCount"></param>
		/// <param name="averageBytesPerSecond"></param>
		/// <param name="blockAlign"></param>
		/// <param name="formatSpecificData"></param>
		private AudioFormatInfo (AudioEncodingFormat encodingFormat, int samplesPerSecond, int bitsPerSample, int channelCount, int averageBytesPerSecond, int blockAlign, byte [] formatSpecificData)
			: this()
		{
		}

		/// <summary>
		/// Gets the format-specific data of the audio format.
		/// </summary>
		/// <returns>Returns the format-specific data of the audio format.</returns>
		public byte [] GetFormatSpecificData ()
		{
			var data = new byte [this.FormatSpecificData.Length];

			Array.Copy(this.FormatSpecificData, data, this.FormatSpecificData.Length);

			return (data);
		}

		public static readonly AudioFormatInfo AudioFormatInfoAudioCD = null;
		public static readonly AudioFormatInfo AudioFormatInfoSuperAudioCD = null;
		public static readonly AudioFormatInfo AudioFormatInfoAudioDVD = null;

		static AudioFormatInfo ()
		{
			AudioFormatInfo.AudioFormatInfoAudioCD = new AudioFormatInfo(44100, AudioBitsPerSample.AudioBitsPerSample_16, AudioChannelCount.AudioChannelCount_Stereo);
			AudioFormatInfo.AudioFormatInfoSuperAudioCD = new AudioFormatInfo(2822400, AudioBitsPerSample.AudioBitsPerSample_16, AudioChannelCount.AudioChannelCount_Stereo);
			AudioFormatInfo.AudioFormatInfoAudioDVD = new AudioFormatInfo(192000, AudioBitsPerSample.AudioBitsPerSample_24, AudioChannelCount.AudioChannelCount_Stereo);
		}
	}
}