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
//	public partial class ScannerSessionDetail:
//		IEntity<ScannerSessionDetail>
//	{
//		public virtual long Id { get; set; }

//		public virtual long ScannerSessionId { get; set; }
//        public virtual ScannerSession ScannerSessions { get; set; }

//        public virtual DateTime DateTime { get; set; }
//        public virtual string FileName { get; set; }
//        public virtual string FileGuidName { get; set; }
//        public virtual string Ordinal { get; set; }

//		public ScannerSessionDetail ()
//		{

//		}
	
//		public void Initialize()
//		{
//		}

//		public ScannerSessionDetail Clone()
//		{
//			return (new ScannerSessionDetail().CopyFrom(this));
//		}

//		public ScannerSessionDetail CopyTo(ScannerSessionDetail destination)
//		{
//			return (destination.CopyFrom(this));
//		}

//		public ScannerSessionDetail CopyFrom(ScannerSessionDetail source)
//		{
//			return (ReflectionUtilities.Copy(source, this));
//		}

//		public XmlElement ToXmlElement(XmlDocument document)
//		{
//			var element = ReflectionUtilities.ToXmlElement(document, this);

//			return (element);
//		}

//		public ScannerSessionDetail FromXmlElement(XmlElement element)
//		{
//			ReflectionUtilities.FromXmlElement(this, element);

//			return (this);
//		}
//	}
//}