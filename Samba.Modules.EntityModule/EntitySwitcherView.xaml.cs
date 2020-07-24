using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Samba.Modules.EntityModule
{
    /// <summary>
    /// Interaction logic for ResourceSwitcherView.xaml
    /// </summary>

    
    public partial class EntitySwitcherView : UserControl
    {
        
        public EntitySwitcherView(EntitySwitcherViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
