using Leadtools;
using Leadtools.Barcode;
using Leadtools.Codecs;
using Leadtools.Drawing;
using Leadtools.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public class Barcode
    {
        //===============
        #region Barcode
        //===============
        public static bool ReadBarcode(Image img, out List<LeadtoolsBarcodeData> barcodedata, out Exception exception)
        {
            bool result = false;
            barcodedata = null;
            exception = null;
            try
            {
                BarcodeEngine barcodeengine = new BarcodeEngine();
                BarcodeReader barcodereader = barcodeengine.Reader;
                RasterCodecs rastercodecs = new RasterCodecs();
                RasterImage rasterimge = Leadtools.Drawing.RasterImageConverter.ConvertFromImage(img, ConvertFromImageOptions.None);
                BarcodeData[] bdata = barcodereader.ReadBarcodes(rasterimge, LogicalRectangle.Empty, 0, null);

                foreach (var item in bdata)
                {
                    LeadtoolsBarcodeData leadtoolsbarcodedata = new LeadtoolsBarcodeData();
                    Rectangle rect = new Rectangle((int)item.Bounds.X, (int)item.Bounds.Y, (int)item.Bounds.Width, (int)item.Bounds.Height);
                    leadtoolsbarcodedata.Bounds = rect;
                    leadtoolsbarcodedata.RotationAngle = item.RotationAngle;
                    leadtoolsbarcodedata.Symbology = item.Symbology.ToString();
                    leadtoolsbarcodedata.Tag = item.Tag;
                    leadtoolsbarcodedata.Value = item.Value;
                    barcodedata.Add(leadtoolsbarcodedata);
                }
                if (barcodedata != null && barcodedata.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool WriteBarcode(Image img, string value, out Image barcodeimage, out Exception exception)
        {
            exception = null;
            bool result = false;
            barcodeimage = null;
            try
            {
                BarcodeEngine barcodeengine = new BarcodeEngine();
                BarcodeWriter barcodeWriter = barcodeengine.Writer;
                int Width = 0, Height = 0;
                Image BarcodeImage = new Bitmap(Width, Height);
                QRBarcodeData data = new QRBarcodeData();
                data.Symbology = BarcodeSymbology.QR;
                data.Value = value;
                data.Bounds = new LogicalRectangle(0, 0, 100, 100, LogicalUnit.Pixel);
                QRBarcodeWriteOptions writeOptions = new QRBarcodeWriteOptions();
                writeOptions.BackColor = RasterColor.White;
                writeOptions.ForeColor = RasterColor.Black;
                // Set X Module
                //writeOptions.XModule = 30;
                RasterImage barcodeRasterImage = RasterImageConverter.ConvertFromImage(BarcodeImage, ConvertFromImageOptions.None);
                barcodeWriter.WriteBarcode(barcodeRasterImage, data, writeOptions);
                barcodeimage = RasterImageConverter.ChangeToImage(barcodeRasterImage, ChangeToImageOptions.None);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            return result;
        }

        //===============
        #endregion Barcode
        //===============
    }
}
