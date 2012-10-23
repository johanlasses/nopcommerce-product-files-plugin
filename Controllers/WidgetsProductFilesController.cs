using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Product.Files.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Product.Files.Controllers
{
    public class WidgetsProductFilesController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly FilePluginSettings _filePluginSettings;
        
        public WidgetsProductFilesController(ISettingService settingService, FilePluginSettings filePluginSettings)
        {
            this._settingService = settingService;
            this._filePluginSettings = filePluginSettings;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.ZoneId = _filePluginSettings.WidgetZone;
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page under overview", Value = "productdetails_under_overview" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page Box", Value = "productbox_add_info"});
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page details before pictures", Value = "productdetails_before_pictures" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page details after pictures", Value = "productdetails_after_pictures" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page details overview top", Value = "productdetails_overview_top" });
            model.AvailableZones.Add(new SelectListItem() { Text = "Product page details overview bottom", Value = "productdetails_overview_bottom" });
            
            return View("Nop.Plugin.Product.Files.Views.ProductFiles.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _filePluginSettings.WidgetZone = model.ZoneId;
            _settingService.SaveSetting(_filePluginSettings);

            return Configure();
        }

     }
}