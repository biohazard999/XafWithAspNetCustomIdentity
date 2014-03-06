using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using XafWithAspNetCustomIdentity.Model._Configuration;

namespace XafWithAspNetCustomIdentity.Module
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic.
    public sealed partial class XafWithAspNetCustomIdentityModule : ModuleBase
    {
        public XafWithAspNetCustomIdentityModule()
        {
            InitializeComponent();
        }

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
        {
            return base.GetDeclaredExportedTypes().Concat(ExportedTypesProvider.ExportedTypes);
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
