using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Media;
using Nop.Services.Media;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Product.Files.Models;
using Nop.Plugin.Product.Files.Services;
using Nop.Plugin.Product.Files.Domain;
using Telerik.Web.Mvc;

namespace Nop.Plugin.Product.Files.Controllers
{
    [AdminAuthorize]
    public partial class DownloadController : Controller
    {
        private readonly IProductFileServices _productFileService;
        private readonly ISettingService _settingService;

        public DownloadController(IProductFileServices productFileService, ISettingService settingService)
        {
            this._productFileService = productFileService;
            this._settingService = settingService;
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(int productId)
        {
            FileModelList model = new FileModelList()
            {
                ProductId = productId,
                ProductFiles = new List<FileModel>()
            };
            var files = _productFileService.GetProductFilesByProductId(productId);
            foreach (var file in files)
            {
                var productFile = new FileModel()
                {
                    FileName = file.ProductFile.Filename,
                    Description = file.ProductFile.Description,
                    DownloadId = file.ProductFile.Id,
                    DisplayOrder = file.DisplayOrder,
                    Id = file.Id
                };
                model.ProductFiles.Add(productFile);
            }

            return View("Nop.Plugin.Product.Files.Views.ProductFiles.PublicInfo", model);
        }

        [ChildActionOnly]
        public ActionResult FileUpload(int productId)
        {
            FileModelList model = new FileModelList()
            {
                ProductId = productId
            };
            return PartialView("Nop.Plugin.Product.Files.Views.ProductFiles.ProductAdmin", model);
        }

        public ActionResult DownloadFile(int downloadId)
        {
            var download = _productFileService.GetProductFileById(downloadId);
            if (download == null)
                return Content("No download record found with the specified id");

           //use stored data
            if (download.ProductFile.DownloadBinary == null)
                return Content(string.Format("Download data is not available any more. Download ID={0}", downloadId));

            string fileName = !String.IsNullOrWhiteSpace(download.ProductFile.Filename) ? download.ProductFile.Filename : downloadId.ToString();
            string contentType = !String.IsNullOrWhiteSpace(download.ProductFile.ContentType) ? download.ProductFile.ContentType : "application/octet-stream";
            return new FileContentResult(download.ProductFile.DownloadBinary, contentType) { FileDownloadName = fileName + download.ProductFile.Extension };
        }

      
        [HttpPost]
        public ActionResult AsyncUpload(int productId)
        {
            //we process it distinct ways based on a browser
            //find more info here http://stackoverflow.com/questions/4884920/mvc3-valums-ajax-file-upload
            Stream stream = null;
            var fileName = "";
            var contentType = "";
            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;
            }
            else
            {
                //Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
            }

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            var download = new ProductFileMap();
            download.ProductFile = new ProductFile();
            download.ProductFile.DownloadGuid = Guid.NewGuid();
            download.ProductFile.UseDownloadUrl = false;
            download.ProductFile.DownloadUrl = "";
            download.ProductFile.DownloadBinary = fileBinary;
            download.ProductFile.ContentType = contentType;
            download.ProductFile.Filename = Path.GetFileNameWithoutExtension(fileName);
            download.ProductFile.Extension = fileExtension;
            download.ProductFile.IsNew = true;
            download.ProductId = productId;
            download.DisplayOrder = _productFileService.GetNumberOfProductFiles(productId) + 1;
            download.ProductFile.Description = "";

            _productFileService.InsertProductFile(download);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new
            {
                success = true,
                downloadId = download.Id,
                downloadUrl = Url.Action("DownloadFile", new { downloadId = download.Id })
            }, "text/plain");
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult ProductFilesList(GridCommand command, int productId)
        {
            var productFiles = _productFileService.GetProductFilesByProductId(productId);
            var productFilesModel = productFiles
                .Select(x =>
                {
                    return new FileModel()
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        DisplayOrder = x.DisplayOrder,
                        Description = x.ProductFile.Description,
                        FileName = x.ProductFile.Filename
                    };
                })
                .ToList();

            var model = new GridModel<FileModel>
            {
                Data = productFilesModel,
                Total = productFilesModel.Count
            };

            return new JsonResult
            {
                Data = model
            };
        }

        [GridAction(EnableCustomBinding = true)]
        public ActionResult ProductFilesUpdate(GridCommand command, FileModel model)
        {
            var productFile = _productFileService.GetProductFileById(model.Id);
            if (productFile == null)
                throw new ArgumentException("No product file mapping found with the specified id");

            productFile.ProductFile.Description = model.Description;
            productFile.DisplayOrder = model.DisplayOrder;
            _productFileService.UpdateProductFile(productFile);

            return ProductFilesList(command, productFile.ProductId);
        }

        [GridAction(EnableCustomBinding = true)]
        public ActionResult ProductFilesDelete(int id, GridCommand command)
        {
            var productFiles = _productFileService.GetProductFileById(id);
            if (productFiles == null)
                throw new ArgumentException("No product file mapping found with the specified id");

            var productId = productFiles.ProductId;
            _productFileService.DeleteProductFile(productFiles);

            return ProductFilesList(command, productId);
        }

    }
}
