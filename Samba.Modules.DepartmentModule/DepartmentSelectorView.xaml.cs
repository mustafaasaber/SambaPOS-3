using System.ComponentModel.Composition;
using System.Windows.Controls;
using Samba.Presentation.Services;
using Samba.Services;

namespace Samba.Modules.DepartmentModule
{
    /// <summary>
    /// Interaction logic for DepartmentButtonView.xaml
    /// </summary>

    
    public partial class DepartmentSelectorView : UserControl
    {
        private readonly IApplicationStateSetter _applicationStateSetter;

        
        public DepartmentSelectorView(IApplicationStateSetter applicationStateSetter, IApplicationState applicationState,
             IUserService userService)
        {
            InitializeComponent();
            _applicationStateSetter = applicationStateSetter;
            DataContext = new DepartmentSelectorViewModel(applicationState, _applicationStateSetter, userService);
        }
    }
}
