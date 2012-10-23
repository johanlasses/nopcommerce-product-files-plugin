using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Product.Files.Models
{
    public class FileModelList : BaseNopModel
    {
        public FileModelList()
        {
            ProductFiles = new List<FileModel>();
        }

        public int DownloadId { get; set; }

        public int ProductId { get; set; }
        
        public IList<FileModel> ProductFiles { get; set; }

        public int NumberOfAvailableFiles { get { return ProductFiles.Count; } }

    }
}