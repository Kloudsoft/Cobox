using Leadtools;
using Leadtools.Forms.Ocr;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class HelperMethods
    {
        public static int GetRotationAngle(IOcrPage ocrpage)
        {
			// Strange that IOcrPage is returning the rotation angle as an Int32 rather than a float.
            return (ocrpage.GetRotateAngle());
        }

        public static RasterImage DeskewImage(RasterImage rasterimg)
        {
            DeskewCommand deskewcom = new DeskewCommand();
            deskewcom.Flags = DeskewCommandFlags.DeskewImage | DeskewCommandFlags.DoNotFillExposedArea;
            deskewcom.Run(rasterimg);
            return rasterimg;
        }

        public static RasterImage RotateImage(int rotation, RasterImage rasterImageToMatch)
        {
			if (rotation != 0)
			//if (!(!(!(rotation == 0))))
            {
                RotateCommand rotate = new RotateCommand();
                rotate.Angle = rotation * 100;
                rotate.FillColor = RasterColor.FromKnownColor(RasterKnownColor.White);
                rotate.Flags = RotateCommandFlags.Resize;
                rotate.Run(rasterImageToMatch);
            }
            return rasterImageToMatch;
        }
    }
}
