using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantTemplateController : Controller
    {
        // GET: TenantTemplate
        [HttpGet]
        public ActionResult Index(int? id, string ErrorMessage)
        {
            Exception exception = null;
            Template template = null;
            TenantUserSession tenantUserSession = null;

            try
            {
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (!(string.IsNullOrEmpty(ErrorMessage)))
                {
                    this.ViewBag.ErrorMessage = ErrorMessage;
                }
                else
                {
                    this.ViewBag.ErrorMessage = ErrorMessage = string.Empty;
                }
                if (id > 0)
                {
                    long ID = (long)id;
                    bool dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, ID, out template, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((template != null) && dbresult)
                    {
                        if (template.TemplateType == TemplateType.Template)
                        {
                            this.ViewBag.Id = ID;
                            this.ViewBag.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                    else
                    {
                        exception = new Exception("Unable to find the template");
                        throw exception;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                this.ViewBag.ErrorMessage = exception.Message;
            }

            return this.View("~/Views/Tenants/Templates/Template.cshtml", template);
        }

       
        public bool ThumbnailCallback() { return false; }

        public byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }


        [HttpPost]
        public ActionResult Index(Template template, HttpPostedFileBase file, string H_Templateid)
        {
            Exception exception = null;
            bool dbresult = false;
            long templateid = 0;
            try
            {
                TenantUserSession tenantUserSession = null;
                if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { throw (exception); }

                if (H_Templateid != null)
                {
                    //Document document = null;
                    //DocumentManagement.GetDocumentById(tenantUserSession, 1, out document, out exception);
                    //AzureCloudStorageAccountHelper azcsh = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                    //Stream s = file.InputStream;
                    //azcsh.DocumentUpload(tenantUserSession, document, s, new System.Threading.CancellationToken(false), out exception);
                    //throw (new Exception("SomeException"));
                    
                    templateid = long.Parse(H_Templateid);
                    Template sourcetemplate = null;
                    dbresult = ElementManagement.GetTemplateByTemplateId(tenantUserSession, templateid, out sourcetemplate, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if ((sourcetemplate != null) && dbresult)
                    {
                        if (sourcetemplate.TemplateType == TemplateType.Template)
                        {
                            sourcetemplate.EntityState = TemplateEntityState.Web;
                            sourcetemplate.Title = template.Title;
                            sourcetemplate.Description = template.Description;
                            

                            dbresult = ElementManagement.UpdateTemplate(tenantUserSession, sourcetemplate, out exception);

                            if (exception != null)
                            {
                                throw exception;
                            }
                            if (dbresult)
                            {
                                this.ViewBag.ErrorMessage = string.Empty;
                                return RedirectToAction("Index", "TenantTemplates", null);
                            }
                            else
                            {
                                Exception ex = new Exception("Unable to update the template");
                                throw ex;
                            }
                        }
                        else
                        {
                            throw (new Exception("The following template is not a form type."));
                        }
                    }
                }
                else
                {
                   
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {


                            int maxWidth = 900;
                            int maxHeight = 1273;

                            try
                            {
                                var DirectorRoot = Path.Combine(Server.MapPath("~/UploadedFiles/Templates/"));
                                var InputFileName = Path.GetFileName(file.FileName);
                                var ServerSavePathO = DirectorRoot + "Original/" + InputFileName;
                                file.SaveAs(ServerSavePathO);
                                                                 

                                Image image = Image.FromFile(ServerSavePathO, true);
                                Bitmap bmp = new Bitmap(image);


                                Bitmap cropbmp = CropWhiteSpace(bmp);

                                //Image newImage = Save(bmp, maxWidth, maxHeight, 50, ServerSavePath);
                                Bitmap newbmp = Save(cropbmp, maxWidth, maxHeight);

                                

                                var ServerSavePathR = DirectorRoot + "Resized/" + InputFileName;
                                newbmp.Save(ServerSavePathR);

                                byte[] imageByte = (byte[])(new ImageConverter()).ConvertTo(newbmp, typeof(byte[]));

                                template.TemplateImage = imageByte;

                                //cropbmp.Dispose();

                            }
                            catch (Exception ex) { }
                                                                                   

                            //template.TemplatePath = file.FileName; 
                        }
                        else
                        {
                            Exception ex = new Exception("Unable to find an image");
                            throw ex;
                        }
                    }
                    else
                    {
                        Exception ex = new Exception("Unable to find an image");
                        throw ex;
                    }
                    template.EntityState = TemplateEntityState.Web;
                    template.TemplateType = TemplateType.Template;
                    template.UserId = tenantUserSession.User.Id;
                    template.CheckedOutByUserId = tenantUserSession.User.Id;

                    dbresult = ElementManagement.CreateTemplate(tenantUserSession, template, out templateid, out exception);
                    if (exception != null)
                    {
                        throw exception;
                    }
                    if (templateid > 0 && (dbresult))
                    {
                        this.ViewBag.ErrorMessage = string.Empty;
                        //return RedirectToAction("Index", "TenantTemplateDesign", new { id = templateid });
                        return RedirectToAction("Index", "TenantTemplates", new { id = templateid });
                    }
                }
            }
            catch (Exception ex)
            {

                this.ViewBag.ErrorMessage = ExceptionUtilities.ExceptionToString(ex);
                return this.View("~/Views/Tenants/Templates/Template.cshtml", template);
            }
            return RedirectToAction("Index");
        }

        public static Bitmap CropWhiteSpace(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            int white = 0xffffff;

            Func<int, bool> allWhiteRow = r =>
            {
                for (int i = 0; i < w; ++i)
                    if ((bmp.GetPixel(i, r).ToArgb() & white) != white)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = c =>
            {
                for (int i = 0; i < h; ++i)
                    if ((bmp.GetPixel(c, i).ToArgb() & white) != white)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (!allWhiteRow(row))
                    break;
                topmost = row;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (!allWhiteRow(row))
                    break;
                bottommost = row;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (!allWhiteColumn(col))
                    break;
                leftmost = col;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (!allWhiteColumn(col))
                    break;
                rightmost = col;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        public Bitmap Save(Bitmap image, int maxWidth, int maxHeight)
        {

            var destRect = new Rectangle(0, 0, maxWidth, maxHeight);
            var destImage = new Bitmap(maxWidth, maxHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                //graphics.Dispose();
            }

            return destImage;

            ////=====

            //// Get the image's original width and height
            //int originalWidth = image.Width;
            //int originalHeight = image.Height;

            //// To preserve the aspect ratio
            //float ratioX = (float)maxWidth / (float)originalWidth;
            //float ratioY = (float)maxHeight / (float)originalHeight;
            //float ratio = Math.Min(ratioX, ratioY);

            //// New width and height based on aspect ratio
            //int newWidth = (int)(originalWidth * ratio);
            //int newHeight = (int)(originalHeight * ratio);

            //// Convert other formats (including CMYK) to RGB.
            //Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            //// Draws the image in the specified size with quality mode set to HighQuality
            //using (Graphics graphics = Graphics.FromImage(newImage))
            //{
            //    graphics.CompositingQuality = CompositingQuality.HighQuality;
            //    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    graphics.SmoothingMode = SmoothingMode.HighQuality;
            //    graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            //}

            //// Get an ImageCodecInfo object that represents the JPEG codec.
            //ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Bmp);

            //// Create an Encoder object for the Quality parameter.
            //System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            //// Create an EncoderParameters object. 
            //EncoderParameters encoderParameters = new EncoderParameters(1);

            //// Save the image as a JPEG file with quality level.
            //EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            //encoderParameters.Param[0] = encoderParameter;
            //newImage.Save(filePath, imageCodecInfo, encoderParameters);

            ////Stream

            //return newImage;

        }
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

    }
}