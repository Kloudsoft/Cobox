using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework.CommandLine
{
	public interface ICommandLineArgumentKey
	{
		string Name { get; }
		bool IsOptional { get; }
		bool IsRequired { get; }
		bool AllowMultiple { get; }
	}
}