using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.OcrLibrary;

namespace HouseOfSynergy.AffinityDms.ConsoleClient
{
	public sealed class WorkerRoleParameters
	{
		public OcrEngineSettings OcrEngineSettings { get; private set; }
		public CancellationToken CancellationToken { get; private set; }

		public WorkerRoleParameters (OcrEngineSettings ocrEngineSettings, CancellationToken cancellationToken)
		{
			if (ocrEngineSettings == null) { throw (new ArgumentNullException("ocrEngineSettings")); }
			if (cancellationToken == null) { throw (new ArgumentNullException("cancellationToken")); }

			this.OcrEngineSettings = ocrEngineSettings;
			this.CancellationToken = cancellationToken;
		}
	}
}
