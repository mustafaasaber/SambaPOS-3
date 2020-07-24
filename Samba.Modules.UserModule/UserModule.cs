using System.ComponentModel.Composition;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Samba.Domain.Models.Users;
using Samba.Localization.Properties;
using Samba.Presentation.Common;
using Samba.Presentation.Common.ModelBase;

namespace Samba.Modules.UserModule
{
    [Module(ModuleName = "UserModule")]
    public class UserModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;

        
        public UserModule(IRegionManager regionManager)
        {
            AddDashboardCommand<EntityCollectionViewModelBase<UserRoleViewModel, UserRole>>(Resources.UserRoleList, Resources.Users, 50);
            AddDashboardCommand<EntityCollectionViewModelBase<UserViewModel, User>>(Resources.UserList, Resources.Users);
            _regionManager = regionManager;
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void OnInitialization()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.RightUserRegion, typeof(LoggedInUserView));
        }
    }
}
