using System.ComponentModel.Composition;
using System.Windows.Controls;
using Samba.Presentation.Common;
using Samba.Presentation.Services.Common;
using Samba.Presentation.ViewModels;

namespace Samba.Modules.ModifierModule
{
    /// <summary>
    /// Interaction logic for SelectedOrdersView.xaml
    /// </summary>
    /// 
    
    public partial class OrderTagGroupEditorView : UserControl
    {
        
        public OrderTagGroupEditorView(OrderTagGroupEditorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void TextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var t = sender as TextBox;
            if (t != null) t.BackgroundFocus();
        }
    }
}
