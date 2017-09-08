using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintasoft.Barcode;
using Vintasoft.Barcode.SymbologySubsets;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public class VintasoftBarcode
	{
		#region BarcodeWiter
		public byte [] ConvertImageToByteArray (Image img)
		{
			ImageConverter _imageConverter = new ImageConverter();
			byte [] xByte = (byte []) _imageConverter.ConvertTo(img, typeof(byte []));
			return xByte;
		}
		public void SaveImage (Image img, string imgpath)
		{

			ImageConverter _imageConverter = new ImageConverter();
			byte [] xByte = (byte []) _imageConverter.ConvertTo(img, typeof(byte []));
			Image bitmapimage = new Bitmap(new MemoryStream(xByte));
			using (FileStream fs = new FileStream(imgpath, FileMode.Create, FileAccess.ReadWrite))
			{
				fs.Write(xByte, 0, xByte.Length);
			}
		}
        public Image WriteBarcode(string data)
        {
            try
            {
                BarcodeWriter barcodewriter = new BarcodeWriter();
                barcodewriter.Settings.Barcode = Vintasoft.Barcode.BarcodeType.QR;
                barcodewriter.Settings.Value = data;
                Image barcodeimg = barcodewriter.GetBarcodeAsBitmap();
                return barcodeimg;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return (null);
            }
            //barcodeimg.Save(imgpath);

        }
		public Image WriteBarcode (string data, BarcodeType barcodetype)
		{
			try
			{
				BarcodeWriter barcodewriter = new BarcodeWriter();
				barcodewriter.Settings.Barcode = barcodetype;
				barcodewriter.Settings.Value = data;
				Image barcodeimg = barcodewriter.GetBarcodeAsBitmap();
				return barcodeimg;
			}
			catch (Exception ex)
			{
				Debug.Write(ex);
				return (null);
			}
			//barcodeimg.Save(imgpath);

		}
		#endregion BarcodeWiter

		#region BarcodeReader
        public VintasoftBarcodeInfo[] ReadBarcodeFromImage(Image barcodeimg)
		{
            int a = 1;
            VintasoftBarcodeInfo[] vintasoftbacodeinfo = new VintasoftBarcodeInfo[a];
			IBarcodeInfo [] infos;
			using (BarcodeReader barcodereader = new BarcodeReader())
			{
				// specify that reader must search for Code39, Code39Extended,
				// Code128, SSCC18 and DataMatrix barcodes
				barcodereader.Settings.ScanBarcodeTypes = BarcodeType.Code39 | BarcodeType.Code128 | BarcodeType.DataMatrix | BarcodeType.QR | BarcodeType.MicroQR;
				barcodereader.Settings.ScanBarcodeSubsets.Add(BarcodeSymbologySubsets.Code39Extended);
				barcodereader.Settings.ScanBarcodeSubsets.Add(BarcodeSymbologySubsets.SSCC18);
				barcodereader.Settings.ScanBarcodeSubsets.Add(BarcodeSymbologySubsets.GS1QR);
				barcodereader.Settings.ScanBarcodeSubsets.Add(BarcodeSymbologySubsets.XFACompressedQRCode);

				// specify that reader must search for horizontal and vertical barcodes only
				barcodereader.Settings.ScanDirection = ScanDirection.Horizontal | ScanDirection.Vertical;

				// use Automatic Recognition
				barcodereader.Settings.AutomaticRecognition = true;

				// read barcodes from image
				infos = barcodereader.ReadBarcodes(barcodeimg);
				// if barcodes are not detected
				if (infos.Length == 0)
				{
					return null;
				}
				// if barcodes are detected
				else
				{
					// get information about extracted barcodes
					foreach (IBarcodeInfo info in infos)
					{
                        vintasoftbacodeinfo[a - 1].BarcodeType = Convert.ToString(info.BarcodeType);
                        vintasoftbacodeinfo[a - 1].Value = Convert.ToString(info.Value);
                        vintasoftbacodeinfo[a - 1].Confidence = info.Confidence;
                        vintasoftbacodeinfo[a - 1].ReadingQuality = info.ReadingQuality;
                        vintasoftbacodeinfo[a - 1].Threshold = info.Threshold;
                        vintasoftbacodeinfo[a - 1].Region = Convert.ToString(info.Region);
                        a++;
					}
					//Console.WriteLine(string.Format("{0} barcodes found:", infos.Length));
					//Console.WriteLine();
					//for (int i = 0; i < infos.Length; i++)
					//{
					//    IBarcodeInfo info = infos[i];

					//    //Console.WriteLine(string.Format("[{0}:{1}]", i + 1, info.BarcodeType));
					//    //Console.WriteLine(string.Format("Value:      {0}", info.Value));
					//    //Console.WriteLine(string.Format("Region:     {0}", info.Region));
					//    //Console.WriteLine();
					//}
				}
			}
            return vintasoftbacodeinfo;
		}
		#endregion BarcodeReader
	}
}