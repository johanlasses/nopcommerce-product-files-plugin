using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Product.Files
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Product.Files.Configure",
                 "Plugins/ProductFiles/Configure",
                 new { controller = "WidgetsProductFiles", action = "Configure" },
                 new[] { "Nop.Plugin.Product.Files.Controllers" }
            );

            routes.MapRoute("Plugin.Product.Files.PublicInfo",
                 "Plugins/ProductFiles/PublicInfo",
                 new { controller = "Download", action = "PublicInfo" },
                 new[] { "Nop.Plugin.Product.Files.Controllers" }
            );

            routes.MapRoute("Plugin.Product.Files.AsyncUpload",
                "Plugins/ProductFiles/AsyncUpload",
                new { controller = "Download", action = "AsyncUpload" },
                new[] { "Nop.Plugin.Product.Files.Controllers" }
           );

           routes.MapRoute("Plugin.Product.Files.FileUpload",
                "Plugins/ProductFiles/FileUpload",
                new { controller = "Download", action = "FileUpload" },
                new[] { "Nop.Plugin.Product.Files.Controllers" }
           );

           routes.MapRoute("Plugin.Product.Files.FileDownload",
                "Plugins/ProductFiles/DownloadFile",
                new { controller = "Download", action = "DownloadFile" },
                new[] { "Nop.Plugin.Product.Files.Controllers" }
           );

           routes.MapRoute("Plugin.Product.Files.ProductFilesList",
               "Plugins/ProductFiles/ProductFilesList",
               new { controller = "Download", action = "ProductFilesList" },
               new[] { "Nop.Plugin.Product.Files.Controllers" }
          );

           routes.MapRoute("Plugin.Product.Files.ProductFilesDelete",
              "Plugins/ProductFiles/ProductFilesDelete",
              new { controller = "Download", action = "ProductFilesDelete" },
              new[] { "Nop.Plugin.Product.Files.Controllers" }
         );
           routes.MapRoute("Plugin.Product.Files.ProductFilesUpdate",
              "Plugins/ProductFiles/ProductFilesUpdate",
              new { controller = "Download", action = "ProductFilesUpdate" },
              new[] { "Nop.Plugin.Product.Files.Controllers" }
         );
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
