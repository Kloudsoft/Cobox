using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
	[Serializable]
	public abstract class AffinityExceptionBase:
		Exception,
		ISerializable
	{
		public DateTime DateTime { get; set; }

		protected AffinityExceptionBase (string message)
			: base(message)
		{
			this.DateTime = DateTime.UtcNow;
		}

		protected AffinityExceptionBase (string message, Exception innerException)
			: base(message, innerException)
		{
			this.DateTime = DateTime.UtcNow;
		}

		protected AffinityExceptionBase (SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.DateTime = info.GetDateTime("DateTime");
		}

		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DateTime", this.DateTime);

			base.GetObjectData(info, context);
		}
	}

	public class AffinityException:
		AffinityExceptionBase
	{
		public AffinityException (string message) : base(message) { }
		public AffinityException (string message, Exception innerException) : base(message, innerException) { }
	}
}