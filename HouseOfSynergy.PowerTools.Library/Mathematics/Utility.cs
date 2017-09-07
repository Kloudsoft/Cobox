using System;
using System.Drawing;

namespace HouseOfSynergy.PowerTools.Library.Mathematics
{
	public static class Utility
	{
		public static int GetGreatestCommonDivisor (int number1, int number2)
		{
			var temp = 0;

			// Ensure B > A.
			if (number1 > number2)
			{
				temp = number2;
				number2 = number1;
				number1 = temp;
			}

			// Find.
			while (number2 != 0)
			{
				temp = number1 % number2;
				number1 = number2;
				number2 = temp;
			}

			return (number1);
		}

		public static int GetLeastCommonMultiple (int number1, int number2) { return ((number1 * number2) / GetGreatestCommonDivisor(number1, number2)); }

		public static double EuclideanDistance (Point p1, Point p2) { return (Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))); }

		public static double EuclideanDistance (PointF p1, PointF p2) { return (Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2))); }

		/// <summary>
		/// Calculates the similarity between 2 points using Euclidean distance.
		/// </summary>
		/// <returns>Returns a value between 0 and 1 where 1 means they are identical.</returns>
		public static double EuclideanSimilarity (Point p1, Point p2) { return (1D / (1D + Utility.EuclideanDistance(p1, p2))); }

		/// <summary>
		/// Calculates the similarity between 2 points using Euclidean distance.
		/// </summary>
		/// <returns>Returns a value between 0 and 1 where 1 means they are identical.</returns>
		public static double EuclideanSimilarity (PointF p1, PointF p2) { return (1D / (1D + Utility.EuclideanDistance(p1, p2))); }

		public static float CalculateGradiant (Point p1, Point p2) { return (((float) (p2.Y - p1.Y)) / ((float) (p2.X - p1.X))); }

		public static float CalculateGradiant (PointF p1, PointF p2) { return ((p2.Y - p1.Y) / (p2.X - p1.X)); }
	}
}