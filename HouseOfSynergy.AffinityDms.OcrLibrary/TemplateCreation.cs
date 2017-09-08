using HouseOfSynergy.AffinityDms.Entities.Tenants;
using Leadtools.Barcode;
using Leadtools.Codecs;
using Leadtools.Forms.Recognition;
using Leadtools.Forms.Auto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leadtools;
using Leadtools.Forms.Processing;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Recognition.Ocr;
using Leadtools.Forms.Ocr;
using Leadtools.Drawing;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using Leadtools.Forms;
using HouseOfSynergy.AffinityDms.Entities.Common;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using HouseOfSynergy.AffinityDms.BusinessLayer;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{


    //Dictionary                        : Definition
    //RasterCodecs                      : Provides support for loading and saving raster image files.
    //DocumentWriter                    : Support for creating document files such as PDF, XPS, DOC, HTML, RTF, or Text from Scalable Vector Graphics (SVG), Windows Enhanced Meta Files (EMF) or raster images.
    //IOcrEngine                        : Provides support for OCR functionality in LEADTOOLS.
    //IMasterFormsCategory              : Represents a category inside a IMasterFormsRepository that may have Master Forms and/or other sub-categories (child categories). It provides methods to add and delete Master Forms, sub-categories (child categories), and category information such as its name and its parent category.
    //DiskMasterFormsRepository         :Implements the IMasterFormsRepository interface for Master Forms stored on local disk.




    /// <summary>
    /// Create Template Files (bin/xml/tiff)
    /// </summary>
    public sealed class TemplateCreation
    {
        /// <summary>
        /// Adds a New Master Form
        /// </summary>
        /// <param name="ocrEngineSettings">OCR engine settings</param>
        /// <param name="entieselements">List of Tempalte Elements to Save</param>
        /// <param name="entieselementsdetails">List of Template Element Details to save</param>
        /// <param name="templatename">Name of the template files to save</param>
        /// <param name="templateimage">Image of the template</param>
        public void AddMasterForm(OcrEngineSettings ocrEngineSettings, Tenant tenant, List<TemplateElement> entieselements, List<List<TemplateElementDetail>> entieselementsdetails, string templatename, Image templateimage, out Exception exception)
        {
			exception = null;

			try
			{
                var barcodeengine = new BarcodeEngine();
                var formrecogengine = new FormRecognitionEngine();

				using (var formsCodec = new RasterCodecs ())
				{
					IMasterFormsCategory parentCategory;

					//=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
					//Need a unique folder that will be set as ocrEngineSettings.WorkDirectory
					//We will be needing:
					//A UniqueFolderName
					//Some method to create that folder
					//Some method to delete that folder and all the files it contains
					//Some method to copy all the files from this folder to another folder
					//=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

					using (var container = ocrEngineSettings.CreateTempFolderContainer (tenant))
					{
						var options = new FormRecognitionOptions ();
						var formattributes = formrecogengine.CreateMasterForm (templatename, new Guid (), options);
						var workingRepository = new DiskMasterFormsRepository (formsCodec, container.Directory.FullName);

						parentCategory = workingRepository.RootCategory;

						formrecogengine.CloseMasterForm (formattributes);
						parentCategory.AddMasterForm (formattributes, null, (RasterImage) null);

						var currentMasterForm = parentCategory.MasterForms [0] as DiskMasterForm;

						var formPages = currentMasterForm.ReadFields ();

						using (var formImage = currentMasterForm.ReadForm ())
						{
							DocumentWriter docwriter = null;
							var optionssss = new PageRecognitionOptions ();

							using (var rasterImage = RasterImageConverter.ConvertFromImage (templateimage, ConvertFromImageOptions.None))
							{
								using (var ocrengine = Engines.LoadEngine (formsCodec, docwriter, container.Directory.FullName, ocrEngineSettings.PathRuntime.FullName))
								{
									if (exception != null) { throw (exception); }
									var ocrobjmanager = new OcrObjectsManager (ocrengine);
									formrecogengine.ObjectsManagers.Add (ocrobjmanager);
									for (int i = 1; i <= rasterImage.PageCount; i++)
									{
										rasterImage.Page = i;
										formrecogengine.OpenMasterForm (formattributes);
										formrecogengine.InsertMasterFormPage (-1, formattributes, rasterImage.Clone (), optionssss, null);
										formrecogengine.CloseMasterForm (formattributes);
									}

									if (formImage != null) { formImage.AddPages (rasterImage.CloneAll (), 1, rasterImage.PageCount); }

									using (var composedImage = (formImage == null) ? rasterImage.CloneAll () : formImage)
									{
										if (formPages != null)
										{
											for (int i = 0; i < rasterImage.PageCount; i++)
												formPages.Add (new FormPage (formPages.Count + 1, rasterImage.XResolution, rasterImage.YResolution));
										}
										else
										{
											//No processing pages exist so we must create them
											var tempProcessingEngine = new FormProcessingEngine ();

											tempProcessingEngine.OcrEngine = ocrengine;
											tempProcessingEngine.BarcodeEngine = barcodeengine;

											for (int i = 0; i < formrecogengine.GetFormProperties (formattributes).Pages; i++)
												tempProcessingEngine.Pages.Add (ProccessMasterFormField (ocrEngineSettings, ocrengine, entieselements, entieselementsdetails, rasterImage));

											formPages = tempProcessingEngine.Pages;
										}

										currentMasterForm.WriteForm (composedImage);
										currentMasterForm.WriteAttributes (formattributes);
										currentMasterForm.WriteFields (formPages);

										var templatefiles = container.Directory.GetFiles();

										foreach (var file in templatefiles)
										{
                                            file.CopyTo (Path.Combine (ocrEngineSettings.GetPathDataTenantTemplates (tenant).FullName, file.Name), true);
                                            using (var azureCloudServiceHelper = new AzureCloudStorageAccountHelper(tenant))
                                            {
                                                bool filecopied = azureCloudServiceHelper.UploadTemplateFilesToBlob(tenant, file, out exception);
                                                if (exception != null)
                                                {
                                                    throw exception;
                                                }
                                                if (!filecopied)
                                                {
                                                    throw (new Exception("Unable to upload template to storage"));
                                                }
                                            }

                                        }
									}
								}
							}
						}
					}
				}
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
        }

       


        /// <summary>
        /// Proccesses Elements and Saves it as an master form field
        /// </summary>
        /// <param name="ocrEngineSettings"></param>
        /// <param name="ocrengine"></param>
        /// <param name="entieselement"></param>
        /// <param name="entieselementdetails"></param>
        /// <param name="rasterImage"></param>
        /// <returns></returns>
        public FormPage ProccessMasterFormField(OcrEngineSettings ocrEngineSettings, IOcrEngine ocrengine, List<TemplateElement> entieselement, List<List<TemplateElementDetail>> entieselementdetails, RasterImage rasterImage)
        {
            var formpage = new FormPage(rasterImage.Page, rasterImage.XResolution, rasterImage.YResolution);
            var formproccessingengine = new FormProcessingEngine();
            // LoadEngine(formsCodec, docwriter, ocrEngineSettings.WorkDirectory.FullName, ocrEngineSettings.PathRuntime.FullName, out ocrengine, out exception);

            formproccessingengine.OcrEngine = ocrengine;

            //Enable Disable BarcodeEngine Based On Usage
            var barcodeengine = new BarcodeEngine();
            formproccessingengine.BarcodeEngine = barcodeengine;

            List<FormField> fields = new List<FormField>();
            foreach (var element in entieselement)
            {

                double X = 0.0;
                double Y = 0.0;
                double Width = 0.0;
                double Height = 0.0;
                if (element.ElementType != ElementType.Table)
                {
                    X = Convert.ToDouble(element.DivX);
                    Y = Convert.ToDouble(element.DivY);
                    Width = double.Parse(element.DivWidth.Replace("px", ""));
                    Height = double.Parse(element.DivHeight.Replace("px", ""));
                }
                else
                {
                    X = Convert.ToDouble(element.X);
                    Y = Convert.ToDouble(element.Y);
                    Width = double.Parse(element.Width);
                    Height = double.Parse(element.Height);
                }



                if (element.ElementType == ElementType.Rectangle)
                {
                    TextFormField text = new TextFormField();
                    text.Name = element.Id.ToString();
                    text.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(text);
                }
                //else if (element.ElementType.ToLower() == "omr")
                //{
                //    OmrFormField omr = new OmrFormField();
                //    omr.Name = element.Id.ToString();
                //    omr.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                //    formpage.Add(omr);
                //}
                else if (element.ElementType == ElementType.Image)
                {
                    ImageFormField image = new ImageFormField();
                    image.Name = element.Id.ToString();
                    image.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(image);
                }
                else if (element.ElementType == ElementType.Barcode2D)
                {
                    BarcodeFormField barcode = new BarcodeFormField();
                    barcode.Name = element.Id.ToString();
                    barcode.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    formpage.Add(barcode);
                }

                else if (element.ElementType == ElementType.Table)
                {
                    TableFormField tableField = new TableFormField();
                    tableField.Name = element.Id.ToString();
                    tableField.Bounds = new LogicalRectangle(X, Y, Width, Height, LogicalUnit.Pixel);
                    tableField.Rules = TableRules.NoRules;
                    foreach (List<TemplateElementDetail> elementdetails in entieselementdetails)
                    {
                        if (elementdetails != null)
                        {
                            foreach (TemplateElementDetail elementdetail in elementdetails)
                            {
                                if (elementdetail.ElementId == element.Id)
                                {
                                    double ColX = Convert.ToDouble(elementdetail.X);
                                    double ColY = Convert.ToDouble(elementdetail.Y);
                                    double ColWidth = double.Parse(elementdetail.Width);
                                    double ColHeight = double.Parse(elementdetail.Height);
                                    TextFormField column = new TextFormField();
                                    column.Name = elementdetail.Id.ToString();
                                    column.Bounds = new LogicalRectangle(ColX, ColY, ColWidth, ColHeight, LogicalUnit.Pixel);
                                    //column.OcrTextType = OcrTextType.Table;
                                    tableField.Columns.Add(new TableColumn(column));
                                }
                            }
                        }
                    }
                    formpage.Add(tableField);
                }
            }
            return formpage;
        }
    }
}
