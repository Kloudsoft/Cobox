using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leadtools;
using Leadtools.Drawing;
using Leadtools.Forms.Ocr;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;
using Leadtools.ImageProcessing.Effects;
using System.Web;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public static class LeadToolsImageEnhancement
	{
		#region GreyScale
		public static RasterImage ConvertToGreyScale (RasterImage rasterImage)
		{

			return rasterImage;
		}
		#endregion GreyScale
		#region DeskewImage
		public static RasterImage DeskewImage (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Flags = DeskewCommandFlags.DeskewImage;
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage DeskewImage (RasterImage rasterImage, DeskewCommandFlags deskewFlag)
		{
			DeskewCommand command = new DeskewCommand();
			command.Flags = deskewFlag;
			command.Run(rasterImage);
			return rasterImage;
		}

		public static RasterImage DeskewImage (RasterImage rasterImage, RasterColor rasterColor, DeskewCommandFlags deskewFlags)
		{
			if (rasterImage.IsGray)
			{
				rasterColor = new RasterColor(0, 0, 0);
			}
			DeskewCommand command = new DeskewCommand(rasterColor, deskewFlags);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static int GetDeskewAngle (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Run(rasterImage);
			int deskewangle = Convert.ToInt32(command.Angle / 100);
			return deskewangle;
		}
		public static int GetDeskewAngleRange (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Run(rasterImage);
			int deskewanglerange = command.AngleRange;
			return deskewanglerange;
		}
		public static int GetDeskewAngleResolution (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Run(rasterImage);
			int deskewangleresolution = command.AngleResolution;
			return deskewangleresolution;
		}
		public static DeskewCommandFlags GetDeskewFlags (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Run(rasterImage);
			DeskewCommandFlags deskewflags = command.Flags;
			return deskewflags;
		}
		public static RasterColor GetDeskewFillColor (RasterImage rasterImage)
		{
			DeskewCommand command = new DeskewCommand();
			command.Run(rasterImage);
			RasterColor deskewrastercolor = command.FillColor;
			return deskewrastercolor;
		}
		#endregion DeskewImage
		#region ShearImage
		public static RasterImage ShearImage (RasterImage rasterImage)
		{
			ShearCommand command = new ShearCommand();
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage ShearImage (RasterImage rasterImage, RasterColor rasterColor, int angle, bool isHorizontal)
		{
			if (rasterImage.IsGray)
			{
				rasterColor = new RasterColor(0, 0, 0);
			}
			ShearCommand command = new ShearCommand((angle * 100), isHorizontal, rasterColor);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static int GetShearImageAngle (RasterImage rasterImage)
		{
			ShearCommand command = new ShearCommand();
			command.Run(rasterImage);
			int angle = Convert.ToInt32(command.Angle / 100);
			return angle;
		}
		public static bool IsShearImageHorizontal (RasterImage rasterImage)
		{
			ShearCommand command = new ShearCommand();
			command.Run(rasterImage);
			bool horizontal = command.Horizontal;
			return horizontal;
		}
		public static RasterColor GetShearFillColor (RasterImage rasterImage)
		{
			ShearCommand command = new ShearCommand();
			command.Run(rasterImage);
			RasterColor deskewrastercolor = command.FillColor;
			return deskewrastercolor;
		}
		#endregion ShearImage
		#region RotateImage
		public static RasterImage RotateImage (RasterImage rasterImage)
		{
			RotateCommand command = new RotateCommand();
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage RotateImage (RasterImage rasterImage, RotateCommandFlags rotateCommandFlags)
		{
			RotateCommand command = new RotateCommand();
			command.Flags = rotateCommandFlags;
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage RotateImage (RasterImage rasterImage, RasterColor rasterColor, RotateCommandFlags rotateCommandFlags, int angle)
		{
			RotateCommand command = new RotateCommand((angle * 100), rotateCommandFlags, rasterColor);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static int GetRotationAngle (RasterImage rasterImage)
		{
			RotateCommand command = new RotateCommand();
			command.Run(rasterImage);
			return Convert.ToInt32(command.Angle / 100);
		}
		public static RasterColor GetFillColor (RasterImage rasterImage)
		{
			RotateCommand command = new RotateCommand();
			command.Run(rasterImage);
			return command.FillColor;
		}
		public static RotateCommandFlags GetRotationFlags (RasterImage rasterImage)
		{
			RotateCommand command = new RotateCommand();
			command.Run(rasterImage);
			return command.Flags;
		}
		#endregion
		#region FlipImage
		public static RasterImage FlipImage (RasterImage rasterImage)
		{
			FlipCommand command = new FlipCommand();
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage FlipImage (RasterImage rasterImage, bool flipHorizontal)
		{
			FlipCommand command = new FlipCommand(flipHorizontal);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static bool IsHorizontalFlip (RasterImage rasterImage)
		{
			FlipCommand command = new FlipCommand();
			command.Run(rasterImage);
			return command.Horizontal;
		}
		#endregion FlipImage
		#region Effects
		#region Blur
		#region Average
		public static RasterImage AverageImage (RasterImage rasterImage)
		{
			AverageCommand command = new AverageCommand();
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage AverageImage (RasterImage rasterImage, int dimension)
		{
			AverageCommand command = new AverageCommand(dimension);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static int GetAverageDimension (RasterImage rasterImage)
		{
			AverageCommand command = new AverageCommand();
			command.Run(rasterImage);
			return command.Dimension;
		}
		#endregion Average
		#endregion Blue
		#region Edge
		#region Detection
		public static RasterImage EdgeDetector (RasterImage rasterImage)
		{
			EdgeDetectorCommand command = new EdgeDetectorCommand();
			command.Run(rasterImage);
			return rasterImage;
		}
		public static RasterImage EdgeDetector (RasterImage rasterImage, EdgeDetectorCommandType filter, int threshold)
		{
			EdgeDetectorCommand command = new EdgeDetectorCommand(threshold, filter);
			command.Run(rasterImage);
			return rasterImage;
		}
		public static EdgeDetectorCommandType GetEdgeDetectorFilter (RasterImage rasterImage)
		{
			EdgeDetectorCommand command = new EdgeDetectorCommand();
			command.Run(rasterImage);
			return command.Filter;
		}
		public static int GetEdgeDetectorThreshold (RasterImage rasterImage)
		{
			EdgeDetectorCommand command = new EdgeDetectorCommand();
			command.Run(rasterImage);
			return command.Threshold;
		}
		#endregion Detection
		#endregion Edge
		#endregion Effects
		#region Binarize
		#region Auto Binarize
		public static RasterImage AutoBinarize (RasterImage rasterImage)
		{
			if (rasterImage.BitsPerPixel != 1)
			{
				AutoBinarizeCommand autoBin = new AutoBinarizeCommand();
				autoBin.Flags = AutoBinarizeCommandFlags.UseAutoPreProcessing;
				autoBin.Run(rasterImage);
				ColorResolutionCommand colorRes = new ColorResolutionCommand();
				colorRes.BitsPerPixel = 1;
				colorRes.Run(rasterImage);
			}
			return rasterImage;
		}
		public static RasterImage AutoBinarize (RasterImage rasterImage, AutoBinarizeCommandFlags autoBinarizeCommandFlags)
		{
			if (rasterImage.BitsPerPixel != 1)
			{
				AutoBinarizeCommand autoBin = new AutoBinarizeCommand();
				autoBin.Flags = autoBinarizeCommandFlags;
				autoBin.Run(rasterImage);
				ColorResolutionCommand colorRes = new ColorResolutionCommand();
				colorRes.BitsPerPixel = 1;
				colorRes.Run(rasterImage);
			}
			return rasterImage;
		}
		#endregion Auto Binarize
		#endregion Binarize
		#region Conversion
		public static RasterImage ConvertImageToRasterImage (Image image)
		{
			RasterImage rasterimage = RasterImageConverter.ConvertFromImage(image, ConvertFromImageOptions.None);
			return rasterimage;
		}
		public static Image ConvertRasterImageToImage (RasterImage rasterImage)
		{
			Image image = RasterImageConverter.ConvertToImage(rasterImage, ConvertToImageOptions.None);
			return image;
		}
		public static Image ConvertRasterImageToImage (RasterImage rasterImage, bool Alpha)
		{
			Image image;
			if (Alpha == true)
			{
				image = RasterImageConverter.ConvertToImage(rasterImage, ConvertToImageOptions.InitAlpha);
			}
			else
			{
				image = RasterImageConverter.ConvertToImage(rasterImage, ConvertToImageOptions.None);
			}

			return image;
		}

		#endregion Conversion
		#region AutoPreProccess
		//public static bool AutoPreproccess(RasterImage rasterimage,out Exception exception,out IOcrPage ocrpage)
		//{
		//    exception = null;
		//    bool result = false;
		//    ocrpage = null;
		//    try
		//    {
		//        LeadToolsOCR leadtoolsOCR = new LeadToolsOCR();
		//        if (leadtoolsOCR.exception != null)
		//        {
		//            throw exception;
		//        }

		//        IOcrEngine ocrengine;
		//        leadtoolsOCR.LoadEngine(null, null, null, out ocrengine, out exception);
		//        if (exception != null)
		//        {
		//            throw exception;
		//        }
		//        IOcrDocument ocrDocument = ocrengine.DocumentManager.CreateDocument();
		//        ocrpage = ocrDocument.Pages.AddPage(rasterimage, null);
		//        ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
		//        ocrpage.Recognize(null);
		//        ocrengine.Shutdown();
		//        ocrengine.Dispose();
		//        rasterimage.Dispose();
		//        result = true;
		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        exception = ex;
		//        return result;
		//    }
		//}
		public static bool AutoPreproccess (RasterImage rasterimage, out RasterImage ProccessedrasterImage, out Exception exception)
		{
			exception = null;
			bool result = false;
			ProccessedrasterImage = null;
			try
			{

                string serverpath = HttpContext.Current.Server.MapPath("~");
                LeadToolsOCR leadtoolsOCR = new LeadToolsOCR(serverpath,string.Empty,string.Empty,out exception);
				if (exception != null) { throw exception; }

				IOcrEngine ocrengine;
				leadtoolsOCR.LoadEngine(null, null, null, out ocrengine, out exception);
				if (exception != null)
				{
					throw exception;
				}
				IOcrDocument ocrDocument = ocrengine.DocumentManager.CreateDocument();
				IOcrPage ocrpage = ocrDocument.Pages.AddPage(rasterimage, null);
				ocrpage.AutoPreprocess(OcrAutoPreprocessPageCommand.All, null);
				ProccessedrasterImage = ocrpage.GetRasterImage(OcrPageType.Processing);
				ocrengine.Shutdown();
				ocrengine.Dispose();
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				exception = ex;
				return result;
			}

		}
		#endregion AutoPreProccess


		//        public static Hist
		//tmpImg.GrayscaleMode != RasterGrayscaleMode.None)
	}
}