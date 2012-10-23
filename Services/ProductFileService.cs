using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Media;
using Nop.Plugin.Product.Files.Domain;
using Nop.Services.Events;

namespace Nop.Plugin.Product.Files.Services
{
    /// <summary>
    /// Tax rate service
    /// </summary>
    public partial class ProductFileService :  IProductFileServices
    {  
        #region Fields

        private readonly IRepository<ProductFileMap> _productFileRepository;
        private readonly IEventPublisher _eventPubisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productFileRepository">Download repository</param>
        /// <param name="eventPubisher"></param>
        public ProductFileService(IRepository<ProductFileMap> productFileRepository,
            IEventPublisher eventPubisher)
        {
            _productFileRepository = productFileRepository;
            _eventPubisher = eventPubisher;
        }

        #endregion

        #region Methods

        public virtual int GetNumberOfProductFiles(int productId)
        {
            var x = (from pf in _productFileRepository.Table
                     where pf.ProductId == productId
                     select (int?)pf.DisplayOrder);

            if (x.Count() == 0)
                return 0;
            else
                return (int)x.Max();
        }

        /// <summary>
        /// Gets a productFile
        /// </summary>
        /// <param name="productFileId">Download identifier</param>
        /// <returns>Download</returns>
        public virtual ProductFileMap GetProductFileById(int productFileId)
        {
            if (productFileId == 0)
                return null;
            
            var productFile = _productFileRepository.GetById(productFileId);
            return productFile;
        }

        /// <summary>
        /// Gets a productFile by GUID
        /// </summary>
        /// <param name="productFileGuid">Download GUID</param>
        /// <returns>Download</returns>
        public virtual IList<ProductFileMap> GetProductFilesByProductId(int productId)
        {
           
            var query = from pf in _productFileRepository.Table
                        where pf.ProductId == productId
                        orderby pf.DisplayOrder, pf.Id
                        select pf;
            var productFiles = query.ToList();
            return productFiles;
        }

        /// <summary>
        /// Deletes a productFile
        /// </summary>
        /// <param name="productFileMap">Download</param>
        public virtual void DeleteProductFile(ProductFileMap productFileMap)
        {
            if (productFileMap == null)
                throw new ArgumentNullException("productFile");

            _productFileRepository.Delete(productFileMap);

            _eventPubisher.EntityDeleted(productFileMap);
        }

        /// <summary>
        /// Inserts a productFile
        /// </summary>
        /// <param name="productFileMap">Download</param>
        public virtual void InsertProductFile(ProductFileMap productFileMap)
        {
            if (productFileMap == null)
                throw new ArgumentNullException("productFile");

            _productFileRepository.Insert(productFileMap);

            _eventPubisher.EntityInserted(productFileMap);
        }


        /// <summary>
        /// Updates the productFile
        /// </summary>
        /// <param name="productFile">productFile</param>
        public virtual void UpdateProductFile(ProductFileMap productFileMap)
        {
            if (productFileMap == null)
                throw new ArgumentNullException("productFile");

            _productFileRepository.Update(productFileMap);

            _eventPubisher.EntityUpdated(productFileMap);
        }

        #endregion

    }
}

