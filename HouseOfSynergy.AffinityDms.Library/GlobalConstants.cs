using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.Library
{
	public static class GlobalConstants
	{
		public const string FileExtensionCloud = @"cfe";
		public const string StringFormatDateTimeDisplay = @"yyyy-MM-dd HH\:mm";
		public const string StringFormatDateTimePersistence = @"yyyyMMddHHmmssffff";

		public const string AzureStorageEmulatorDefaultEndpointsProtocol = @"https";
		public const string AzureStorageEmulatorAccountName = @"devstoreaccount1";
		public const string AzureStorageEmulatorAccountKey = @"Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
		public const string AzureStorageEmulatorBlobEndpoint = @"https://127.0.0.1:10000/";
		public const string AzureStorageEmulatorQueueEndpoint = @"https://127.0.0.1:10001/";
		public const string AzureStorageEmulatorTableEndpoint = @"https://127.0.0.1:10002/";
		public const string AzureStorageEmulatorFileEndpoint = @"https://127.0.0.1:10003/";

		public const string AzureStorageEmulatorConnectionString
			= "DefaultEndpointsProtocol="
			+ GlobalConstants.AzureStorageEmulatorDefaultEndpointsProtocol
			+ ";AccountName="
			+ GlobalConstants.AzureStorageEmulatorAccountName
			+ ";AccountKey="
			+ GlobalConstants.AzureStorageEmulatorAccountKey
			+ ";BlobEndpoint="
			+ GlobalConstants.AzureStorageEmulatorBlobEndpoint
			+ ";QueueEndpoint="
			+ GlobalConstants.AzureStorageEmulatorQueueEndpoint
			+ ";TableEndpoint="
			+ GlobalConstants.AzureStorageEmulatorTableEndpoint
			+ ";FileEndpoint="
			+ GlobalConstants.AzureStorageEmulatorFileEndpoint
			+ ";";

		public const int ProtectedDataEntropySize = 256;

		public const int AlgorithmSymmetricKeySize = 256;
		public const int AlgorithmSymmetricInitializationVectorSize = 16 * 8;
		public const int AlgorithmSymmetricRfc2898DeriveBytesIterations = 1000;

		public static readonly int AlgorithmAsymmetricKeySize = AffinityConfiguration.IsConfigurationDebug ? 1024 : 4096;

		public static readonly string AlgorithmHashName = typeof(SHA512Managed).FullName;
		public static readonly string AlgorithmSymmetricName = typeof(RijndaelManaged).FullName;
		public static readonly string AlgorithmAsymmetricName = typeof(RSACryptoServiceProvider).FullName;

		public static readonly Encoding EncodingCryptography = Encoding.UTF8;
		public static readonly Sha.EnumShaKind AlgorithmHashShaKind = Sha.EnumShaKind.Sha512;

		public const int WebViewPageNumberMinimum = 0;
		public const int WebViewPageNumberDefault = GlobalConstants.WebViewPageNumberMinimum;

		public const int WebViewPageRowCountMinimum = 1;
		public const int WebViewPageRowCountDefault = 10;
		public const int WebViewPageRowCountMaximum = 100;
	}
}