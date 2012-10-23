using System;
using System.Collections.Generic;

namespace Nop.Plugin.Product.Files.Domain
{
    /// <summary>
    /// Represents a product file
    /// </summary>
    public partial class ProductFile : Nop.Core.Domain.Media.Download
    {
        public virtual string Description { get; set; }
    }

}
