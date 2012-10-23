using System;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Product.Files.Domain
{
    public class ProductFileMap : BaseEntity
    {
        public virtual int ProductId { get; set; }

        public virtual int DisplayOrder { get; set; }

        public virtual ProductFile ProductFile { get; set; }
    }
}