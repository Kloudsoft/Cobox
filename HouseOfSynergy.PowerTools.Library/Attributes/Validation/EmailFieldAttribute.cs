using System.Collections.Generic;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public sealed class EmailFieldAttribute:
		HouseOfSynergy.PowerTools.Library.Attributes.StringSingleLineFieldAttribute
	{
		public EmailFieldAttribute
		(
			System.Type Type,
			int Ordinal,
			string Name,
			string Description,
			string Label,
			string Tooltip,
			bool Required,
			bool ReadOnly
		)
			: base
			(
				Type,
				Ordinal,
				Name,
				Description,
				Label,
				Tooltip,
				Required,
				ReadOnly,
				5,
				250
			)
		{
		}

		public override void OnHelpTextRequest (List<string> helpTextLines)
		{
			base.OnHelpTextRequest(helpTextLines);

			helpTextLines.Add("This field requires a valid email address.");
		}

		protected override bool OnValidate ()
		{
			return (base.OnValidate() && ValidateEmail(this.TextBoxControl.Text));
		}

		public static List<char> PartLocalValidCharacters
		{
			get
			{
				List<char> list = new List<char>();

				list.AddRange(".-_".ToCharArray());

				for (int i = ((int) '0'); i <= ((int) '9'); i++)
					list.Add((char) i);
				for (int i = ((int) 'a'); i <= ((int) 'z'); i++)
					list.Add((char) i);
				for (int i = ((int) 'A'); i <= ((int) 'Z'); i++)
					list.Add((char) i);

				return (list);
			}
		}

		public static List<string> PartLocalInValidCombinations
		{
			get
			{
				List<string> list = new List<string>();

				list.Add("..");
				list.Add("--");
				list.Add(".-");
				list.Add("-.");

				return (list);
			}
		}

		public static List<string> PartLocalInValidStart
		{
			get
			{
				List<string> list = new List<string>();

				list.Add(".");
				list.Add("-");

				return (list);
			}
		}

		public static List<string> PartLocalInValidEnd
		{
			get
			{
				List<string> list = new List<string>();

				list.Add(".");
				list.Add("-");

				return (list);
			}
		}

		public static List<char> PartDomainValidCharacters
		{
			get
			{
				List<char> list = new List<char>();

				list.AddRange(".-".ToCharArray());

				for (int i = ((int) '0'); i <= ((int) '9'); i++)
					list.Add((char) i);
				for (int i = ((int) 'a'); i <= ((int) 'z'); i++)
					list.Add((char) i);
				for (int i = ((int) 'A'); i <= ((int) 'Z'); i++)
					list.Add((char) i);

				return (list);
			}
		}

		public static List<string> PartDomainInValidCombinations
		{
			get
			{
				List<string> list = new List<string>();

				list.Add("..");
				list.Add("--");
				list.Add("-.");
				list.Add(".-");

				return (list);
			}
		}

		public static List<string> PartDomainInValidStart
		{
			get
			{
				List<string> list = new List<string>();

				list.Add(".");
				list.Add("-");

				return (list);
			}
		}

		public static List<string> PartDomainInValidEnd
		{
			get
			{
				List<string> list = new List<string>();

				list.Add(".");
				list.Add("-");

				return (list);
			}
		}

		public override System.Windows.Forms.TextBox CreateTextBoxControl ()
		{
			base.CreateTextBoxControl();

			this.TextBoxControl.MaxLength = this.LengthMaximum;

			return (this.TextBoxControl);
		}

		public static bool ValidateEmail (string email)
		{
			if (email == null)
				return (false);

			if (email.Length < 5)
				return (false);

			if (email.Length > 250)
				return (false);

			int atCount = 0;
			foreach (char c in email)
			{
				if (c == '@')
				{
					atCount++;
				}
			}
			if (atCount != 1)
				return (false);

			if (email.StartsWith("@"))
				return (false);
			if (email.EndsWith("@"))
				return (false);

			if (!ValidateEmailPartLocal(email.Split("@".ToCharArray()) [0]))
				return (false);
			if (!ValidateEmailPartDomain(email.Split("@".ToCharArray()) [1]))
				return (false);

			return (true);
		}

		public static bool ValidateEmailCharacters (string email)
		{
			return (true);
		}

		public static bool ValidateEmailPartLocal (string part)
		{
			if (part.Length == 0)
				return (false);

			if (part.Length > 250)
				return (false);

			foreach (char c in part)
				if (!PartLocalValidCharacters.Contains(c))
					return (false);

			foreach (string invalidCombination in PartLocalInValidCombinations)
				if (part.Contains(invalidCombination))
					return (false);

			foreach (string invalidStart in PartLocalInValidStart)
				if (part.StartsWith(invalidStart))
					return (false);

			foreach (string invalidEnd in PartLocalInValidEnd)
				if (part.EndsWith(invalidEnd))
					return (false);

			return (true);
		}

		public static bool ValidateEmailPartDomain (string part)
		{
			if (part.Length < 2)
				return (false);

			if (part.Length > 250)
				return (false);

			foreach (char c in part)
				if (!PartDomainValidCharacters.Contains(c))
					return (false);

			foreach (string invalidCombination in PartDomainInValidCombinations)
				if (part.Contains(invalidCombination))
					return (false);

			foreach (string invalidStart in PartDomainInValidStart)
				if (part.StartsWith(invalidStart))
					return (false);

			foreach (string invalidEnd in PartDomainInValidEnd)
				if (part.EndsWith(invalidEnd))
					return (false);

			if (!part.Contains("."))
				return (false);

			return (true);
		}
	}
}