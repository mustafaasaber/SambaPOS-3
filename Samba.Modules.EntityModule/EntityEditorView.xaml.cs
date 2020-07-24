using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Samba.Presentation.Common;

namespace Samba.Modules.EntityModule
{
    /// <summary>
    /// Interaction logic for AccountEditorView.xaml
    /// </summary>
    
    
    public partial class EntityEditorView : UserControl
    {
        
        public EntityEditorView(EntityEditorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
