Product Files for NopCommerce

This plugin makes it possible to upload documents to products. There is a widget that lists all uploaded files. This widget should be used on the product page. The administration part of this plugin requires modification of the _CreateAndUpdate template for Products in the administration. If there will be a way to hook into the administration will that way be used in the future.

1. Install Plugin Configuration -> Plugins
2. Decide in which widget area you want the file list. Add more zones in WidgetsProductFilesController.
3. Check the localization strings by searching on "Plugin.ProductFiles" Configuration -> Languages -> View string resources.
4. Enable widget by setting it to active Content -> Widgets
5. Edit a product and notice the new tab for files.

When the plugin is uninstalled the database tables for the files aren't deleted, only renamed with todays date. This to avoid deletion of files. Please delete the tables manually if you don't want them.

Code you need to add in Nop.Admin/Views/Product/_CreateAndUpdate.cshtml

//Add new tab for the files. Check that the plugin is installed before displaying it. Helper function below to render the plugin view.
if(EngineContext.Current.Resolve<Nop.Core.Plugins.IPluginFinder>().GetPluginDescriptorBySystemName("Product.Files") != null)
        x.Add().Text(T("Plugins.ProductFiles.Tabheading").Text).Content(TabFiles().ToHtmlString());
   
@helper TabFiles() { @Html.Action( "FileUpload", "Download", new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Product.Files.Controllers" }, { "area", "" }, {"productId", Model.Id}}) }

