using System.ComponentModel.Composition;
using System.Windows.Controls;
using Samba.Presentation.Common;

namespace Samba.Modules.WorkperiodModule
{
    /// <summary>
    /// Interaction logic for EndOfDayView.xaml
    /// </summary>
    /// 
    
    public partial class WorkPeriodsView : UserControl
    {
        
        public WorkPeriodsView(WorkPeriodsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void StartDescriptionEditor_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            StartDescriptionEditor.BackgroundFocus();
        }

        private void EndDescriptionEditor_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            EndDescriptionEditor.BackgroundFocus();
        }
    }
}
