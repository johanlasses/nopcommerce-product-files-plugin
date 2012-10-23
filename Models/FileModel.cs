using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Product.Files.Models
{
    public class FileModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.Catalog.Products.Variants.Fields.Download")]
        [UIHint("Download")]
        public int DownloadId { get; set; }

        public int ProductId { get; set; }

        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
    }
}