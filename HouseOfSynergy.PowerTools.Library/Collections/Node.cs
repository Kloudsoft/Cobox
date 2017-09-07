using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class Node<T>
		where T: new()
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public int Index { get; internal set; }
		public Guid Guid { get; internal set; }

		public Nodes<T> Nodes { get; set; }
		public Tree<T> Tree { get; internal set; }
		public Node<T> ParentNode { get; internal set; }
		public Nodes<T> ParentNodes { get; internal set; }

		public Tag<T> TagGeneric { get; private set; }
		public Tag<object> TagObject { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public Node ()
		{
			this.Tree = (this.ParentNodes == null ? null : this.ParentNodes.Tree);

			this.Nodes = new Nodes<T>(this.Tree, this);

			this.TagGeneric = new Tag<T>();
			this.TagObject = new Tag<object>();
		}

		internal Node (Tree<T> tree, Nodes<T> parentNodes, Node<T> parentNode)
		{
			this.Tree = tree;
			this.ParentNodes = parentNodes;
			this.ParentNode = parentNode;

			this.Nodes = new Nodes<T>(this.Tree, this);

			this.TagGeneric = new Tag<T>();
			this.TagObject = new Tag<object>();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public Node<T> this [int index] { get { return (this.Nodes [index]); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Node<T> Add (T item) { return (this.Nodes.Add(item)); }
		public List<Node<T>> AddRange (List<T> items) { return (this.Nodes.AddRange(items)); }
		public Node<T> Insert (int index, T item) { return (this.Nodes.Insert(index, item)); }
		public List<Node<T>> InsertRange (int index, List<T> items) { return (this.Nodes.InsertRange(index, items)); }
		public int GetDepth () { return (this.Nodes.GetDepth()); }

		#endregion Methods.
	}
}