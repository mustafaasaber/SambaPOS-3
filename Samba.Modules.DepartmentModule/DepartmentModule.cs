using System.ComponentModel.Composition;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Samba.Localization.Properties;
using Samba.Presentation.Common;
using Samba.Presentation.Services.Common;
using Samba.Services;

namespace Samba.Modules.DepartmentModule
{
    [Module(ModuleName = "DepartmentModule")]
    public class DepartmentModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDepartmentService _departmentService;


        public DepartmentModule(IRegionManager regionManager, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            _regionManager = regionManager;
            AddDashboardCommand<DepartmentListViewModel>(Resources.Departments, Resources.Settings,20);
            PermissionRegistry.RegisterPermission(PermissionNames.ChangeDepartment, PermissionCategories.Department, Resources.CanChangeDepartment);

            foreach (var department in _departmentService.GetDepartments())
            {
                PermissionRegistry.RegisterPermission(PermissionNames.UseDepartment + department.Id, PermissionCategories.Department, department.Name);
            }
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void OnInitialization()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.UserRegion, typeof(DepartmentSelectorView));
        }
    }
}
