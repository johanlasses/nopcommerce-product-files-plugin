using System.Data.Entity.ModelConfiguration;
using Nop.Plugin.Product.Files.Domain;

namespace Nop.Plugin.Product.Files.Data
{
    public class ProductFileRecordMap : EntityTypeConfiguration<ProductFile>
    {
        public ProductFileRecordMap()
        {
            ToTable("ProductFiles");

            this.HasKey(ff => ff.Id);

            Property(ff => ff.DownloadGuid).IsRequired();
            Property(ff => ff.UseDownloadUrl).IsRequired();
            Property(ff => ff.IsNew).IsRequired();
        }
    }

    public class ProductFileMappingRecordMap : EntityTypeConfiguration<ProductFileMap>
    {
        public ProductFileMappingRecordMap()
        {
            ToTable("Product_File_Mapping");

            this.HasKey(ff => ff.Id);
            
            Property(ff => ff.ProductId).IsRequired();
            Property(ff => ff.DisplayOrder).IsRequired();
        }
    }
}