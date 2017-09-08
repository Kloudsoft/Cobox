using System;
using System.Collections.Generic;
using System.Linq;
namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class VintasoftBarcodeInfo
    {
        public string BarcodeType { get; set; }
        public string Value { get; set; }
        public double Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public int Threshold { get; set; }
        public string Region { get; set; }
    }
}
//str += "BarcodeType " + info.BarcodeType + "\n";
//                        str += "Value " + info.Value + "\n";
//                        str += "Confidence " + info.Confidence + "\n";
//                        str += "Reading Quality " + info.ReadingQuality + "\n";
//                        str += "Threshold " + info.Threshold + "\n";
//                        str += "Region " + info.Region + "\n";