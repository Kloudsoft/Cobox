using System;
using System.Linq;
using System.Linq.Expressions;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public abstract class Point<T>:
		//HouseOfSynergy.PowerTools.Library.Interfaces.ICloneableStruct<Point<T>>,
		IPoint<T>
		where T: struct
	{
		public PointType Type { get; private set; }

		protected Point (PointType type) { this.Type = type; }

		public abstract T Distance { get; }

		public readonly static Func<T, T, T> OperatorAdd;
		public readonly static Func<T, T, T> OperatorSubtract;
		public readonly static Func<T, T, T> OperatorMultiply;
		public readonly static Func<T, T, T> OperatorDivide;

		static Point ()
		{
			Point<T>.OperatorAdd = Point<T>.MakeBinaryOperator(ExpressionType.Add);
			Point<T>.OperatorSubtract = Point<T>.MakeBinaryOperator(ExpressionType.Subtract);
			Point<T>.OperatorMultiply = Point<T>.MakeBinaryOperator(ExpressionType.Multiply);
			Point<T>.OperatorDivide = Point<T>.MakeBinaryOperator(ExpressionType.Divide);
		}

		private static Func<T, T, T> MakeBinaryOperator (ExpressionType type)
		{
			var x = Expression.Parameter(typeof(T), "x");
			var y = Expression.Parameter(typeof(T), "y");
			var body = Expression.MakeBinary(type, x, y);
			var expression = Expression.Lambda<Func<T, T, T>>(body, x, y);

			return (expression.Compile());
		}
	}
}