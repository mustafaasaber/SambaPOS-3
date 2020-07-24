using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Samba.Modules.MarketModule
{
    /// <summary>
    /// Interaction logic for MenuModuleView.xaml
    /// </summary>
    
    
    public partial class MarketModuleView : UserControl
    {
        
        public MarketModuleView(MarketModuleViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
