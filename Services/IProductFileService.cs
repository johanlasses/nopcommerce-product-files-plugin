using System.Collections.Generic;
using Nop.Services.Media;
using Nop.Plugin.Product.Files.Domain;

namespace Nop.Plugin.Product.Files.Services
{
    /// <summary>
    /// Tax rate service interface
    /// </summary>
    public partial interface IProductFileServices 
    {
        /// <summary>
        /// Gets a download
        /// </summary>
        /// <param name="downloadId">Download identifier</param>
        /// <returns>Download</returns>
        ProductFileMap GetProductFileById(int downloadId);

        /// <summary>
        /// Gets a download by Product Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>List of ProductFiles</returns>
        IList<ProductFileMap> GetProductFilesByProductId(int productId);

        /// <summary>
        /// Deletes a download
        /// </summary>
        /// <param name="download">ProductFile</param>
        void DeleteProductFile(ProductFileMap productFile);

        /// <summary>
        /// Inserts a download 
        /// </summary>
        /// <param name="download">Download</param>
        void InsertProductFile(ProductFileMap productFile);

        /// <summary>
        /// Update a download 
        /// </summary>
        /// <param name="download">Download</param>
        void UpdateProductFile(ProductFileMap productFile);

        int GetNumberOfProductFiles(int productId);
    }
}
