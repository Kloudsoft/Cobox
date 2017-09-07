using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Media.Video
{
	public static class VideoUtilities
	{
		private static readonly Dictionary<VideoSize, Size> _Sizes = null;

		static VideoUtilities ()
		{
			VideoUtilities._Sizes = new Dictionary<VideoSize, Size>();

			foreach (var size in EnumUtilities.GetValues<VideoSize>())
			{
				if (size != VideoSize.Unknown)
				{
					VideoUtilities._Sizes.Add(size, new Size(int.Parse(size.ToString().Substring(10, 4)), int.Parse(size.ToString().Substring(15, 4))));
				}
			}
		}

		public static Dictionary<VideoSize, Size> Sizes { get { return (new Dictionary<VideoSize, Size>(VideoUtilities._Sizes)); } }
	}
}