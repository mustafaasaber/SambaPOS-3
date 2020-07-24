using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Prism.Ioc;
using Prism.Modularity;
using Samba.Domain.Models.Tasks;
using Samba.Localization.Properties;
using Samba.Presentation.Common;
using Samba.Presentation.Common.ModelBase;
using Samba.Presentation.Services.Common;

namespace Samba.Modules.TaskModule
{
    [Module(ModuleName = "TaskModule")]
    public class TaskModule : ModuleBase
    {
        
        public TaskModule()
        {
            AddDashboardCommand<EntityCollectionViewModelBase<TaskTypeViewModel, TaskType>>(Resources.TaskType.ToPlural(), Resources.Settings, 20);
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
