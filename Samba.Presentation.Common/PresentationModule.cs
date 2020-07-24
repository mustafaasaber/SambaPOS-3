using Prism.Ioc;
using Prism.Modularity;
using Samba.Presentation.Common.Widgets;
using System.Collections.Generic;

namespace Samba.Presentation.Common
{
    [Module(ModuleName = "PresentationModule")]
    public class PresentationModule : ModuleBase
    {

        public IEnumerable<IWidgetCreator> RegisteredCreators { get; set; }

        public PresentationModule(IEnumerable<IWidgetCreator> _RegisteredCreators)
        {
            RegisteredCreators = _RegisteredCreators;
        }
        protected override void OnInitialization()
        {
            foreach (var registeredCreator in RegisteredCreators)
            {
                WidgetCreatorRegistry.RegisterWidgetCreator(registeredCreator);
            }
            base.OnInitialization();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
