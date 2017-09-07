using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Collections;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static XmlElement ToXmlElement<T> (this List<T> list, XmlDocument document)
			where T: class, IPersistXmlElement<T>, new()
		{
			var type = typeof(T);
			var element = document.CreateElement(type.Name);

			foreach (var item in list)
			{
				element.AppendChild(item.ToXmlElement(document));
			}

			return (element);
		}

		public static List<T> FromXmlElement<T> (this List<T> list, XmlElement element)
			where T: class, IPersistXmlElement<T>, new()
		{
			list.Clear();

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				var item = new T();

				item.FromXmlElement(element.ChildNodes [i] as XmlElement);

				list.Add(item);
			}

			return (list);
		}

		public static List<string> Clone (this List<string> list)
		{
			var clone = new List<string>();

			foreach (var item in list)
			{
				clone.Add(item);
			}

			return (clone);
		}

		public static List<T> Clone<T> (this List<T> list)
			where T: new()
		{
			var clone = new List<T>();

			foreach (var item in list)
			{
				clone.Add(item);
			}

			return (clone);
		}

		public static Dictionary<TKey, TValue> Clone<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
		{
			return (new Dictionary<TKey, TValue>(dictionary));
		}

		public static System.Collections.ObjectModel.ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
		{
			return (new System.Collections.ObjectModel.ReadOnlyDictionary<TKey, TValue>(dictionary));
		}

		public static NameValueCollection ToNameValueCollection<TKey, TValue> (this IDictionary<TKey, TValue> dictionary)
		{
			var nameValueCollection = new NameValueCollection();

			foreach (var pair in dictionary)
			{
				nameValueCollection.Add(pair.Key.ToString(), ((pair.Value == null) ? "" : pair.Value.ToString()));
			}

			return (nameValueCollection);
		}

		public static IDictionary<string, string> ToDictionary<TKey, TValue> (this NameValueCollection nameValueCollection)
		{
			var dictionary = new Dictionary<string, string>();

			foreach (var key in nameValueCollection.Keys)
			{
				dictionary.Add(key.ToString(), nameValueCollection [key.ToString()]);
			}

			return (dictionary);
		}

		public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue> (this NameValueCollection nameValueCollection, Func<string, object, KeyValuePair<TKey, TValue>> nameValuePairToKeyValuePairConvertor)
		{
			var dictionary = new Dictionary<TKey, TValue>();

			foreach (var key in nameValueCollection.Keys)
			{
				var pair = nameValuePairToKeyValuePairConvertor(key.ToString(), nameValueCollection [key.ToString()]);

				dictionary.Add(pair.Key, pair.Value);
			}

			return (dictionary);
		}

		public static List<T> GetSorted<T> (this List<T> list)
		{
			list.Sort();

			return (list);
		}

		public static List<T> GetSorted<T> (this List<T> list, IComparer<T> comparer)
		{
			list.Sort(comparer);

			return (list);
		}

		public static List<T> GetSorted<T> (this List<T> list, int index, int count, IComparer<T> comparer)
		{
			list.Sort(index, count, comparer);

			return (list);
		}

		public static List<T> GetSorted<T> (this List<T> list, Comparison<T> comparison)
		{
			list.Sort(comparison);

			return (list);
		}
	}
}