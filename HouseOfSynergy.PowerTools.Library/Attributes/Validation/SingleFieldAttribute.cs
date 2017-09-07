using System;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class SingleFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.DoubleFieldAttribute
	{
		public new int PrecisionMinimum { get { return (0); } }

		public new int PrecisionMaximum { get { return (7); } }

		public new float Minimum { get; private set; }

		public new float Maximum { get; private set; }

		public SingleFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly,
			float Minimum,
			float Maximum,
			int Precision
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly, Minimum, Maximum, Precision)
		{
			if ((Precision < PrecisionMinimum) || (Precision > PrecisionMaximum))
			{
				throw (new ArgumentException("Precision has to be a value between " + PrecisionMinimum.ToString() + " and " + PrecisionMaximum.ToString() + " inclusive.", "Precision"));
			}
		}
	}
}