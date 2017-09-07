using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public class ReadOnlyList<T>:
		IList<T>,
		IEnumerable<T>,
		ICollection<T>,
		IReadOnlyCollection,
		IQueue<T>,
		IStack<T>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		private List<T> Collection { get; set; }
		public bool AllowListEdit { get; private set; }
		public bool AllowItemEdit { get; private set; }
		private ReadOnlyCollection<T> ReadOnlyCollection { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public ReadOnlyList (bool allowItemEdit = false) : this(true, allowItemEdit) { }
		public ReadOnlyList (bool allowListEdit, bool allowItemEdit) { this.AllowListEdit = allowListEdit; this.AllowItemEdit = allowItemEdit; this.Collection = new List<T>(); this.ReadOnlyCollection = new ReadOnlyCollection<T>(this.Collection); }
		public ReadOnlyList (List<T> collection, bool allowListEdit = false, bool allowItemEdit = false) : this(allowListEdit, allowItemEdit) { if (collection == null) { throw (new ArgumentNullException("collection")); } else { this.Collection.AddRange(collection); } }
		public ReadOnlyList (IEnumerable<T> collection, bool allowListEdit = false, bool allowItemEdit = false) : this(allowListEdit, allowItemEdit) { if (collection == null) { throw (new ArgumentNullException("collection")); } else { this.Collection.AddRange(collection); } }

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public bool HasItems { get { return (!this.IsEmpty); } }
		public bool IsEmpty { get { return (this.Count == 0); } }
		public int Capacity { get { return (this.Collection.Capacity); } set { this.ThrowListReadOnlyException(); this.Collection.Capacity = value; } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		private void ThrowItemReadOnlyException () { if (!this.AllowItemEdit) { throw (new ReadOnlyListItemEditException("This collection does not allow editing items.")); } }
		private void ThrowListReadOnlyException () { if (!this.AllowListEdit) { throw (new ReadOnlyListListEditException("This collection does not allow adding or removing items.")); } }

		public void AddRange (IEnumerable<T> collection) { this.ThrowListReadOnlyException(); this.Collection.AddRange(collection); }
		public ReadOnlyCollection<T> AsReadOnly () { return (this.ReadOnlyCollection); }
		public int BinarySearch (T item) { return (this.Collection.BinarySearch(item)); }
		public int BinarySearch (T item, IComparer<T> comparer) { return (this.Collection.BinarySearch(item, comparer)); }
		public int BinarySearch (int index, int count, T item, IComparer<T> comparer) { return (this.Collection.BinarySearch(index, count, item, comparer)); }
		public List<TOutput> ConvertAll<TOutput> (Converter<T, TOutput> converter) { return (this.Collection.ConvertAll<TOutput>(converter)); }
		public void CopyTo (T [] array) { this.Collection.CopyTo(array); }
		public void CopyTo (int index, T [] array, int arrayIndex, int count) { this.Collection.CopyTo(index, array, arrayIndex, count); }
		public bool Exists (Predicate<T> match) { return (this.Collection.Exists(match)); }
		public T Find (Predicate<T> match) { return (this.Collection.Find(match)); }
		public List<T> FindAll (Predicate<T> match) { return (this.Collection.FindAll(match)); }
		public int FindIndex (Predicate<T> match) { return (this.Collection.FindIndex(match)); }
		public int FindIndex (int startIndex, Predicate<T> match) { return (this.Collection.FindIndex(startIndex, match)); }
		public int FindIndex (int startIndex, int count, Predicate<T> match) { return (this.Collection.FindIndex(startIndex, count, match)); }
		public T FindLast (Predicate<T> match) { return (this.Collection.FindLast(match)); }
		public int FindLastIndex (Predicate<T> match) { return (this.Collection.FindLastIndex(match)); }
		public int FindLastIndex (int startIndex, Predicate<T> match) { return (this.Collection.FindLastIndex(startIndex, match)); }
		public int FindLastIndex (int startIndex, int count, Predicate<T> match) { return (this.Collection.FindLastIndex(startIndex, count, match)); }
		public void ForEach (Action<T> action) { this.Collection.ForEach(action); }
		public List<T> GetRange (int index, int count) { return (this.Collection.GetRange(index, count)); }
		public int IndexOf (T item, int index) { return (this.Collection.IndexOf(item, index)); }
		public int IndexOf (T item, int index, int count) { return (this.Collection.IndexOf(item, index, count)); }
		public void InsertRange (int index, IEnumerable<T> collection) { this.ThrowListReadOnlyException(); this.Collection.InsertRange(index, collection); }
		public int LastIndexOf (T item) { return (this.Collection.LastIndexOf(item)); }
		public int LastIndexOf (T item, int index) { return (this.Collection.LastIndexOf(item, index)); }
		public int LastIndexOf (T item, int index, int count) { return (this.Collection.LastIndexOf(item, index, count)); }
		public int RemoveAll (Predicate<T> match) { this.ThrowListReadOnlyException(); return (this.Collection.RemoveAll(match)); }
		public void RemoveRange (int index, int count) { this.ThrowListReadOnlyException(); this.Collection.RemoveRange(index, count); }
		public void Reverse () { this.Collection.Reverse(); }
		public void Reverse (int index, int count) { this.Collection.Reverse(index, count); }
		public void Sort () { this.Collection.Sort(); }
		public void Sort (Comparison<T> comparison) { this.Collection.Sort(comparison); }
		public void Sort (IComparer<T> comparer) { this.Collection.Sort(comparer); }
		public void Sort (int index, int count, IComparer<T> comparer) { this.Collection.Sort(index, count, comparer); }
		public T [] ToArray () { return (this.Collection.ToArray()); }
		public void TrimExcess () { this.Collection.TrimExcess(); }
		public bool TrueForAll (Predicate<T> match) { return (this.Collection.TrueForAll(match)); }

		public IQueue<T> AsQueue () { return (this as IQueue<T>); }
		public T PeekQueue () { return (this [this.Count - 1]); }
		public T Enqueue (T item) { this.ThrowListReadOnlyException(); this.Insert(0, item); return (item); }
		public T Dequeue () { this.ThrowListReadOnlyException(); var item = this [this.Count - 1]; this.RemoveAt(this.Count - 1); return (item); }

		public T PeekStack (int startIndex = 0) { return (this [startIndex]); }
		public T Push (T item, int startIndex = 0) { this.ThrowListReadOnlyException(); this.Insert(startIndex, item); return (item); }
		public T Pop (int startIndex = 0) { this.ThrowListReadOnlyException(); var item = this [startIndex]; this.RemoveAt(startIndex); return (item); }
		public IStack<T> AsStack () { return (this as IStack<T>); }

		#endregion Methods.

		#region Interface Implementation: IQueue<T>.

		//====================================================================================================
		// Interface Implementation: IQueue<T>.
		//====================================================================================================

		T IQueue<T>.Peek () { return (this [this.Count - 1]); }
		T IQueue<T>.Enqueue (T item) { this.ThrowListReadOnlyException(); this.Insert(this.Count, item); return (item); }
		T IQueue<T>.Dequeue () { this.ThrowListReadOnlyException(); var item = this [this.Count - 1]; this.RemoveAt(this.Count - 1); return (item); }

		#endregion Interface Implementation: IQueue<T>.

		#region Interface Implementation: IStack<T>.

		//====================================================================================================
		// Interface Implementation: IStack<T>.
		//====================================================================================================

		T IStack<T>.Peek () { return (this [0]); }
		T IStack<T>.Push (T item) { this.ThrowListReadOnlyException(); this.Insert(0, item); return (item); }
		T IStack<T>.Pop () { this.ThrowListReadOnlyException(); var item = this [0]; this.RemoveAt(0); return (item); }

		#endregion Interface Implementation: IStack<T>.

		#region Interface Implementation: IEnumerable<T>.

		//====================================================================================================
		// Interface Implementation: IEnumerable<T>.
		//====================================================================================================

		IEnumerator IEnumerable.GetEnumerator () { return (this.Collection.GetEnumerator()); }
		public IEnumerator<T> GetEnumerator () { return (this.ReadOnlyCollection.GetEnumerator()); }

		#endregion Interface Implementation: IEnumerable<T>.

		#region Interface Implementation: IList<T>.

		//====================================================================================================
		// Interface Implementation: IList<T>.
		//====================================================================================================

		public T this [int index] { get { return (this.Collection [index]); } set { this.ThrowItemReadOnlyException(); this.Collection [index] = value; } }

		public int IndexOf (T item) { return (this.Collection.IndexOf(item)); }
		public void RemoveAt (int index) { this.ThrowListReadOnlyException(); this.Collection.RemoveAt(index); }
		public void Insert (int index, T item) { this.ThrowListReadOnlyException(); this.Collection.Insert(index, item); }

		#endregion Interface Implementation: IList<T>.

		#region Interface Implementation: ICollection<T>.

		//====================================================================================================
		// Interface Implementation: ICollection<T>.
		//====================================================================================================

		public int Count { get { return (this.Collection.Count); } }
		public bool IsReadOnly { get { return (this.AllowListEdit); } }

		public bool Contains (T item) { return (this.Collection.Contains(item)); }
		public void Clear () { this.ThrowListReadOnlyException(); this.Collection.Clear(); }
		public void Add (T item) { this.ThrowListReadOnlyException(); this.Collection.Add(item); }
		public void CopyTo (T [] array, int arrayIndex) { this.Collection.CopyTo(array, arrayIndex); }
		public bool Remove (T item) { this.ThrowListReadOnlyException(); return (this.Collection.Remove(item)); }

		#endregion Interface Implementation: ICollection<T>.
	}
}