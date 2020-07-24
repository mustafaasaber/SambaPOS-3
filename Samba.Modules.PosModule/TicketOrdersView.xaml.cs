using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using Prism.Events;
using Samba.Domain.Models.Tickets;
using Samba.Presentation.Services.Common;

namespace Samba.Modules.PosModule
{
    /// <summary>
    /// Interaction logic for TicketOrdersView.xaml
    /// </summary>
    
    public partial class TicketOrdersView : UserControl
    {
        
        public TicketOrdersView(TicketOrdersViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            EventServiceFactory.EventService.GetEvent<GenericEvent<Order>>().Subscribe(
               x =>
               {
                   if (x.Topic == EventTopicNames.OrderAdded)
                       Scroller.ScrollToEnd();
               });


            EventServiceFactory.EventService.GetEvent<GenericEvent<EventAggregator>>().Subscribe(
                x =>
                {
                    if (x.Topic == EventTopicNames.ActivatePosView && !((TicketOrdersViewModel)DataContext).SelectedOrders.Any())
                    {
                        Scroller.ScrollToEnd();
                    }
                });
        }
    }
}
