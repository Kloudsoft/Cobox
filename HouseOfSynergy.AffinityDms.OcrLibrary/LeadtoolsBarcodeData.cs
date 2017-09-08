using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class LeadtoolsBarcodeData
    {
        public string Symbology { get; set; }
        public string Value { get; set; }
        public object Tag { get; set; }
        public Rectangle Bounds { get; set; }
        public int RotationAngle { get; set; }
    }
}
