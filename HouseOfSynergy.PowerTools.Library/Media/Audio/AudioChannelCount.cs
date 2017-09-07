using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Audio
{
	public enum AudioChannelCount:
		int
	{
		AudioChannelCount_Unknown = 00,
		AudioChannelCount_Mono = 01,
		AudioChannelCount_Stereo = 02,
		AudioChannelCount_01 = 01,
		AudioChannelCount_02 = 02,
		AudioChannelCount_03 = 03,
		AudioChannelCount_04 = 04,
		AudioChannelCount_05 = 05,
		AudioChannelCount_06 = 06,
		AudioChannelCount_07 = 07,
		AudioChannelCount_08 = 08,
		AudioChannelCount_09 = 09,
		AudioChannelCount_10 = 10,
	}
}