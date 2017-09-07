namespace HouseOfSynergy.PowerTools.Library.Network
{
	public abstract class NetworkDevice
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public NetworkDevice ()
		{
			this.Id = 0;
			this.Name = "";
			this.Description = "";
		}

		public NetworkDevice (int id, string name, string description = "")
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
		}

		public override string ToString ()
		{
			return (this.Name);
		}
	}
}