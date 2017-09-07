using System;

namespace HouseOfSynergy.PowerTools.Library.Threading
{
	public sealed class DegreeOfParallelism
	{
		private int _Value { get; set; }

		public DegreeOfParallelism () : this(1) { }
		public DegreeOfParallelism (int defaultDegreeOfParallelism) { this.Value = this.DefaultDegreeOfParallelism; }

		public int DefaultDegreeOfParallelism { get; private set; }

		public bool IsLimited { get { return (this.Value != -1); } }
		public bool IsUnlimited { get { return (this.Value == -1); } }
		public bool IsMinimum { get { return (this.Value == DegreeOfParallelism.MinimumDegreeOfParallelism); } }
		public bool IsMaximum { get { return ((this.IsUnlimited) || (this.Value == DegreeOfParallelism.MaximumDegreeOfParallelism)); } }
		public bool ExceedsProcessorCount { get { return (this.Value > DegreeOfParallelism.MaximumDegreeOfParallelism); } }

		public int Value
		{
			get
			{
				return (this._Value);
			}
			private set
			{
				if (value < -1) { this._Value = 1; }
				else if (value == -1) { this._Value = value; }
				else if (value == 0) { this._Value = 1; }
				else if (value <= DegreeOfParallelism.MaximumDegreeOfParallelism) { this._Value = value; }
				else { this._Value = DegreeOfParallelism.MaximumDegreeOfParallelism; }
			}
		}

		public static int MinimumDegreeOfParallelism { get { return (1); } }
		public static int MaximumDegreeOfParallelism { get { return (Environment.ProcessorCount); } }
	}
}