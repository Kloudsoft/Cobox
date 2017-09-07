using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Win32Api;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class StringUtilities
	{
		public static List<string> BreakSearchCriteria (string searchQuery)
		{
			var words = new List<string>();

			if (string.IsNullOrWhiteSpace(searchQuery)) { return (words); }

			var comparer = new StringCaseInsensitiveComparer();
			var list = searchQuery.Split(new string [] { " ", Environment.NewLine, Environment.NewLine [0].ToString(), Environment.NewLine [1].ToString(), }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var item in list)
			{
				var word = item.Trim().ToLower();
				var index = words.BinarySearch(item, comparer);

				if (index < 0) { words.Insert(~index, word); }
			}

			return (words);
		}

		public static List<string> BreakOcrXmlResult (string ocrXml)
		{
			var words = new List<string>();
			var document = new XmlDocument();

			document.LoadXml(ocrXml);

			var elementPages = document.DocumentElement;

			if (string.Compare(elementPages.Name, "pages", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<pages>] was not found.")); }

			for (int i = 0; i < elementPages.ChildNodes.Count; i++)
			{
				var elementPage = elementPages.ChildNodes [i] as XmlElement;

				if (string.Compare(elementPage.Name, "page", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<page>] was not found.")); }

				for (int j = 0; j < elementPage.ChildNodes.Count; j++)
				{
					var elementZone = elementPage.ChildNodes [j] as XmlElement;

					if (string.Compare(elementZone.Name, "zone", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<zone>] was not found.")); }

					for (int k = 0; k < elementZone.ChildNodes.Count; k++)
					{
						var elementParagraph = elementZone.ChildNodes [k] as XmlElement;

						if (string.Compare(elementParagraph.Name, "paragraph", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<paragraph>] was not found.")); }

						for (int l = 0; l < elementParagraph.ChildNodes.Count; l++)
						{
							var elementLine = elementParagraph.ChildNodes [l] as XmlElement;

							if (string.Compare(elementLine.Name, "line", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<line>] was not found.")); }

							for (int m = 0; m < elementLine.ChildNodes.Count; m++)
							{
								var elementWord = elementLine.ChildNodes [m] as XmlElement;

								if (string.Compare(elementWord.Name, "word", StringComparison.OrdinalIgnoreCase) != 0) { throw (new Exception("Expected document element [<word>] was not found.")); }

								var attribute = elementWord.Attributes ["value"];

								if (attribute == null)
								{
									throw (new Exception("Expected attribute [<value>] was not found in element [<word>] was not found."));
								}
								else
								{
									var add = true;
									var word = attribute.Value.Trim().ToLowerInvariant();

									var charactersToExclude = @"(){}<>'!#$?%^&*_+=;""\|	~`";

									charactersToExclude.ToList().ForEach(c => { word = word.Replace(c.ToString(), ""); });

									add &= ((add) && (word.Length > 1));
									add &= ((add) && (!string.IsNullOrWhiteSpace(word)));
									add &= ((add) && (word.Any(c => (char.IsLetter(c)))));

									if (add)
									{
										var index = words.BinarySearch(word);

										if (index < 0) { words.Insert(~index, word); }
									}
								}
							}
						}
					}
				}
			}

			return (words);
		}

		public static List<string> BreakOcrTextResult (string ocrText)
		{
			var text = ocrText;
			var words = new List<string>();
			var newLine = "{[<___NEW_LINE>]}";

			if (string.IsNullOrWhiteSpace(text)) { return (words); }

			text = text.Replace(Environment.NewLine, newLine);

			for (int i = 0; i < Environment.NewLine.Length; i++)
			{
				text = text.Replace(Environment.NewLine [i], ' ');
			}

			var comparer = new StringCaseInsensitiveComparer();
			var list = text.Split(new string [] { " ", Environment.NewLine, Environment.NewLine [0].ToString(), Environment.NewLine [1].ToString(), }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var item in list)
			{
				var word = item.Trim().ToLower();
				var index = words.BinarySearch(item, comparer);

				if (index < 0) { words.Insert(~index, word); }
			}

			return (words);
		}
	}
}