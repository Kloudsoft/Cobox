using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class Tree<T>
		where T: new()
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public Nodes<T> Nodes { get; private set; }
		private Dictionary<Guid, Node<T>> Dictionary { get; set; }

		public Tag<T> TagGeneric { get; private set; }
		public Tag<object> TagObject { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public Tree ()
		{
			this.Nodes = new Nodes<T>(this, null);
			this.Dictionary = new Dictionary<Guid, Node<T>>();

			this.TagGeneric = new Tag<T>();
			this.TagObject = new Tag<object>();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public bool HasNodes { get { return (this.Nodes.Count > 0); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Node<T> Add (T item) { return (this.Nodes.Add(item)); }

		public int GetDepth () { return (this.Nodes.GetDepth()); }

		public Guid GetFreeGuid ()
		{
			Guid guid = Guid.Empty;

			do { guid = Guid.NewGuid(); } while (this.Dictionary.ContainsKey(guid));

			return (guid);
		}

		#endregion Methods.
	}
}