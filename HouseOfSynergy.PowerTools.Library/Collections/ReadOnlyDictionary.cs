using System;
using System.Collections;
using System.Collections.Generic;

//using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyDictionary<TKey, TValue>:
		IDictionary<TKey, TValue>,
		IEnumerable<KeyValuePair<TKey, TValue>>,
		IReadOnlyCollection
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private Dictionary<TKey, TValue> Dictionary { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public ReadOnlyDictionary () : this(allowListEdit : false, allowItemEdit : false) { }
		public ReadOnlyDictionary (bool allowListEdit, bool allowItemEdit) { this.AllowListEdit = allowListEdit; this.AllowItemEdit = allowItemEdit; this.Dictionary = new Dictionary<TKey, TValue>(); }
		public ReadOnlyDictionary (IEqualityComparer<TKey> comparer, bool allowListEdit, bool allowItemEdit) { this.AllowListEdit = allowListEdit; this.AllowItemEdit = allowItemEdit; this.Dictionary = new Dictionary<TKey, TValue>(comparer); }
		public ReadOnlyDictionary (IDictionary<TKey, TValue> dictionary, bool allowListEdit = false, bool allowItemEdit = false) : this(allowListEdit, allowItemEdit) { foreach (var pair in dictionary) { this.Dictionary.Add(pair.Key, pair.Value); } }
		public ReadOnlyDictionary (IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, bool allowListEdit = false, bool allowItemEdit = false) : this(comparer, allowListEdit, allowItemEdit) { foreach (var pair in dictionary) { this.Dictionary.Add(pair.Key, pair.Value); } }

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public IEqualityComparer<TKey> Comparer { get { return (this.Dictionary.Comparer); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		private void ThrowItemReadOnlyException () { if (!this.AllowListEdit) { throw (new ReadOnlyListItemEditException("This collection does not allow editing items.")); } }
		private void ThrowListReadOnlyException () { if (!this.AllowItemEdit) { throw (new ReadOnlyListListEditException("This collection does not allow adding or removing items.")); } }

		//public void Clear () { this.ThrowListReadOnlyException(); this.Dictionary.Clear(); }
		public bool ContainsValue (TValue value) { return (this.Dictionary.ContainsValue(value)); }

		public Dictionary<TKey, TValue> ToDictionary ()
		{
			return (new Dictionary<TKey, TValue>(this.Dictionary as IDictionary<TKey, TValue>));
		}

		#endregion Methods.

		#region Interface Implementation: IReadOnlyCollection.

		//====================================================================================================
		// Interface Implementation: IReadOnlyCollection.
		//====================================================================================================

		public bool AllowListEdit { get; private set; }
		public bool AllowItemEdit { get; private set; }

		#endregion Interface Implementation: IReadOnlyCollection.

		#region Interface Implementation: IEnumerable<KeyValuePair<TKey, TValue>>IReadOnlyCollection.

		//====================================================================================================
		// Interface Implementation: IEnumerable<KeyValuePair<TKey, TValue>>.
		//====================================================================================================

		IEnumerator IEnumerable.GetEnumerator () { return (this.Dictionary.GetEnumerator()); }

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator () { return (this.Dictionary.GetEnumerator()); }

		#endregion Interface Implementation: IEnumerable<KeyValuePair<TKey, TValue>>IReadOnlyCollection.

		#region Interface Implementation: ICollection<KeyValuePair<TKey, TValue>>.

		//====================================================================================================
		// Interface Implementation: ICollection<KeyValuePair<TKey, TValue>>.
		//====================================================================================================

		public int Count { get { return (this.Dictionary.Count); } }
		public ICollection<TKey> Keys { get { return (this.Dictionary.Keys); } }
		public ICollection<TValue> Values { get { return (this.Dictionary.Values); } }
		public bool IsReadOnly { get { return (!(this.AllowListEdit || this.AllowItemEdit)); } }

		public void Clear () { this.ThrowListReadOnlyException(); this.Dictionary.Clear(); }
		public void Add (KeyValuePair<TKey, TValue> item) { this.ThrowListReadOnlyException(); this.Dictionary.Add(item.Key, item.Value); }
		public bool Contains (KeyValuePair<TKey, TValue> item) { return ((this.Dictionary as ICollection<KeyValuePair<TKey, TValue>>).Contains(item)); }
		public void CopyTo (KeyValuePair<TKey, TValue> [] array, int arrayIndex) { (this.Dictionary as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex); }
		public bool Remove (KeyValuePair<TKey, TValue> item) { this.ThrowListReadOnlyException(); return ((this.Dictionary as ICollection<KeyValuePair<TKey, TValue>>).Remove(item)); }

		#endregion Interface Implementation: ICollection<KeyValuePair<TKey, TValue>>.

		#region Interface Implementation: IDictionary<TKey, TValue>.

		//====================================================================================================
		// Interface Implementation: IDictionary<TKey, TValue>.
		//====================================================================================================

		public TValue this [TKey key] { get { return (this.Dictionary [key]); } set { this.ThrowItemReadOnlyException(); this.Dictionary [key] = value; } }

		public void Add (TKey key, TValue value) { this.ThrowListReadOnlyException(); this.Dictionary.Add(key, value); }
		public bool ContainsKey (TKey key) { return (this.Dictionary.ContainsKey(key)); }
		public bool Remove (TKey key) { this.ThrowListReadOnlyException(); return (this.Dictionary.Remove(key)); }
		public bool TryGetValue (TKey key, out TValue value) { return (this.Dictionary.TryGetValue(key, out value)); }

		#endregion Interface Implementation: IDictionary<TKey, TValue>.
	}
}