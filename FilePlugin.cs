using System;
using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Plugin.Product.Files.Data;

namespace Nop.Plugin.Product.Files
{
    public class FilePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ProductFileObjectContext _productFileObjectContext;
        private readonly ISettingService _settingService;
        private readonly FilePluginSettings _fileSettings;

        public FilePlugin(ISettingService settingService, FilePluginSettings fileSettings, ProductFileObjectContext productFileObjectContext)
        {
            this._settingService = settingService;
            this._fileSettings = fileSettings;
            this._productFileObjectContext = productFileObjectContext;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_fileSettings.WidgetZone)
                       ? new List<string>() { _fileSettings.WidgetZone }
                       : new List<string>() { "head_html_tag" };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsProductFiles";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Product.Files.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "Download";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Product.Files.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new FilePluginSettings()
            {
                WidgetZone = "productbox_add_info"
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.ProductFiles.Blockheading", "Product Files");
            this.AddOrUpdatePluginLocaleResource("Plugins.ProductFiles.Tabheadingr", "Files");
            
            //Install the database tables
            _productFileObjectContext.Install();

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<FilePluginSettings>();

            this.DeletePluginLocaleResource("Plugins.ProductFiles.Blockheading");
            this.DeletePluginLocaleResource("Plugins.ProductFiles.Tabheadingr");
            
            //Uninstall, rename the database tables
            _productFileObjectContext.Uninstall();

            base.Uninstall();
        }
    }
}
