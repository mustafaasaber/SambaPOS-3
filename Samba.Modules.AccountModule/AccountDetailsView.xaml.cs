using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Samba.Modules.AccountModule
{
    /// <summary>
    /// Interaction logic for AccountTransactionsView.xaml
    /// </summary>

    
    public partial class AccountDetailsView : UserControl
    {
        
        public AccountDetailsView(AccountDetailsViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
