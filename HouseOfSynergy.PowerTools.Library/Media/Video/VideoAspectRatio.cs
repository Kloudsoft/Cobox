using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Video
{
	/// <summary>
	/// Represents the number of frames per second.
	/// </summary>
	public enum VideoAspectRatio:
		int
	{
		/// <summary>
		/// Ratio: Unknown.
		/// </summary>
		Unknown,
		/// <summary>
		/// Ratio: 1:1.
		/// </summary>
		One_One,
		/// <summary>
		/// Ratio: 4:3.
		/// </summary>
		Four_Three,
		/// <summary>
		/// Ratio: 16:9.
		/// </summary>
		Sixteen_Nine,
		/// <summary>
		/// Ratio: 21:9.
		/// </summary>
		TwentyOne_Nine,
		/// <summary>
		/// Ratio: 64:27.
		/// </summary>
		SixtyFour_TwentySeven,
		/// <summary>
		/// Ratio: 1.19:1, 119:100.
		/// </summary>
		OneNineteen_Hundred,
		/// <summary>
		/// 35 mm full-stage film image.
		/// Ratio: 1.375:1, 1,375:1.
		/// </summary>
		ThirteenSeventyFive_OneThousand,
		/// <summary>
		/// IMAX format.
		/// Ratio: 1.43:1.
		/// </summary>
		OneFortyThree_One,
		/// <summary>
		/// Ratio: 3:2.
		/// </summary>
		Three_Two,
		/// <summary>
		/// Ratio: 14:9.
		/// </summary>
		Fourteen_Nine,
		/// <summary>
		/// Ratio: 16:10.
		/// </summary>
		Sixteen_Ten,
		/// <summary>
		/// Ratio: 5:3.
		/// </summary>
		Five_Three,
		/// <summary>
		/// Ratio: 2:1.
		/// </summary>
		Two_One,
	}
}