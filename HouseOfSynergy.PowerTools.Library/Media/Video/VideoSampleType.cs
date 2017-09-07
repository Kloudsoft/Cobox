using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Video
{
	/// <summary>
	/// Represents the number of frames per second.
	/// </summary>
	public enum VideoSampleType:
		int
	{
		Interlaced,
		Progressive,
		Differential,
	}
}