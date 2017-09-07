using HouseOfSynergy.AffinityDms.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Utility
{
    public class DirectoryHelper
    {
        public static bool DeleteAllFiles(string folderPath, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (!Directory.Exists(folderPath)) { throw new Exception("Unable to find the following directory"); }
                string[] filesArray = Directory.GetFiles(folderPath);
                var fileEntries = filesArray.ToList();
                foreach (var fileEntry in fileEntries)
                {
                    if (File.Exists(fileEntry))
                    {
                        var fileInfo = new FileInfo(fileEntry.ToString());
                        if (fileInfo != null)
                        {
                            fileInfo.Delete();
                            fileInfo = null;
                            result = true;
                        }
                        else
                        {
                            throw new Exception("Unable to find the following document");
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool DeleteFile(string filePath, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (filePath == null) { throw new Exception("No Files found"); }
                if (!Directory.Exists(Path.GetDirectoryName(filePath))) { throw new Exception("Directory not found"); }
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        var fileInfo = new FileInfo(filePath);
                        fileInfo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool DeleteFile(List<string> filePath, out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                foreach (var file in filePath)
                {
                    if (file == null) { throw new Exception("No Files found"); }
                    if (!Directory.Exists(Path.GetDirectoryName(file))) { throw new Exception("Directory not found"); }
                    if (!string.IsNullOrEmpty(file))
                    {
                        if (File.Exists(file))
                        {
                            var fileInfo = new FileInfo(file);
                            fileInfo.Delete();
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool  GetExternalFiles(string folderPath,out List<string> filesList,out Exception exception)
        {
            bool result = false;
            filesList = new List<string>();
            exception = null;
            try
            {
                var fileEntries = new List<FileUploadStatus>();
                if (Directory.Exists(folderPath))
                {
                    string[] filesArray = Directory.GetFiles(folderPath);

                    fileEntries = filesArray.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });

                    foreach (var fileEntry in fileEntries)
                    {
                        filesList.Add(fileEntry.File.ToString());
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
    }
}