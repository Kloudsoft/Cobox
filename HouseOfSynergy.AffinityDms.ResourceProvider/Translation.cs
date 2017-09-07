using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ResourceProvider.Classes
{
	public class Translation:
		IPersistXmlElement<Translation>,
		ICopyable<Translation>
	{
		public const int NameLengthMinimum = 1;
		public const int NameLengthMaximum = 999;
		public const string NameRegularExpressionPattern = @"^[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz]+[ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_]*$";

		public long Id { get; set; }

		public string Name { get; set; }

		private string _Comment = "";
		public string Comment { get { return (this._Comment); } set { this._Comment = (value ?? "").Trim(); } }

		public Translation ()
		{
		}

		public void Initialize ()
		{
		}

		public bool Validate (out string message)
		{
			var result = true;

			message = "";

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				result = false;
				message += Environment.NewLine + " - Translation name cannot be empty.";
			}

			if (!Regex.IsMatch(this.Name, Translation.NameRegularExpressionPattern, RegexOptions.Singleline))
			{
				result = false;
				message += Environment.NewLine + " - Translation name must start with a letter.";
				message += Environment.NewLine + " - Translation name can only contain letters, digits and underscores.";
				message += Environment.NewLine + " - Translation name length must be between " + Translation.NameLengthMinimum.ToString() + " and " + Translation.NameLengthMaximum.ToString() + " character(s) inclusive.";
			}

			if (!result) { message = "Translation has the following inconsistencies:" + Environment.NewLine + message; }

			return (result);
		}

		public Translation Clone ()
		{
			return (new Translation().CopyFrom(this));
		}

		public Translation CopyTo (Translation destination)
		{
			return (destination.CopyFrom(this));
		}

		public Translation CopyFrom (Translation source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Translation FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}