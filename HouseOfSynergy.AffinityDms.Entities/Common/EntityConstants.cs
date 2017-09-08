using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public static class EntityConstants
	{
		public const int LengthUrl = 2083;
		public const int LengthEmail = 254;
		public const int LengthNameGiven = 35;
		public const int LengthNameFamily = 35;
        public const int LengthCompanyName = 500;
        public const int LengthSQLTableName = 30;
        public const int LengthCloudServiceName = 300;
		public const int LengthAddress = 999;
		public const int LengthFolderName = 256;
		public const int LengthFilename = 256;
		public const int LengthPhoneNumber = 256;

		public const int LengthCultureName = 999;

		public const int LengthCity = 300;
		public const int LengthCountry = 300;
		public const int LengthZipOrPostCode = 10;

		public const int LengthDomainNameMinimum = 1;
		public const int LengthDomainNameMaximum = 300;

        public const int LengthFullXmlSearchMaximum = 500;
		public const int LengthFullTextSearchMaximum = 999;

		public const int LengthUserNameMinimum = 1;
		public const int LengthUserNameMaximum = 100;
		public const int LengthPasswordMinimum = 8;
		public const int LengthPasswordMaximum = 100;
		public const int LengthPasswordHashMinimum = 8;
		public const int LengthPasswordHashMaximum = 999;
		public const int LengthPasswordSaltMinimum = 8;
		public const int LengthPasswordSaltMaximum = 999;
		public const int LengthUserTokenMinimum = 1;
		public const int LengthUserTokenMaximum = 999;

		public const int LengthNameGeneral = 100;
		public const int LengthDescriptionGeneral = 999;

		public static string TokenDelimiter { get { return ("|"); } }
	}
}