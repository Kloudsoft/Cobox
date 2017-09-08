using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public class ApplicationVersion:
		IEntity<ApplicationVersion>
	{
		public virtual long Id { get; set; }

		private int _VersionMajor = 0;
		private int _VersionMinor = 0;
		private int _VersionBuild = 0;
		private int _VersionRevision = 0;
		private Version _Version = new Version();

		public virtual ApplicationModule ApplicationModule { get; set; }
		public virtual int VersionMajor { get { return (this._VersionMajor); } set { this._VersionMajor = value; this.UpdateVersion(); } }
		public virtual int VersionMinor { get { return (this._VersionMinor); } set { this._VersionMinor = value; this.UpdateVersion(); } }
		public virtual int VersionBuild { get { return (this._VersionBuild); } set { this._VersionBuild = value; this.UpdateVersion(); } }
		public virtual int VersionRevision { get { return (this._VersionRevision); } set { this._VersionRevision = value; this.UpdateVersion(); } }

		[NotMapped]
		public virtual Version Version { get { return (this._Version); } private set { this._Version = value; this.UpdateVersionReverse(); } }

		public ApplicationVersion ()
		{
		}

		public ApplicationVersion (Version version)
		{
			this._VersionMajor = version.Major;
			this._VersionMinor = version.Minor;
			this._VersionBuild = version.Build;
			this._VersionRevision = version.Revision;

			this.UpdateVersion();
		}

		public ApplicationVersion (int major, int minor, int build, int revision)
		{
			this.FromVersion(major, minor, build, revision);
		}

		public ApplicationVersion FromVersion (Version version)
		{
			this.FromVersion(version.Major, version.Minor, version.Build, version.Revision);

			return (this);
		}

		public ApplicationVersion FromVersion (int major, int minor, int build, int revision)
		{
			this._VersionMajor = major;
			this._VersionMinor = minor;
			this._VersionBuild = build;
			this._VersionRevision = revision;

			this.UpdateVersion();

			return (this);
		}

		private void UpdateVersion ()
		{
			this._Version = new Version(this._VersionMajor, this._VersionMinor, this._VersionBuild, this._VersionRevision);
		}

		private void UpdateVersionReverse ()
		{
			this._VersionMajor = this._Version.Major;
			this._VersionMinor = this._Version.Minor;
			this._VersionBuild = this._Version.Build;
			this._VersionRevision = this._Version.Revision;
		}

		public void Initialize ()
		{
		}

		public ApplicationVersion Clone ()
		{
			return (new ApplicationVersion().CopyFrom(this));
		}

		public ApplicationVersion CopyTo (ApplicationVersion destination)
		{
			return (destination.CopyFrom(this));
		}

		public ApplicationVersion CopyFrom (ApplicationVersion source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public ApplicationVersion FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}