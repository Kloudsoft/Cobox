using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public sealed class PointND<T>:
		Point<T>,
		IPoint<T>,
		IPointND<T>
		where T: struct
	{
		private List<T> _Vertices { get; set; }
		public ReadOnlyCollection<T> Vertices { get; private set; }

		public PointND (List<T> vertices) : this() { this._Vertices.AddRange(vertices); }
		public PointND (IEnumerable<T> vertices) : this() { this._Vertices.AddRange(vertices); }
		public PointND () : base(PointType.NDimentional) { this._Vertices = new List<T>(); this.Vertices = new ReadOnlyCollection<T>(this._Vertices); }
		public PointND (int verticeCount) : this() { this._Vertices.AddRange(Enumerable.Range(0, verticeCount).ToList().ConvertAll<T>((item) => default(T))); }

		public int VerticeCount { get { return (this._Vertices.Count); } }

		public sealed override T Distance
		{
			get
			{
				T result = default(T);
				List<T> values = null;

				if (this.VerticeCount > 0)
				{
					values = new List<T>(this._Vertices.ConvertAll<T>(v => Point<T>.OperatorMultiply(v, v)));

					while (values.Count > 1)
					{
						values [0] = Point<T>.OperatorAdd(values [0], values [1]);

						values.RemoveAt(1);
					}

					result = values [0];
				}

				return (result);
			}
		}

		public PointND<T> Clone () { return (new PointND<T>(this._Vertices)); }
	}
}