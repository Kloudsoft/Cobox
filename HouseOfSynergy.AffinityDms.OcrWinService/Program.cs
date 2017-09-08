using HouseOfSynergy.AffinityDms.OcrLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HouseOfSynergy.AffinityDms.OcrWinService
{
	internal static class Program
	{
		private static void Main ()
		{
			using (var global = new Global())
			{
				try
				{
					global.Initialize();

					var services = new ServiceBase []
					{
						new ServiceMain(global)
					};

					ServiceBase.Run(services);
				}
				catch (Exception exception)
				{
					var error = exception.ToString();

					Debug.Write(error);
					Console.Write(error);

					if (global.Initialized)
					{
						if (global.Logger != null)
						{
							try { global.Logger.Write(exception); } catch { }
						}
					}

					throw;
				}
			}
		}
	}
}