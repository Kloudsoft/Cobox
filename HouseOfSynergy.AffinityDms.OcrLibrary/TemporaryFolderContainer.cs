using HouseOfSynergy.PowerTools.Library.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
    public sealed class TemporaryFolderContainer:
        Disposable
    {
        private bool Disposed = false;

        public DirectoryInfo Directory { get; private set; }

        public TemporaryFolderContainer(DirectoryInfo directoryBase)
        {
            var guid = Guid.NewGuid();

            if (directoryBase == null) { throw (new ArgumentNullException("directoryBase")); }
            if (!directoryBase.Exists) { throw (new ArgumentException("The argument [directoryBase] points to a non-existing folder.", "directoryBase")); }

            do
            {
                guid = Guid.NewGuid();
                var path = Path.Combine(directoryBase.FullName, guid.ToString(GuidUtilities.EnumGuidFormat.FileSystem));

                if (!new DirectoryInfo(path).Exists)
                {
                    this.Directory = new DirectoryInfo(path);

                    this.Directory.Create();

                    break;
                }
            }
            while (true);
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // Managed.
                    if (this.Directory != null)
                    {
                        try
                        {
                            this.Directory.Refresh();

                            try
                            {
                                foreach (var file in this.Directory.GetFiles("*.*", SearchOption.AllDirectories))
                                {
                                    try { file.Delete(); }
                                    catch { }
                                }
                            }
                            catch
                            {
                                // Die quietly.
                            }

                            this.Directory.Delete(true);
                        }
                        catch
                        {
                        }
                    }
                }

                // Unmanaged.

                this.Disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
