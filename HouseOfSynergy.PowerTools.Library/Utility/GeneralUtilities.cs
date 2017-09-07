using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class GeneralUtilities
	{
		public static void Swap<T> (ref T left, ref T right)
		{
			T temp = left;
			left = right;
			right = temp;
		}
	}
}