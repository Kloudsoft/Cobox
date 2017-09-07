using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Video
{
	/// <summary>
	/// Represents the number of frames per second.
	/// </summary>
	public enum VideoSampleRate:
		int
	{
		VideoSampleRate_0001 = 0001, // XXXXX.
		VideoSampleRate_0002 = 0002, // XXXXX.
		VideoSampleRate_0003 = 0003, // XXXXX.
		VideoSampleRate_0004 = 0004, // XXXXX.
		VideoSampleRate_0005 = 0005, // XXXXX.
		VideoSampleRate_0006 = 0006, // XXXXX.
		VideoSampleRate_0007 = 0007, // XXXXX.
		VideoSampleRate_0008 = 0008, // XXXXX.
		VideoSampleRate_0009 = 0009, // XXXXX.
		VideoSampleRate_0010 = 0010, // XXXXX.
		VideoSampleRate_0011 = 0011, // XXXXX.
		VideoSampleRate_0012 = 0012, // XXXXX.
		VideoSampleRate_0013 = 0013, // XXXXX.
		VideoSampleRate_0014 = 0014, // XXXXX.
		VideoSampleRate_0015 = 0015, // XXXXX.
		VideoSampleRate_0016 = 0016, // XXXXX.
		VideoSampleRate_0017 = 0017, // XXXXX.
		VideoSampleRate_0018 = 0018, // XXXXX.
		VideoSampleRate_0019 = 0019, // XXXXX.
		VideoSampleRate_0020 = 0020, // XXXXX.
		VideoSampleRate_0021 = 0021, // XXXXX.
		VideoSampleRate_0022 = 0022, // XXXXX.
		VideoSampleRate_0023 = 0023, // XXXXX.
		VideoSampleRate_0024 = 0024, // XXXXX.
		VideoSampleRate_0025 = 0025, // XXXXX.
		VideoSampleRate_0026 = 0026, // XXXXX.
		VideoSampleRate_0027 = 0027, // XXXXX.
		VideoSampleRate_0028 = 0028, // XXXXX.
		VideoSampleRate_0029 = 0029, // XXXXX.
		VideoSampleRate_0030 = 0030, // XXXXX.
		VideoSampleRate_0031 = 0031, // XXXXX.
		VideoSampleRate_0032 = 0032, // XXXXX.
		VideoSampleRate_0033 = 0033, // XXXXX.
		VideoSampleRate_0034 = 0034, // XXXXX.
		VideoSampleRate_0035 = 0035, // XXXXX.
		VideoSampleRate_0036 = 0036, // XXXXX.
		VideoSampleRate_0037 = 0037, // XXXXX.
		VideoSampleRate_0038 = 0038, // XXXXX.
		VideoSampleRate_0039 = 0039, // XXXXX.
		VideoSampleRate_0040 = 0040, // XXXXX.
		VideoSampleRate_0041 = 0041, // XXXXX.
		VideoSampleRate_0042 = 0042, // XXXXX.
		VideoSampleRate_0043 = 0043, // XXXXX.
		VideoSampleRate_0044 = 0044, // XXXXX.
		VideoSampleRate_0045 = 0045, // XXXXX.
		VideoSampleRate_0046 = 0046, // XXXXX.
		VideoSampleRate_0047 = 0047, // XXXXX.
		VideoSampleRate_0048 = 0048, // XXXXX.
		VideoSampleRate_0049 = 0049, // XXXXX.
		VideoSampleRate_0050 = 0050, // XXXXX.
		VideoSampleRate_0051 = 0051, // XXXXX.
		VideoSampleRate_0052 = 0052, // XXXXX.
		VideoSampleRate_0053 = 0053, // XXXXX.
		VideoSampleRate_0054 = 0054, // XXXXX.
		VideoSampleRate_0055 = 0055, // XXXXX.
		VideoSampleRate_0056 = 0056, // XXXXX.
		VideoSampleRate_0057 = 0057, // XXXXX.
		VideoSampleRate_0058 = 0058, // XXXXX.
		VideoSampleRate_0059 = 0059, // XXXXX.
		VideoSampleRate_0060 = 0060, // XXXXX.
		VideoSampleRate_0061 = 0061, // XXXXX.
		VideoSampleRate_0062 = 0062, // XXXXX.
		VideoSampleRate_0063 = 0063, // XXXXX.
		VideoSampleRate_0064 = 0064, // XXXXX.
		VideoSampleRate_0065 = 0065, // XXXXX.
		VideoSampleRate_0066 = 0066, // XXXXX.
		VideoSampleRate_0067 = 0067, // XXXXX.
		VideoSampleRate_0068 = 0068, // XXXXX.
		VideoSampleRate_0069 = 0069, // XXXXX.
		VideoSampleRate_0070 = 0070, // XXXXX.
		VideoSampleRate_0071 = 0071, // XXXXX.
		VideoSampleRate_0072 = 0072, // XXXXX.
		VideoSampleRate_0073 = 0073, // XXXXX.
		VideoSampleRate_0074 = 0074, // XXXXX.
		VideoSampleRate_0075 = 0075, // XXXXX.
		VideoSampleRate_0076 = 0076, // XXXXX.
		VideoSampleRate_0077 = 0077, // XXXXX.
		VideoSampleRate_0078 = 0078, // XXXXX.
		VideoSampleRate_0079 = 0079, // XXXXX.
		VideoSampleRate_0080 = 0080, // XXXXX.
		VideoSampleRate_0081 = 0081, // XXXXX.
		VideoSampleRate_0082 = 0082, // XXXXX.
		VideoSampleRate_0083 = 0083, // XXXXX.
		VideoSampleRate_0084 = 0084, // XXXXX.
		VideoSampleRate_0085 = 0085, // XXXXX.
		VideoSampleRate_0086 = 0086, // XXXXX.
		VideoSampleRate_0087 = 0087, // XXXXX.
		VideoSampleRate_0088 = 0088, // XXXXX.
		VideoSampleRate_0089 = 0089, // XXXXX.
		VideoSampleRate_0090 = 0090, // XXXXX.
		VideoSampleRate_0091 = 0091, // XXXXX.
		VideoSampleRate_0092 = 0092, // XXXXX.
		VideoSampleRate_0093 = 0093, // XXXXX.
		VideoSampleRate_0094 = 0094, // XXXXX.
		VideoSampleRate_0095 = 0095, // XXXXX.
		VideoSampleRate_0096 = 0096, // XXXXX.
		VideoSampleRate_0097 = 0097, // XXXXX.
		VideoSampleRate_0098 = 0098, // XXXXX.
		VideoSampleRate_0099 = 0099, // XXXXX.
		VideoSampleRate_0100 = 0100, // XXXXX.
		VideoSampleRate_0101 = 0101, // XXXXX.
		VideoSampleRate_0102 = 0102, // XXXXX.
		VideoSampleRate_0103 = 0103, // XXXXX.
		VideoSampleRate_0104 = 0104, // XXXXX.
		VideoSampleRate_0105 = 0105, // XXXXX.
		VideoSampleRate_0106 = 0106, // XXXXX.
		VideoSampleRate_0107 = 0107, // XXXXX.
		VideoSampleRate_0108 = 0108, // XXXXX.
		VideoSampleRate_0109 = 0109, // XXXXX.
		VideoSampleRate_0110 = 0110, // XXXXX.
		VideoSampleRate_0111 = 0111, // XXXXX.
		VideoSampleRate_0112 = 0112, // XXXXX.
		VideoSampleRate_0113 = 0113, // XXXXX.
		VideoSampleRate_0114 = 0114, // XXXXX.
		VideoSampleRate_0115 = 0115, // XXXXX.
		VideoSampleRate_0116 = 0116, // XXXXX.
		VideoSampleRate_0117 = 0117, // XXXXX.
		VideoSampleRate_0118 = 0118, // XXXXX.
		VideoSampleRate_0119 = 0119, // XXXXX.
		VideoSampleRate_0120 = 0120, // XXXXX.
	}
}