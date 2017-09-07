using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	internal static class ProcessRestarter
	{
		private static int Main (string [] args)
		{
			int processId = 0;
			FileInfo file = null;
			var mode = ProcessRestarterMode.None;
			var code = ProcessRestarterExitCode.Unknown;

			Console.Title = "Process Restarter";

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Argument(s): [].", args.Length);

			if (args.Length > 0)
			{
				Console.WriteLine();

				for (int i = 0; i < args.Length; i++)
				{
					Console.Write(" - [{0}].", args [i]);
				}
			}

			Console.WriteLine();

			try
			{
			}
			catch (Exception exception)
			{
				Console.WriteLine();
				Console.Write("The operation could not be completed.");
				Console.WriteLine();
				Console.Write("{0}: [{1}].", exception.GetType().Name, exception.Message);
				Console.WriteLine();
				Console.WriteLine();
				Console.Write("Process Restarter Usage:");
				Console.WriteLine();
				Console.WriteLine();
				Console.Write("ProcessStarter.exe Restart TargetProcessId TargetProcessFilenameWithPath");
			}

			if (args.Length == 3)
			{
				if ((args [0] == ProcessRestarterMode.Test.ToString()) || (args [0] == ProcessRestarterMode.Restart.ToString()))
				{
					mode = (ProcessRestarterMode) Enum.Parse(typeof(ProcessRestarterMode), args [0]);

					if (int.TryParse(args [1], out processId))
					{
						if (processId <= 0)
						{
							code |= ProcessRestarterExitCode.ErrorArgumentProcessId;
							Console.WriteLine();
							Console.Write("The argument [Process Id] is illegal: [{0}].", args [1]);
						}
					}
					else
					{
						code |= ProcessRestarterExitCode.ErrorArgumentProcessId;
						Console.WriteLine();
						Console.Write("The argument [Process Id] is illegal: [{0}].", args [1]);
					}

					try
					{
						file = new FileInfo(args [3]);

						if (!file.Exists)
						{
							code |= ProcessRestarterExitCode.ErrorArgumentProcessFilename;
							Console.WriteLine();
							Console.Write("The argument [Process Filename] does not exist: [{0}].", args [2]);
						}
					}
					catch (Exception exception)
					{
						code |= ProcessRestarterExitCode.ErrorArgumentProcessFilename;
						Console.WriteLine();
						Console.Write("The argument [Process Filename] is illegal: [{0}].", args [2]);
						Console.WriteLine();
						Console.Write("{0}: [{1}].", exception.GetType().Name, exception.Message);
					}

					if (code == ProcessRestarterExitCode.Unknown)
					{
						try
						{
							using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
							}
						}
						catch (Exception exception)
						{
							Console.WriteLine();
							Console.Write("A read-only lock could not be acquired on: [{1}].", exception.GetType().Name, exception.Message);
							Console.WriteLine();
							Console.Write("{0}: [{1}].", exception.GetType().Name, exception.Message);
						}
					}
				}
				else
				{
					code = ProcessRestarterExitCode.ErrorArgumentMode;
					Console.Write("The argument [Mode] is illegal: [{0}].", args [0]);
				}
			}
			else
			{
				code = ProcessRestarterExitCode.ErrorArgumentCount;
				Console.Write("The wrong number of arguments were passed.");
			}

			return ((int) code);
		}
	}
}