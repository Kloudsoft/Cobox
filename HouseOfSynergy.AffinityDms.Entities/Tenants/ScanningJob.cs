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
//	public partial class ScanningJob:
//		IEntity<ScanningJob>
//	{
//		public virtual long Id { get; set; }

//		public virtual string Description { get; set; }
//        public virtual DateTime StartDate { get; set; }
//        public virtual DateTime StartTime { get; set; }
//        public virtual DateTime EndDate { get; set; }
//        public virtual DateTime EndTime { get; set; }
//        public virtual string TargetScan { get; set; }
//        public virtual string CreatedBy { get; set; }
//        public virtual DateTime CreatedWhen { get; set; }
//        public virtual string ModifiedBy { get; set; }
//        public virtual DateTime ModifiedWhen { get; set; }

//        public virtual long UserId { get; set; }
//        public virtual User User { get; set; }

//		private ICollection<ScannerSession> _ScannerSessions = null;
//		public virtual ICollection<ScannerSession> ScannerSessions { get { if (this._ScannerSessions == null) { this._ScannerSessions = new List<ScannerSession>(); } return (this._ScannerSessions); } protected set { this._ScannerSessions = value; } }

//		public ScanningJob ()
//		{
//		}
	
//		public void Initialize()
//		{
//		}

//		public ScanningJob Clone()
//		{
//			return (new ScanningJob().CopyFrom(this));
//		}

//		public ScanningJob CopyTo(ScanningJob destination)
//		{
//			return (destination.CopyFrom(this));
//		}

//		public ScanningJob CopyFrom(ScanningJob source)
//		{
//			return (ReflectionUtilities.Copy(source, this));
//		}

//		public XmlElement ToXmlElement(XmlDocument document)
//		{
//			var element = ReflectionUtilities.ToXmlElement(document, this);

//			return (element);
//		}

//		public ScanningJob FromXmlElement(XmlElement element)
//		{
//			ReflectionUtilities.FromXmlElement(this, element);

//			return (this);
//		}
//	}
//}