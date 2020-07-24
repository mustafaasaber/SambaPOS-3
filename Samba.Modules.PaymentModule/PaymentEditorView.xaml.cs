using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Samba.Modules.PaymentModule
{
    /// <summary>
    /// Interaction logic for PaymentView.xaml
    /// </summary>
    
    
    public partial class PaymentEditorView : UserControl
    {
        
        public PaymentEditorView(PaymentEditorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
