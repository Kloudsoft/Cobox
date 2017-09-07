namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class BooleanFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.FieldAttribute
	{
		public BooleanFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly
		)
			: base(Type, Ordinal, Name, Description, Label, Tooltip, Required, ReadOnly)
		{
		}

		protected override bool OnValidate ()
		{
			return (true);
		}
	}
}