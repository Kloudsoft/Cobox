using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.Internal
{
	internal class WinErrorTools
	{
		public static void DownloadErrorPages ()
		{
			// Download from:
			//	http://msdn.microsoft.com/en-us/library/windows/desktop/ms681381(v=vs.85).aspx.
		}

		public static void GenerateConstants ()
		{
			var builderConstants = new StringBuilder();
			var builderEnumeration = new StringBuilder();
			var dictionary = new Dictionary<string, string>();
			var directory = new DirectoryInfo(@"C:\Users\Raheel Khan\Desktop\Errors");
			var files = directory.GetFiles("System Error Codes - *.html", SearchOption.TopDirectoryOnly).ToList();
			var fileOutPut = new FileInfo(Path.Combine(directory.FullName, "WinErrorGenerated.cs"));
			var fileTemplate = new FileInfo(Path.Combine(directory.FullName, "WinError Template.txt"));

			if (!fileTemplate.Exists) { throw (new FileNotFoundException("The template file was not found.", fileTemplate.FullName)); }

			foreach (var f in files)
			{
				var pattern = @"<dt><a id=""";
				string html = File.ReadAllText(f.FullName);
				var matches = Regex.Matches(html, pattern);

				var list = new List<Tuple<string, string, string>>();

				foreach (Match match in matches)
				{
					var name = "";
					var value = "0";
					var description = ".";

					var line = html.Substring(match.Index, html.IndexOf("</dd>", match.Index) - match.Index);

					name = line.Substring((line.IndexOf("<strong>") + 8), line.IndexOf("</strong>") - (line.IndexOf("<strong>") + 8));
					value = line.Substring(line.IndexOf("<dt>", 1) + 4);
					value = value.Substring(0, value.IndexOf("</dt>"));
					value = value.Replace(" (", "; // (");
					description = line.Substring(line.IndexOf("<p>", 1) + 3);
					description = description.Substring(0, description.IndexOf("</p>"));
					description = description.Replace(Environment.NewLine [0].ToString(), " ");
					description = description.Replace(Environment.NewLine [1].ToString(), " ");
					description = description.Replace(Environment.NewLine, " ");
					while (description.Contains("  ")) { description = description.Replace("  ", " "); }

					var tuple = new Tuple<string, string, string>(name, value, description);

					var entry
						= "		/// <summary>"
						+ Environment.NewLine
						+ "		/// "
						+ tuple.Item1
						+ " ("
						+ tuple.Item2
						+ ")."
						+ Environment.NewLine
						+ "		/// "
						+ tuple.Item3
						+ Environment.NewLine
						+ "		/// "
						+ f.Name
						+ "."
						+ Environment.NewLine
						+ "		/// </summary>"
						+ Environment.NewLine
						+ (tuple.Item1.Contains("*") ? ("		// public const uint ") : ("		public const uint "))
						+ tuple.Item1
						+ " = "
						+ tuple.Item2
						;

					builderConstants.AppendLine(entry);

					entry
						= "			/// <summary>"
						+ Environment.NewLine
						+ "			/// "
						+ tuple.Item1
						+ " ("
						+ tuple.Item2
						+ ")."
						+ Environment.NewLine
						+ "			/// "
						+ tuple.Item3
						+ Environment.NewLine
						+ "			/// "
						+ f.Name
						+ "."
						+ Environment.NewLine
						+ "			/// </summary>"
						+ Environment.NewLine
						+ "			"
						+ (tuple.Item1.Contains("*") ? ("// " + tuple.Item1) : (tuple.Item1))
						+ " = WinError."
						+ tuple.Item1
						+ ","
						;

					builderEnumeration.AppendLine(entry);

					//Console.WriteLine();
					//Console.Write(entry);
				}

				//break;
			}

			var template = File.ReadAllText(fileTemplate.FullName);

			template = template.Replace("<<<CONSTANTS>>>", builderConstants.ToString());
			template = template.Replace("<<<ENUM_ENTRIES>>>", builderEnumeration.ToString());

			File.WriteAllText(fileOutPut.FullName, template);

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Press any key to continue...");
			Console.ReadKey();
		}
	}
}