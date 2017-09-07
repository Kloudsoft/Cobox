using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;
using System.Web.UI.WebControls;
using System.IO;

namespace HouseOfSynergy.AffinityDms.WebRole.Pages
{
    public partial class ImageEditor : System.Web.UI.Page
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            try
            {
                long _documentId = 0;
                if (Request.QueryString["id"] != null)
                {
                    _documentId = long.Parse(Request.QueryString["id"]);
                    hDocumentId.Value = _documentId.ToString();
                    UploadImage(_documentId);
                }
                else
                    lblMessage.Text = "Document id is null";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void RadiosTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (RadiosTools.SelectedIndex)
            //{
            //    case 1:
            //        //Load tools that open dialogs
            //        RadImageEditor1.ToolsFile = "~/ImageEditor/Examples/Overview/Dialogs.xml";
            //        break;
            //    case 2:
            //        //Load Basic toolset
            //        RadImageEditor1.ToolsFile = "~/ImageEditor/Examples/Overview/Basic.xml";
            //        break;
            //    case 3:
            //        //Load Canvas toolset
            //        RadImageEditor1.ToolsFile = "~/ImageEditor/Examples/Overview/Canvas.xml";
            //        break;
            //    default:
            //        //Load Full set of tools
            //        RadImageEditor1.ToolsFile = null;
            //        break;
            //}
        }

        protected void RadiosStatusBar_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (RadiosStatusBar.SelectedIndex)
            //{
            //    case 1:
            //        RadImageEditor1.StatusBarMode = Telerik.Web.UI.ImageEditor.StatusBarMode.Top;
            //        break;
            //    case 2:
            //        RadImageEditor1.StatusBarMode = Telerik.Web.UI.ImageEditor.StatusBarMode.Hidden;
            //        break;
            //    default:
            //        RadImageEditor1.StatusBarMode = Telerik.Web.UI.ImageEditor.StatusBarMode.Bottom;
            //        break;
            //}
        }

        protected void RadiosEnableResize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RadImageEditor1.EnableResize = (RadiosEnableResize.SelectedIndex == 0);
        }

        protected void RadImageEditor1_ImageLoading(object sender, ImageEditorLoadingEventArgs args)
        {
            
            long _documentId = 0;
            try
            {
                if (hDocumentId.Value != "0")
                {
                    _documentId = long.Parse(hDocumentId.Value);
                    string DocumentsDirectory = Server.MapPath("../App_Data/Tenants/Documents/");
                    DirectoryInfo directoryinfo = new DirectoryInfo(DocumentsDirectory);
                    FileInfo[] files = directoryinfo.GetFiles(_documentId + ".*");
                    if (files.Count() == 1)
                    {
                        RadImageEditor1.ResetChanges();
                        byte[] imgData = File.ReadAllBytes(DocumentsDirectory + files[0].Name);
                        MemoryStream ms = new MemoryStream();
                        ms.Write(imgData, 0, imgData.Length);
                        using (EditableImage image = new EditableImage((MemoryStream)ms))
                        {
                            args.Image = image.Clone();
                            args.Cancel = true;
                        }

                        ms.Close();
                        ms.Dispose();
                    }
                }
                else
                {
                    lblMessage.Text = "No image found for this document";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        

        protected void RadImageEditor1_ImageSaving(object sender, ImageEditorSavingEventArgs e)
        {
            string filename = e.FileName;
            try
            {
                if (!string.IsNullOrWhiteSpace(filename))
                {
                    EditableImage ei = e.Image;
                    System.Drawing.Image img = ei.Image;
                    string DocumentsDirectory = Server.MapPath("../App_Data/Tenants/Documents/");
                    DirectoryInfo directoryinfo = new DirectoryInfo(DocumentsDirectory);
                    FileInfo[] files = directoryinfo.GetFiles(filename + ".*");
                    if (files.Count() == 1)
                    {
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, img.RawFormat);
                        File.WriteAllBytes(Path.Combine(files[0].DirectoryName, files[0].FullName), ms.ToArray());
                        ms.Close();
                        ms.Dispose();
                        img.Dispose();
                        ei.Dispose();

                    }
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        #endregion

        #region User Define Methods

        protected void UploadImage(long documentid)
        {
            string DocumentsDirectory = Server.MapPath("../App_Data/Tenants/Documents/");
            DirectoryInfo directoryinfo = new DirectoryInfo(DocumentsDirectory);
            FileInfo[] files = directoryinfo.GetFiles(documentid + ".*");
            if (files.Count() == 1)
            {

                RadImageEditor1.ResetChanges();
                byte[] imgData = File.ReadAllBytes(DocumentsDirectory + files[0].Name);
                MemoryStream ms = new MemoryStream();
                ms.Write(imgData, 0, imgData.Length);
                //Context.Cache.Insert(Session.SessionID + "UploadedFile", ms, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                RadImageEditor1.ImageUrl = "../App_Data/Document/" + files[0].Name + "?" + DateTime.UtcNow.Ticks.ToString();

                ms.Close();
                ms.Dispose();
            }

        }
        #endregion

        //protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        //{
        //    //Clear changes and remove uploaded image from Cache
        //    RadImageEditor1.ResetChanges();
        //    Context.Cache.Remove(Session.SessionID + "UploadedFile");
        //    using (Stream stream = e.File.InputStream)
        //    {
        //        byte[] imgData = new byte[stream.Length];
        //        stream.Read(imgData, 0, imgData.Length);
        //        MemoryStream ms = new MemoryStream();
        //        ms.Write(imgData, 0, imgData.Length);

        //        Context.Cache.Insert(Session.SessionID + "UploadedFile", ms, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
        //    }
        //}

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    UploadedFileCollection uploadfileCol = AsyncUpload1.UploadedFiles;

        //    //Clear changes and remove uploaded image from Cache
        //    RadImageEditor1.ResetChanges();
        //    Context.Cache.Remove(Session.SessionID + "UploadedFile");
        //    using (Stream stream = uploadfileCol[0].InputStream)
        //    {
        //        byte[] imgData = new byte[stream.Length];
        //        stream.Read(imgData, 0, imgData.Length);
        //        MemoryStream ms = new MemoryStream();
        //        ms.Write(imgData, 0, imgData.Length);

        //        Context.Cache.Insert(Session.SessionID + "UploadedFile", ms, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
        //    }
        //}
    }
}