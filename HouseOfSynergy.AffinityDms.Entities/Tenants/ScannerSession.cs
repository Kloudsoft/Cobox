//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Net;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.PowerTools.Library.Extensions;
//using HouseOfSynergy.PowerTools.Library.Interfaces;
//using HouseOfSynergy.PowerTools.Library.Utility;

//namespace HouseOfSynergy.AffinityDms.Entities.Tenants
//{
//	public partial class ScannerSession:
//		IEntity<ScannerSession>
//	{
//		public virtual long Id { get; set; }

//		public virtual DateTime StartDateTime { get; set; }
//        public virtual DateTime EndDateTime { get; set; }
//        public virtual string HardwareInfo { get; set; }
//        public virtual long ScanCount { get; set; }
//        public virtual long ScanCountFailed { get; set; }
//        public virtual long ScanCountSuccess{ get; set; }
//        public virtual string FeederType { get; set; }
//        public virtual string FilesPath { get; set; }
//        public virtual long SeparatorTypeId { get; set; }

//        public virtual long ScanningJobId { get; set; }
//        public virtual ScanningJob ScanningJob { get; set; }

//        public virtual long? UserId { get; set; }
//        public virtual User User { get; set; }

//		private ICollection<ScannerSessionDetail> _ScannerSessionDetails = null;
//		public virtual ICollection<ScannerSessionDetail> ScannerSessionDetails { get { if (this._ScannerSessionDetails == null) { this._ScannerSessionDetails = new List<ScannerSessionDetail>(); } return (this._ScannerSessionDetails); } protected set { this._ScannerSessionDetails = value; } }

//		public ScannerSession ()
//		{
//            this.ScannerSessionDetails = new List<ScannerSessionDetail>();
//		}
	
//		public void Initialize()
//		{
//		}

//		public ScannerSession Clone()
//		{
//			return (new ScannerSession().CopyFrom(this));
//		}

//		public ScannerSession CopyTo(ScannerSession destination)
//		{
//			return (destination.CopyFrom(this));
//		}

//		public ScannerSession CopyFrom(ScannerSession source)
//		{
//			return (ReflectionUtilities.Copy(source, this));
//		}

//		public XmlElement ToXmlElement(XmlDocument document)
//		{
//			var element = ReflectionUtilities.ToXmlElement(document, this);

//			return (element);
//		}

//		public ScannerSession FromXmlElement(XmlElement element)
//		{
//			ReflectionUtilities.FromXmlElement(this, element);

//			return (this);
//		}
//	}
//}