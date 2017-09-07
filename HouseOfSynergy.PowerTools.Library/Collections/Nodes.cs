using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class Nodes<T>:
		IEnumerable<Node<T>>
		where T: new()
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private List<Node<T>> _Nodes = null;

		public Tree<T> Tree { get; private set; }
		public Node<T> ParentNode { get; private set; }

		public Tag<T> TagGeneric { get; private set; }
		public Tag<object> TagObject { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public Nodes ()
		{
			this._Nodes = new List<Node<T>>();

			this.TagGeneric = new Tag<T>();
			this.TagObject = new Tag<object>();
		}

		internal Nodes (Tree<T> tree, Node<T> parentNode)
			: this()
		{
			this.Tree = tree;
			this.ParentNode = parentNode;
		}

		public Nodes (Node<T> parentNode)
			: this(null, parentNode)
		{
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public int Count { get { return (this._Nodes.Count); } }
		public bool HasChildren { get { return (this.Count > 0); } }
		public Node<T> this [int index] { get { return (this._Nodes [index]); } }
		public Node<T> this [Guid guid] { get { return (this._Nodes.Single(node => (node.Guid == guid))); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public Node<T> Add () { return (this.Insert(this._Nodes.Count, new T())); }
		public Node<T> Add (T item) { return (this.Insert(this._Nodes.Count, item)); }
		public List<Node<T>> AddRange (List<T> items) { return (this.InsertRange(this._Nodes.Count, items)); }

		public Node<T> Insert (int index, T item)
		{
			Node<T> node = null;

			node = new Node<T>(this.Tree, this, this.ParentNode);
			node.Tree = this.Tree;
			node.ParentNodes = this;
			node.ParentNode = this.ParentNode;
			node.Guid = this.Tree.GetFreeGuid();

			this._Nodes.Insert(index, node);

			this.ReIndex();

			return (node);
		}

		public List<Node<T>> InsertRange (int index, List<T> items)
		{
			List<Node<T>> nodes = null;

			nodes = new List<Node<T>>();

			for (int i = items.Count - 1; i >= 0; i--)
			{
				nodes.Add(this.Insert(index, items [i]));
			}

			return (nodes);
		}

		public void RemoveAt (int index) { this._Nodes.RemoveAt(index); }

		public int GetDepth () { return (this._Nodes.Count == 0 ? 0 : (1 + this._Nodes.Max(n => n.GetDepth()))); }

		internal void ReIndex () { for (int i = 0; i < this._Nodes.Count; i++) { this._Nodes [i].Index = i; } }

		#endregion Methods.

		#region Interface Implementation: IEnumerable<Node<T>>.

		//====================================================================================================
		// Interface Implementation: IEnumerable<Node<T>>.
		//====================================================================================================

		IEnumerator IEnumerable.GetEnumerator () { return (this._Nodes.GetEnumerator()); }
		IEnumerator<Node<T>> IEnumerable<Node<T>>.GetEnumerator () { return (this._Nodes.GetEnumerator()); }

		#endregion Interface Implementation: IEnumerable<Node<T>>.
	}
}