using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Product.Files.Data;
using Nop.Plugin.Product.Files.Domain;
using Nop.Plugin.Product.Files.Services;

namespace Nop.Plugin.Product.Files
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<ProductFileService>().As<IProductFileServices>().InstancePerHttpRequest();

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                //register named context
                builder.Register<IDbContext>(c => new ProductFileObjectContext(dataProviderSettings.DataConnectionString))
                    .Named<IDbContext>("nop_object_context_product_file")
                    .InstancePerHttpRequest();

                builder.Register<ProductFileObjectContext>(c => new ProductFileObjectContext(dataProviderSettings.DataConnectionString))
                    .InstancePerHttpRequest();
            }
            else
            {
                //register named context
                builder.Register<IDbContext>(c => new ProductFileObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .Named<IDbContext>("nop_object_context_product_file")
                    .InstancePerHttpRequest();

                builder.Register<ProductFileObjectContext>(c => new ProductFileObjectContext(c.Resolve<DataSettings>().DataConnectionString))
                    .InstancePerHttpRequest();
            }

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ProductFileMap>>()
                .As<IRepository<ProductFileMap>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_product_file"))
                .InstancePerHttpRequest();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ProductFile>>()
                .As<IRepository<ProductFile>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_product_file"))
                .InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
