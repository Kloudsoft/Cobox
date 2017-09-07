namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class EntityAttribute:
		System.Attribute
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Label { get; private set; }

		public EntityAttribute (int Id, string Name, string Label)
		{
			this.Id = Id;
			this.Name = Name;
			this.Label = Label;
		}
	}
}