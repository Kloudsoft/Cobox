using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class Tag<T>
		where T: new()
	{
		public T Value { get; set; }
		//public List<T> List { get; private set; }
		//public Stack<T> Stack { get; private set; }
		//public Queue<T> Queue { get; private set; }
		//public HashSet<T> HashSet { get; private set; }
		//public SortedSet<T> SortedSet { get; private set; }
		//public LinkedList<T> LinkedList { get; private set; }
		//public Dictionary<string, T> Dictionary { get; private set; }
		//public SortedList<string, T> SortedList { get; private set; }
		//public SortedDictionary<string, T> SortedDictionary { get; private set; }
		//public ObservableCollection<T> ObservableCollection { get; private set; }

		public Tag ()
		{
			this.Value = default(T);
			//this.List = new List<T>();
			//this.Stack = new Stack<T>();
			//this.Queue = new Queue<T>();
			//this.HashSet = new HashSet<T>();
			//this.SortedSet = new SortedSet<T>();
			//this.LinkedList = new LinkedList<T>();
			//this.Dictionary = new Dictionary<string,T>();
			//this.SortedList = new SortedList<string,T>();
			//this.SortedDictionary = new SortedDictionary<string,T>();
			//this.ObservableCollection = new ObservableCollection<T>();
		}
	}
}